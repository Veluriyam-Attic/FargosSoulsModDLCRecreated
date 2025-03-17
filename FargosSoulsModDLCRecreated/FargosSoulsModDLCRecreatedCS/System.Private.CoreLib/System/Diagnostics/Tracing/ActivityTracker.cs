using System;
using System.Threading;
using System.Threading.Tasks;

namespace System.Diagnostics.Tracing
{
	// Token: 0x02000705 RID: 1797
	internal class ActivityTracker
	{
		// Token: 0x0600598C RID: 22924 RVA: 0x001B1EAC File Offset: 0x001B10AC
		public void OnStart(string providerName, string activityName, int task, ref Guid activityId, ref Guid relatedActivityId, EventActivityOptions options, bool useTplSource = true)
		{
			if (this.m_current == null)
			{
				if (this.m_checkedForEnable)
				{
					return;
				}
				this.m_checkedForEnable = true;
				if (useTplSource && TplEventSource.Log.IsEnabled(EventLevel.Informational, (EventKeywords)128L))
				{
					this.Enable();
				}
				if (this.m_current == null)
				{
					return;
				}
			}
			ActivityTracker.ActivityInfo value = this.m_current.Value;
			string text = ActivityTracker.NormalizeActivityName(providerName, activityName, task);
			TplEventSource tplEventSource = useTplSource ? TplEventSource.Log : null;
			bool flag = tplEventSource != null && tplEventSource.Debug;
			if (flag)
			{
				tplEventSource.DebugFacilityMessage("OnStartEnter", text);
				tplEventSource.DebugFacilityMessage("OnStartEnterActivityState", ActivityTracker.ActivityInfo.LiveActivities(value));
			}
			if (value != null)
			{
				if (value.m_level >= 100)
				{
					activityId = Guid.Empty;
					relatedActivityId = Guid.Empty;
					if (flag)
					{
						tplEventSource.DebugFacilityMessage("OnStartRET", "Fail");
					}
					return;
				}
				if ((options & EventActivityOptions.Recursive) == EventActivityOptions.None)
				{
					ActivityTracker.ActivityInfo activityInfo = ActivityTracker.FindActiveActivity(text, value);
					if (activityInfo != null)
					{
						this.OnStop(providerName, activityName, task, ref activityId, true);
						value = this.m_current.Value;
					}
				}
			}
			long uniqueId;
			if (value == null)
			{
				uniqueId = Interlocked.Increment(ref ActivityTracker.m_nextId);
			}
			else
			{
				uniqueId = Interlocked.Increment(ref value.m_lastChildID);
			}
			relatedActivityId = EventSource.CurrentThreadActivityId;
			ActivityTracker.ActivityInfo activityInfo2 = new ActivityTracker.ActivityInfo(text, uniqueId, value, relatedActivityId, options);
			this.m_current.Value = activityInfo2;
			activityId = activityInfo2.ActivityId;
			if (flag)
			{
				tplEventSource.DebugFacilityMessage("OnStartRetActivityState", ActivityTracker.ActivityInfo.LiveActivities(activityInfo2));
				tplEventSource.DebugFacilityMessage1("OnStartRet", activityId.ToString(), relatedActivityId.ToString());
			}
		}

		// Token: 0x0600598D RID: 22925 RVA: 0x001B2040 File Offset: 0x001B1240
		public void OnStop(string providerName, string activityName, int task, ref Guid activityId, bool useTplSource = true)
		{
			if (this.m_current == null)
			{
				return;
			}
			string text = ActivityTracker.NormalizeActivityName(providerName, activityName, task);
			TplEventSource tplEventSource = useTplSource ? TplEventSource.Log : null;
			bool flag = tplEventSource != null && tplEventSource.Debug;
			if (flag)
			{
				tplEventSource.DebugFacilityMessage("OnStopEnter", text);
				tplEventSource.DebugFacilityMessage("OnStopEnterActivityState", ActivityTracker.ActivityInfo.LiveActivities(this.m_current.Value));
			}
			ActivityTracker.ActivityInfo activityInfo;
			for (;;)
			{
				ActivityTracker.ActivityInfo value = this.m_current.Value;
				activityInfo = null;
				ActivityTracker.ActivityInfo activityInfo2 = ActivityTracker.FindActiveActivity(text, value);
				if (activityInfo2 == null)
				{
					break;
				}
				activityId = activityInfo2.ActivityId;
				ActivityTracker.ActivityInfo activityInfo3 = value;
				while (activityInfo3 != activityInfo2 && activityInfo3 != null)
				{
					if (activityInfo3.m_stopped != 0)
					{
						activityInfo3 = activityInfo3.m_creator;
					}
					else
					{
						if (activityInfo3.CanBeOrphan())
						{
							if (activityInfo == null)
							{
								activityInfo = activityInfo3;
							}
						}
						else
						{
							activityInfo3.m_stopped = 1;
						}
						activityInfo3 = activityInfo3.m_creator;
					}
				}
				if (Interlocked.CompareExchange(ref activityInfo2.m_stopped, 1, 0) == 0)
				{
					goto Block_11;
				}
			}
			activityId = Guid.Empty;
			if (flag)
			{
				tplEventSource.DebugFacilityMessage("OnStopRET", "Fail");
			}
			return;
			Block_11:
			if (activityInfo == null)
			{
				ActivityTracker.ActivityInfo activityInfo2;
				activityInfo = activityInfo2.m_creator;
			}
			this.m_current.Value = activityInfo;
			if (flag)
			{
				tplEventSource.DebugFacilityMessage("OnStopRetActivityState", ActivityTracker.ActivityInfo.LiveActivities(activityInfo));
				tplEventSource.DebugFacilityMessage("OnStopRet", activityId.ToString());
			}
		}

		// Token: 0x0600598E RID: 22926 RVA: 0x001B2190 File Offset: 0x001B1390
		public void Enable()
		{
			if (this.m_current == null)
			{
				try
				{
					this.m_current = new AsyncLocal<ActivityTracker.ActivityInfo>(new Action<AsyncLocalValueChangedArgs<ActivityTracker.ActivityInfo>>(this.ActivityChanging));
				}
				catch (NotImplementedException)
				{
					Debugger.Log(0, null, "Activity Enabled() called but AsyncLocals Not Supported (pre V4.6).  Ignoring Enable");
				}
			}
		}

		// Token: 0x17000EA6 RID: 3750
		// (get) Token: 0x0600598F RID: 22927 RVA: 0x001B21E0 File Offset: 0x001B13E0
		public static ActivityTracker Instance
		{
			get
			{
				return ActivityTracker.s_activityTrackerInstance;
			}
		}

		// Token: 0x06005990 RID: 22928 RVA: 0x001B21E8 File Offset: 0x001B13E8
		private static ActivityTracker.ActivityInfo FindActiveActivity(string name, ActivityTracker.ActivityInfo startLocation)
		{
			for (ActivityTracker.ActivityInfo activityInfo = startLocation; activityInfo != null; activityInfo = activityInfo.m_creator)
			{
				if (name == activityInfo.m_name && activityInfo.m_stopped == 0)
				{
					return activityInfo;
				}
			}
			return null;
		}

		// Token: 0x06005991 RID: 22929 RVA: 0x001B221C File Offset: 0x001B141C
		private static string NormalizeActivityName(string providerName, string activityName, int task)
		{
			if (activityName.EndsWith("Start", StringComparison.Ordinal))
			{
				return providerName + activityName.AsSpan(0, activityName.Length - "Start".Length);
			}
			if (activityName.EndsWith("Stop", StringComparison.Ordinal))
			{
				return providerName + activityName.AsSpan(0, activityName.Length - "Stop".Length);
			}
			if (task != 0)
			{
				return providerName + "task" + task.ToString();
			}
			return providerName + activityName;
		}

		// Token: 0x06005992 RID: 22930 RVA: 0x001B22AC File Offset: 0x001B14AC
		private void ActivityChanging(AsyncLocalValueChangedArgs<ActivityTracker.ActivityInfo> args)
		{
			ActivityTracker.ActivityInfo activityInfo = args.CurrentValue;
			ActivityTracker.ActivityInfo previousValue = args.PreviousValue;
			if (previousValue != null && previousValue.m_creator == activityInfo && (activityInfo == null || previousValue.m_activityIdToRestore != activityInfo.ActivityId))
			{
				EventSource.SetCurrentThreadActivityId(previousValue.m_activityIdToRestore);
				return;
			}
			while (activityInfo != null)
			{
				if (activityInfo.m_stopped == 0)
				{
					EventSource.SetCurrentThreadActivityId(activityInfo.ActivityId);
					return;
				}
				activityInfo = activityInfo.m_creator;
			}
		}

		// Token: 0x040019E2 RID: 6626
		private AsyncLocal<ActivityTracker.ActivityInfo> m_current;

		// Token: 0x040019E3 RID: 6627
		private bool m_checkedForEnable;

		// Token: 0x040019E4 RID: 6628
		private static readonly ActivityTracker s_activityTrackerInstance = new ActivityTracker();

		// Token: 0x040019E5 RID: 6629
		private static long m_nextId;

		// Token: 0x02000706 RID: 1798
		private class ActivityInfo
		{
			// Token: 0x06005995 RID: 22933 RVA: 0x001B2324 File Offset: 0x001B1524
			public ActivityInfo(string name, long uniqueId, ActivityTracker.ActivityInfo creator, Guid activityIDToRestore, EventActivityOptions options)
			{
				this.m_name = name;
				this.m_eventOptions = options;
				this.m_creator = creator;
				this.m_uniqueId = uniqueId;
				this.m_level = ((creator != null) ? (creator.m_level + 1) : 0);
				this.m_activityIdToRestore = activityIDToRestore;
				this.CreateActivityPathGuid(out this.m_guid, out this.m_activityPathGuidOffset);
			}

			// Token: 0x17000EA7 RID: 3751
			// (get) Token: 0x06005996 RID: 22934 RVA: 0x001B2382 File Offset: 0x001B1582
			public Guid ActivityId
			{
				get
				{
					return this.m_guid;
				}
			}

			// Token: 0x06005997 RID: 22935 RVA: 0x001B238A File Offset: 0x001B158A
			public static string Path(ActivityTracker.ActivityInfo activityInfo)
			{
				if (activityInfo == null)
				{
					return "";
				}
				return ActivityTracker.ActivityInfo.Path(activityInfo.m_creator) + "/" + activityInfo.m_uniqueId.ToString();
			}

			// Token: 0x06005998 RID: 22936 RVA: 0x001B23B5 File Offset: 0x001B15B5
			public override string ToString()
			{
				return this.m_name + "(" + ActivityTracker.ActivityInfo.Path(this) + ((this.m_stopped != 0) ? ",DEAD)" : ")");
			}

			// Token: 0x06005999 RID: 22937 RVA: 0x001B23E1 File Offset: 0x001B15E1
			public static string LiveActivities(ActivityTracker.ActivityInfo list)
			{
				if (list == null)
				{
					return "";
				}
				return list.ToString() + ";" + ActivityTracker.ActivityInfo.LiveActivities(list.m_creator);
			}

			// Token: 0x0600599A RID: 22938 RVA: 0x001B2407 File Offset: 0x001B1607
			public bool CanBeOrphan()
			{
				return (this.m_eventOptions & EventActivityOptions.Detachable) != EventActivityOptions.None;
			}

			// Token: 0x0600599B RID: 22939 RVA: 0x001B2418 File Offset: 0x001B1618
			private unsafe void CreateActivityPathGuid(out Guid idRet, out int activityPathGuidOffset)
			{
				fixed (Guid* ptr = &idRet)
				{
					Guid* outPtr = ptr;
					int whereToAddId = 0;
					if (this.m_creator != null)
					{
						whereToAddId = this.m_creator.m_activityPathGuidOffset;
						idRet = this.m_creator.m_guid;
					}
					else
					{
						int domainID = Thread.GetDomainID();
						whereToAddId = ActivityTracker.ActivityInfo.AddIdToGuid(outPtr, whereToAddId, (uint)domainID, false);
					}
					activityPathGuidOffset = ActivityTracker.ActivityInfo.AddIdToGuid(outPtr, whereToAddId, (uint)this.m_uniqueId, false);
					if (12 < activityPathGuidOffset)
					{
						this.CreateOverflowGuid(outPtr);
					}
				}
			}

			// Token: 0x0600599C RID: 22940 RVA: 0x001B2488 File Offset: 0x001B1688
			private unsafe void CreateOverflowGuid(Guid* outPtr)
			{
				for (ActivityTracker.ActivityInfo creator = this.m_creator; creator != null; creator = creator.m_creator)
				{
					if (creator.m_activityPathGuidOffset <= 10)
					{
						uint id = (uint)Interlocked.Increment(ref creator.m_lastChildID);
						*outPtr = creator.m_guid;
						int num = ActivityTracker.ActivityInfo.AddIdToGuid(outPtr, creator.m_activityPathGuidOffset, id, true);
						if (num <= 12)
						{
							break;
						}
					}
				}
			}

			// Token: 0x0600599D RID: 22941 RVA: 0x001B24E0 File Offset: 0x001B16E0
			private unsafe static int AddIdToGuid(Guid* outPtr, int whereToAddId, uint id, bool overflow = false)
			{
				byte* ptr = (byte*)outPtr;
				byte* ptr2 = ptr + 12;
				ptr += whereToAddId;
				if (ptr2 == ptr)
				{
					return 13;
				}
				if (0U < id && id <= 10U && !overflow)
				{
					ActivityTracker.ActivityInfo.WriteNibble(ref ptr, ptr2, id);
				}
				else
				{
					uint num = 4U;
					if (id <= 255U)
					{
						num = 1U;
					}
					else if (id <= 65535U)
					{
						num = 2U;
					}
					else if (id <= 16777215U)
					{
						num = 3U;
					}
					if (overflow)
					{
						if (ptr2 == ptr + 2)
						{
							return 13;
						}
						ActivityTracker.ActivityInfo.WriteNibble(ref ptr, ptr2, 11U);
					}
					ActivityTracker.ActivityInfo.WriteNibble(ref ptr, ptr2, 12U + (num - 1U));
					if (ptr < ptr2 && *ptr != 0)
					{
						if (id < 4096U)
						{
							*ptr = (byte)(192U + (id >> 8));
							id &= 255U;
						}
						ptr++;
					}
					while (0U < num)
					{
						if (ptr2 == ptr)
						{
							ptr++;
							break;
						}
						*(ptr++) = (byte)id;
						id >>= 8;
						num -= 1U;
					}
				}
				*(int*)(outPtr + (IntPtr)3 * 4 / (IntPtr)sizeof(Guid)) = (int)(*(uint*)outPtr + *(uint*)(outPtr + 4 / sizeof(Guid)) + *(uint*)(outPtr + (IntPtr)2 * 4 / (IntPtr)sizeof(Guid)) + 1503500717U ^ (uint)Environment.ProcessId);
				return (int)((long)((byte*)ptr - (byte*)outPtr));
			}

			// Token: 0x0600599E RID: 22942 RVA: 0x001B25D8 File Offset: 0x001B17D8
			private unsafe static void WriteNibble(ref byte* ptr, byte* endPtr, uint value)
			{
				if (*ptr != 0)
				{
					byte* ptr2 = ptr;
					ptr = ptr2 + 1;
					byte* ptr3 = ptr2;
					*ptr3 |= (byte)value;
					return;
				}
				*ptr = (byte)(value << 4);
			}

			// Token: 0x040019E6 RID: 6630
			internal readonly string m_name;

			// Token: 0x040019E7 RID: 6631
			private readonly long m_uniqueId;

			// Token: 0x040019E8 RID: 6632
			internal readonly Guid m_guid;

			// Token: 0x040019E9 RID: 6633
			internal readonly int m_activityPathGuidOffset;

			// Token: 0x040019EA RID: 6634
			internal readonly int m_level;

			// Token: 0x040019EB RID: 6635
			internal readonly EventActivityOptions m_eventOptions;

			// Token: 0x040019EC RID: 6636
			internal long m_lastChildID;

			// Token: 0x040019ED RID: 6637
			internal int m_stopped;

			// Token: 0x040019EE RID: 6638
			internal readonly ActivityTracker.ActivityInfo m_creator;

			// Token: 0x040019EF RID: 6639
			internal readonly Guid m_activityIdToRestore;
		}
	}
}
