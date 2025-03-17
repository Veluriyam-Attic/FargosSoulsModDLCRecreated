using System;

namespace System.Reflection.Emit
{
	// Token: 0x02000663 RID: 1635
	public class OpCodes
	{
		// Token: 0x060053AE RID: 21422 RVA: 0x0019B1A0 File Offset: 0x0019A3A0
		public static bool TakesSingleByteArgument(OpCode inst)
		{
			OperandType operandType = inst.OperandType;
			return operandType - OperandType.ShortInlineBrTarget <= 1 || operandType == OperandType.ShortInlineVar;
		}

		// Token: 0x04001639 RID: 5689
		public static readonly OpCode Nop = new OpCode(OpCodeValues.Nop, 6556325);

		// Token: 0x0400163A RID: 5690
		public static readonly OpCode Break = new OpCode(OpCodeValues.Break, 6556197);

		// Token: 0x0400163B RID: 5691
		public static readonly OpCode Ldarg_0 = new OpCode(OpCodeValues.Ldarg_0, 275120805);

		// Token: 0x0400163C RID: 5692
		public static readonly OpCode Ldarg_1 = new OpCode(OpCodeValues.Ldarg_1, 275120805);

		// Token: 0x0400163D RID: 5693
		public static readonly OpCode Ldarg_2 = new OpCode(OpCodeValues.Ldarg_2, 275120805);

		// Token: 0x0400163E RID: 5694
		public static readonly OpCode Ldarg_3 = new OpCode(OpCodeValues.Ldarg_3, 275120805);

		// Token: 0x0400163F RID: 5695
		public static readonly OpCode Ldloc_0 = new OpCode(OpCodeValues.Ldloc_0, 275120805);

		// Token: 0x04001640 RID: 5696
		public static readonly OpCode Ldloc_1 = new OpCode(OpCodeValues.Ldloc_1, 275120805);

		// Token: 0x04001641 RID: 5697
		public static readonly OpCode Ldloc_2 = new OpCode(OpCodeValues.Ldloc_2, 275120805);

		// Token: 0x04001642 RID: 5698
		public static readonly OpCode Ldloc_3 = new OpCode(OpCodeValues.Ldloc_3, 275120805);

		// Token: 0x04001643 RID: 5699
		public static readonly OpCode Stloc_0 = new OpCode(OpCodeValues.Stloc_0, -261877083);

		// Token: 0x04001644 RID: 5700
		public static readonly OpCode Stloc_1 = new OpCode(OpCodeValues.Stloc_1, -261877083);

		// Token: 0x04001645 RID: 5701
		public static readonly OpCode Stloc_2 = new OpCode(OpCodeValues.Stloc_2, -261877083);

		// Token: 0x04001646 RID: 5702
		public static readonly OpCode Stloc_3 = new OpCode(OpCodeValues.Stloc_3, -261877083);

		// Token: 0x04001647 RID: 5703
		public static readonly OpCode Ldarg_S = new OpCode(OpCodeValues.Ldarg_S, 275120818);

		// Token: 0x04001648 RID: 5704
		public static readonly OpCode Ldarga_S = new OpCode(OpCodeValues.Ldarga_S, 275382962);

		// Token: 0x04001649 RID: 5705
		public static readonly OpCode Starg_S = new OpCode(OpCodeValues.Starg_S, -261877070);

		// Token: 0x0400164A RID: 5706
		public static readonly OpCode Ldloc_S = new OpCode(OpCodeValues.Ldloc_S, 275120818);

		// Token: 0x0400164B RID: 5707
		public static readonly OpCode Ldloca_S = new OpCode(OpCodeValues.Ldloca_S, 275382962);

		// Token: 0x0400164C RID: 5708
		public static readonly OpCode Stloc_S = new OpCode(OpCodeValues.Stloc_S, -261877070);

		// Token: 0x0400164D RID: 5709
		public static readonly OpCode Ldnull = new OpCode(OpCodeValues.Ldnull, 275909285);

		// Token: 0x0400164E RID: 5710
		public static readonly OpCode Ldc_I4_M1 = new OpCode(OpCodeValues.Ldc_I4_M1, 275382949);

		// Token: 0x0400164F RID: 5711
		public static readonly OpCode Ldc_I4_0 = new OpCode(OpCodeValues.Ldc_I4_0, 275382949);

		// Token: 0x04001650 RID: 5712
		public static readonly OpCode Ldc_I4_1 = new OpCode(OpCodeValues.Ldc_I4_1, 275382949);

		// Token: 0x04001651 RID: 5713
		public static readonly OpCode Ldc_I4_2 = new OpCode(OpCodeValues.Ldc_I4_2, 275382949);

		// Token: 0x04001652 RID: 5714
		public static readonly OpCode Ldc_I4_3 = new OpCode(OpCodeValues.Ldc_I4_3, 275382949);

		// Token: 0x04001653 RID: 5715
		public static readonly OpCode Ldc_I4_4 = new OpCode(OpCodeValues.Ldc_I4_4, 275382949);

		// Token: 0x04001654 RID: 5716
		public static readonly OpCode Ldc_I4_5 = new OpCode(OpCodeValues.Ldc_I4_5, 275382949);

		// Token: 0x04001655 RID: 5717
		public static readonly OpCode Ldc_I4_6 = new OpCode(OpCodeValues.Ldc_I4_6, 275382949);

		// Token: 0x04001656 RID: 5718
		public static readonly OpCode Ldc_I4_7 = new OpCode(OpCodeValues.Ldc_I4_7, 275382949);

		// Token: 0x04001657 RID: 5719
		public static readonly OpCode Ldc_I4_8 = new OpCode(OpCodeValues.Ldc_I4_8, 275382949);

		// Token: 0x04001658 RID: 5720
		public static readonly OpCode Ldc_I4_S = new OpCode(OpCodeValues.Ldc_I4_S, 275382960);

		// Token: 0x04001659 RID: 5721
		public static readonly OpCode Ldc_I4 = new OpCode(OpCodeValues.Ldc_I4, 275384994);

		// Token: 0x0400165A RID: 5722
		public static readonly OpCode Ldc_I8 = new OpCode(OpCodeValues.Ldc_I8, 275516067);

		// Token: 0x0400165B RID: 5723
		public static readonly OpCode Ldc_R4 = new OpCode(OpCodeValues.Ldc_R4, 275647153);

		// Token: 0x0400165C RID: 5724
		public static readonly OpCode Ldc_R8 = new OpCode(OpCodeValues.Ldc_R8, 275778215);

		// Token: 0x0400165D RID: 5725
		public static readonly OpCode Dup = new OpCode(OpCodeValues.Dup, 275258021);

		// Token: 0x0400165E RID: 5726
		public static readonly OpCode Pop = new OpCode(OpCodeValues.Pop, -261875035);

		// Token: 0x0400165F RID: 5727
		public static readonly OpCode Jmp = new OpCode(OpCodeValues.Jmp, 23333444);

		// Token: 0x04001660 RID: 5728
		public static readonly OpCode Call = new OpCode(OpCodeValues.Call, 7842372);

		// Token: 0x04001661 RID: 5729
		public static readonly OpCode Calli = new OpCode(OpCodeValues.Calli, 7842377);

		// Token: 0x04001662 RID: 5730
		public static readonly OpCode Ret = new OpCode(OpCodeValues.Ret, 23440101);

		// Token: 0x04001663 RID: 5731
		public static readonly OpCode Br_S = new OpCode(OpCodeValues.Br_S, 23331343);

		// Token: 0x04001664 RID: 5732
		public static readonly OpCode Brfalse_S = new OpCode(OpCodeValues.Brfalse_S, -261868945);

		// Token: 0x04001665 RID: 5733
		public static readonly OpCode Brtrue_S = new OpCode(OpCodeValues.Brtrue_S, -261868945);

		// Token: 0x04001666 RID: 5734
		public static readonly OpCode Beq_S = new OpCode(OpCodeValues.Beq_S, -530308497);

		// Token: 0x04001667 RID: 5735
		public static readonly OpCode Bge_S = new OpCode(OpCodeValues.Bge_S, -530308497);

		// Token: 0x04001668 RID: 5736
		public static readonly OpCode Bgt_S = new OpCode(OpCodeValues.Bgt_S, -530308497);

		// Token: 0x04001669 RID: 5737
		public static readonly OpCode Ble_S = new OpCode(OpCodeValues.Ble_S, -530308497);

		// Token: 0x0400166A RID: 5738
		public static readonly OpCode Blt_S = new OpCode(OpCodeValues.Blt_S, -530308497);

		// Token: 0x0400166B RID: 5739
		public static readonly OpCode Bne_Un_S = new OpCode(OpCodeValues.Bne_Un_S, -530308497);

		// Token: 0x0400166C RID: 5740
		public static readonly OpCode Bge_Un_S = new OpCode(OpCodeValues.Bge_Un_S, -530308497);

		// Token: 0x0400166D RID: 5741
		public static readonly OpCode Bgt_Un_S = new OpCode(OpCodeValues.Bgt_Un_S, -530308497);

		// Token: 0x0400166E RID: 5742
		public static readonly OpCode Ble_Un_S = new OpCode(OpCodeValues.Ble_Un_S, -530308497);

		// Token: 0x0400166F RID: 5743
		public static readonly OpCode Blt_Un_S = new OpCode(OpCodeValues.Blt_Un_S, -530308497);

		// Token: 0x04001670 RID: 5744
		public static readonly OpCode Br = new OpCode(OpCodeValues.Br, 23333376);

		// Token: 0x04001671 RID: 5745
		public static readonly OpCode Brfalse = new OpCode(OpCodeValues.Brfalse, -261866912);

		// Token: 0x04001672 RID: 5746
		public static readonly OpCode Brtrue = new OpCode(OpCodeValues.Brtrue, -261866912);

		// Token: 0x04001673 RID: 5747
		public static readonly OpCode Beq = new OpCode(OpCodeValues.Beq, -530308512);

		// Token: 0x04001674 RID: 5748
		public static readonly OpCode Bge = new OpCode(OpCodeValues.Bge, -530308512);

		// Token: 0x04001675 RID: 5749
		public static readonly OpCode Bgt = new OpCode(OpCodeValues.Bgt, -530308512);

		// Token: 0x04001676 RID: 5750
		public static readonly OpCode Ble = new OpCode(OpCodeValues.Ble, -530308512);

		// Token: 0x04001677 RID: 5751
		public static readonly OpCode Blt = new OpCode(OpCodeValues.Blt, -530308512);

		// Token: 0x04001678 RID: 5752
		public static readonly OpCode Bne_Un = new OpCode(OpCodeValues.Bne_Un, -530308512);

		// Token: 0x04001679 RID: 5753
		public static readonly OpCode Bge_Un = new OpCode(OpCodeValues.Bge_Un, -530308512);

		// Token: 0x0400167A RID: 5754
		public static readonly OpCode Bgt_Un = new OpCode(OpCodeValues.Bgt_Un, -530308512);

		// Token: 0x0400167B RID: 5755
		public static readonly OpCode Ble_Un = new OpCode(OpCodeValues.Ble_Un, -530308512);

		// Token: 0x0400167C RID: 5756
		public static readonly OpCode Blt_Un = new OpCode(OpCodeValues.Blt_Un, -530308512);

		// Token: 0x0400167D RID: 5757
		public static readonly OpCode Switch = new OpCode(OpCodeValues.Switch, -261866901);

		// Token: 0x0400167E RID: 5758
		public static readonly OpCode Ldind_I1 = new OpCode(OpCodeValues.Ldind_I1, 6961829);

		// Token: 0x0400167F RID: 5759
		public static readonly OpCode Ldind_U1 = new OpCode(OpCodeValues.Ldind_U1, 6961829);

		// Token: 0x04001680 RID: 5760
		public static readonly OpCode Ldind_I2 = new OpCode(OpCodeValues.Ldind_I2, 6961829);

		// Token: 0x04001681 RID: 5761
		public static readonly OpCode Ldind_U2 = new OpCode(OpCodeValues.Ldind_U2, 6961829);

		// Token: 0x04001682 RID: 5762
		public static readonly OpCode Ldind_I4 = new OpCode(OpCodeValues.Ldind_I4, 6961829);

		// Token: 0x04001683 RID: 5763
		public static readonly OpCode Ldind_U4 = new OpCode(OpCodeValues.Ldind_U4, 6961829);

		// Token: 0x04001684 RID: 5764
		public static readonly OpCode Ldind_I8 = new OpCode(OpCodeValues.Ldind_I8, 7092901);

		// Token: 0x04001685 RID: 5765
		public static readonly OpCode Ldind_I = new OpCode(OpCodeValues.Ldind_I, 6961829);

		// Token: 0x04001686 RID: 5766
		public static readonly OpCode Ldind_R4 = new OpCode(OpCodeValues.Ldind_R4, 7223973);

		// Token: 0x04001687 RID: 5767
		public static readonly OpCode Ldind_R8 = new OpCode(OpCodeValues.Ldind_R8, 7355045);

		// Token: 0x04001688 RID: 5768
		public static readonly OpCode Ldind_Ref = new OpCode(OpCodeValues.Ldind_Ref, 7486117);

		// Token: 0x04001689 RID: 5769
		public static readonly OpCode Stind_Ref = new OpCode(OpCodeValues.Stind_Ref, -530294107);

		// Token: 0x0400168A RID: 5770
		public static readonly OpCode Stind_I1 = new OpCode(OpCodeValues.Stind_I1, -530294107);

		// Token: 0x0400168B RID: 5771
		public static readonly OpCode Stind_I2 = new OpCode(OpCodeValues.Stind_I2, -530294107);

		// Token: 0x0400168C RID: 5772
		public static readonly OpCode Stind_I4 = new OpCode(OpCodeValues.Stind_I4, -530294107);

		// Token: 0x0400168D RID: 5773
		public static readonly OpCode Stind_I8 = new OpCode(OpCodeValues.Stind_I8, -530290011);

		// Token: 0x0400168E RID: 5774
		public static readonly OpCode Stind_R4 = new OpCode(OpCodeValues.Stind_R4, -530281819);

		// Token: 0x0400168F RID: 5775
		public static readonly OpCode Stind_R8 = new OpCode(OpCodeValues.Stind_R8, -530277723);

		// Token: 0x04001690 RID: 5776
		public static readonly OpCode Add = new OpCode(OpCodeValues.Add, -261739867);

		// Token: 0x04001691 RID: 5777
		public static readonly OpCode Sub = new OpCode(OpCodeValues.Sub, -261739867);

		// Token: 0x04001692 RID: 5778
		public static readonly OpCode Mul = new OpCode(OpCodeValues.Mul, -261739867);

		// Token: 0x04001693 RID: 5779
		public static readonly OpCode Div = new OpCode(OpCodeValues.Div, -261739867);

		// Token: 0x04001694 RID: 5780
		public static readonly OpCode Div_Un = new OpCode(OpCodeValues.Div_Un, -261739867);

		// Token: 0x04001695 RID: 5781
		public static readonly OpCode Rem = new OpCode(OpCodeValues.Rem, -261739867);

		// Token: 0x04001696 RID: 5782
		public static readonly OpCode Rem_Un = new OpCode(OpCodeValues.Rem_Un, -261739867);

		// Token: 0x04001697 RID: 5783
		public static readonly OpCode And = new OpCode(OpCodeValues.And, -261739867);

		// Token: 0x04001698 RID: 5784
		public static readonly OpCode Or = new OpCode(OpCodeValues.Or, -261739867);

		// Token: 0x04001699 RID: 5785
		public static readonly OpCode Xor = new OpCode(OpCodeValues.Xor, -261739867);

		// Token: 0x0400169A RID: 5786
		public static readonly OpCode Shl = new OpCode(OpCodeValues.Shl, -261739867);

		// Token: 0x0400169B RID: 5787
		public static readonly OpCode Shr = new OpCode(OpCodeValues.Shr, -261739867);

		// Token: 0x0400169C RID: 5788
		public static readonly OpCode Shr_Un = new OpCode(OpCodeValues.Shr_Un, -261739867);

		// Token: 0x0400169D RID: 5789
		public static readonly OpCode Neg = new OpCode(OpCodeValues.Neg, 6691493);

		// Token: 0x0400169E RID: 5790
		public static readonly OpCode Not = new OpCode(OpCodeValues.Not, 6691493);

		// Token: 0x0400169F RID: 5791
		public static readonly OpCode Conv_I1 = new OpCode(OpCodeValues.Conv_I1, 6953637);

		// Token: 0x040016A0 RID: 5792
		public static readonly OpCode Conv_I2 = new OpCode(OpCodeValues.Conv_I2, 6953637);

		// Token: 0x040016A1 RID: 5793
		public static readonly OpCode Conv_I4 = new OpCode(OpCodeValues.Conv_I4, 6953637);

		// Token: 0x040016A2 RID: 5794
		public static readonly OpCode Conv_I8 = new OpCode(OpCodeValues.Conv_I8, 7084709);

		// Token: 0x040016A3 RID: 5795
		public static readonly OpCode Conv_R4 = new OpCode(OpCodeValues.Conv_R4, 7215781);

		// Token: 0x040016A4 RID: 5796
		public static readonly OpCode Conv_R8 = new OpCode(OpCodeValues.Conv_R8, 7346853);

		// Token: 0x040016A5 RID: 5797
		public static readonly OpCode Conv_U4 = new OpCode(OpCodeValues.Conv_U4, 6953637);

		// Token: 0x040016A6 RID: 5798
		public static readonly OpCode Conv_U8 = new OpCode(OpCodeValues.Conv_U8, 7084709);

		// Token: 0x040016A7 RID: 5799
		public static readonly OpCode Callvirt = new OpCode(OpCodeValues.Callvirt, 7841348);

		// Token: 0x040016A8 RID: 5800
		public static readonly OpCode Cpobj = new OpCode(OpCodeValues.Cpobj, -530295123);

		// Token: 0x040016A9 RID: 5801
		public static readonly OpCode Ldobj = new OpCode(OpCodeValues.Ldobj, 6698669);

		// Token: 0x040016AA RID: 5802
		public static readonly OpCode Ldstr = new OpCode(OpCodeValues.Ldstr, 275908266);

		// Token: 0x040016AB RID: 5803
		public static readonly OpCode Newobj = new OpCode(OpCodeValues.Newobj, 276014660);

		// Token: 0x040016AC RID: 5804
		public static readonly OpCode Castclass = new OpCode(OpCodeValues.Castclass, 7513773);

		// Token: 0x040016AD RID: 5805
		public static readonly OpCode Isinst = new OpCode(OpCodeValues.Isinst, 6989485);

		// Token: 0x040016AE RID: 5806
		public static readonly OpCode Conv_R_Un = new OpCode(OpCodeValues.Conv_R_Un, 7346853);

		// Token: 0x040016AF RID: 5807
		public static readonly OpCode Unbox = new OpCode(OpCodeValues.Unbox, 6990509);

		// Token: 0x040016B0 RID: 5808
		public static readonly OpCode Throw = new OpCode(OpCodeValues.Throw, -245061883);

		// Token: 0x040016B1 RID: 5809
		public static readonly OpCode Ldfld = new OpCode(OpCodeValues.Ldfld, 6727329);

		// Token: 0x040016B2 RID: 5810
		public static readonly OpCode Ldflda = new OpCode(OpCodeValues.Ldflda, 6989473);

		// Token: 0x040016B3 RID: 5811
		public static readonly OpCode Stfld = new OpCode(OpCodeValues.Stfld, -530270559);

		// Token: 0x040016B4 RID: 5812
		public static readonly OpCode Ldsfld = new OpCode(OpCodeValues.Ldsfld, 275121825);

		// Token: 0x040016B5 RID: 5813
		public static readonly OpCode Ldsflda = new OpCode(OpCodeValues.Ldsflda, 275383969);

		// Token: 0x040016B6 RID: 5814
		public static readonly OpCode Stsfld = new OpCode(OpCodeValues.Stsfld, -261876063);

		// Token: 0x040016B7 RID: 5815
		public static readonly OpCode Stobj = new OpCode(OpCodeValues.Stobj, -530298195);

		// Token: 0x040016B8 RID: 5816
		public static readonly OpCode Conv_Ovf_I1_Un = new OpCode(OpCodeValues.Conv_Ovf_I1_Un, 6953637);

		// Token: 0x040016B9 RID: 5817
		public static readonly OpCode Conv_Ovf_I2_Un = new OpCode(OpCodeValues.Conv_Ovf_I2_Un, 6953637);

		// Token: 0x040016BA RID: 5818
		public static readonly OpCode Conv_Ovf_I4_Un = new OpCode(OpCodeValues.Conv_Ovf_I4_Un, 6953637);

		// Token: 0x040016BB RID: 5819
		public static readonly OpCode Conv_Ovf_I8_Un = new OpCode(OpCodeValues.Conv_Ovf_I8_Un, 7084709);

		// Token: 0x040016BC RID: 5820
		public static readonly OpCode Conv_Ovf_U1_Un = new OpCode(OpCodeValues.Conv_Ovf_U1_Un, 6953637);

		// Token: 0x040016BD RID: 5821
		public static readonly OpCode Conv_Ovf_U2_Un = new OpCode(OpCodeValues.Conv_Ovf_U2_Un, 6953637);

		// Token: 0x040016BE RID: 5822
		public static readonly OpCode Conv_Ovf_U4_Un = new OpCode(OpCodeValues.Conv_Ovf_U4_Un, 6953637);

		// Token: 0x040016BF RID: 5823
		public static readonly OpCode Conv_Ovf_U8_Un = new OpCode(OpCodeValues.Conv_Ovf_U8_Un, 7084709);

		// Token: 0x040016C0 RID: 5824
		public static readonly OpCode Conv_Ovf_I_Un = new OpCode(OpCodeValues.Conv_Ovf_I_Un, 6953637);

		// Token: 0x040016C1 RID: 5825
		public static readonly OpCode Conv_Ovf_U_Un = new OpCode(OpCodeValues.Conv_Ovf_U_Un, 6953637);

		// Token: 0x040016C2 RID: 5826
		public static readonly OpCode Box = new OpCode(OpCodeValues.Box, 7477933);

		// Token: 0x040016C3 RID: 5827
		public static readonly OpCode Newarr = new OpCode(OpCodeValues.Newarr, 7485101);

		// Token: 0x040016C4 RID: 5828
		public static readonly OpCode Ldlen = new OpCode(OpCodeValues.Ldlen, 6989477);

		// Token: 0x040016C5 RID: 5829
		public static readonly OpCode Ldelema = new OpCode(OpCodeValues.Ldelema, -261437779);

		// Token: 0x040016C6 RID: 5830
		public static readonly OpCode Ldelem_I1 = new OpCode(OpCodeValues.Ldelem_I1, -261437787);

		// Token: 0x040016C7 RID: 5831
		public static readonly OpCode Ldelem_U1 = new OpCode(OpCodeValues.Ldelem_U1, -261437787);

		// Token: 0x040016C8 RID: 5832
		public static readonly OpCode Ldelem_I2 = new OpCode(OpCodeValues.Ldelem_I2, -261437787);

		// Token: 0x040016C9 RID: 5833
		public static readonly OpCode Ldelem_U2 = new OpCode(OpCodeValues.Ldelem_U2, -261437787);

		// Token: 0x040016CA RID: 5834
		public static readonly OpCode Ldelem_I4 = new OpCode(OpCodeValues.Ldelem_I4, -261437787);

		// Token: 0x040016CB RID: 5835
		public static readonly OpCode Ldelem_U4 = new OpCode(OpCodeValues.Ldelem_U4, -261437787);

		// Token: 0x040016CC RID: 5836
		public static readonly OpCode Ldelem_I8 = new OpCode(OpCodeValues.Ldelem_I8, -261306715);

		// Token: 0x040016CD RID: 5837
		public static readonly OpCode Ldelem_I = new OpCode(OpCodeValues.Ldelem_I, -261437787);

		// Token: 0x040016CE RID: 5838
		public static readonly OpCode Ldelem_R4 = new OpCode(OpCodeValues.Ldelem_R4, -261175643);

		// Token: 0x040016CF RID: 5839
		public static readonly OpCode Ldelem_R8 = new OpCode(OpCodeValues.Ldelem_R8, -261044571);

		// Token: 0x040016D0 RID: 5840
		public static readonly OpCode Ldelem_Ref = new OpCode(OpCodeValues.Ldelem_Ref, -260913499);

		// Token: 0x040016D1 RID: 5841
		public static readonly OpCode Stelem_I = new OpCode(OpCodeValues.Stelem_I, -798697819);

		// Token: 0x040016D2 RID: 5842
		public static readonly OpCode Stelem_I1 = new OpCode(OpCodeValues.Stelem_I1, -798697819);

		// Token: 0x040016D3 RID: 5843
		public static readonly OpCode Stelem_I2 = new OpCode(OpCodeValues.Stelem_I2, -798697819);

		// Token: 0x040016D4 RID: 5844
		public static readonly OpCode Stelem_I4 = new OpCode(OpCodeValues.Stelem_I4, -798697819);

		// Token: 0x040016D5 RID: 5845
		public static readonly OpCode Stelem_I8 = new OpCode(OpCodeValues.Stelem_I8, -798693723);

		// Token: 0x040016D6 RID: 5846
		public static readonly OpCode Stelem_R4 = new OpCode(OpCodeValues.Stelem_R4, -798689627);

		// Token: 0x040016D7 RID: 5847
		public static readonly OpCode Stelem_R8 = new OpCode(OpCodeValues.Stelem_R8, -798685531);

		// Token: 0x040016D8 RID: 5848
		public static readonly OpCode Stelem_Ref = new OpCode(OpCodeValues.Stelem_Ref, -798681435);

		// Token: 0x040016D9 RID: 5849
		public static readonly OpCode Ldelem = new OpCode(OpCodeValues.Ldelem, -261699923);

		// Token: 0x040016DA RID: 5850
		public static readonly OpCode Stelem = new OpCode(OpCodeValues.Stelem, -798636371);

		// Token: 0x040016DB RID: 5851
		public static readonly OpCode Unbox_Any = new OpCode(OpCodeValues.Unbox_Any, 6727341);

		// Token: 0x040016DC RID: 5852
		public static readonly OpCode Conv_Ovf_I1 = new OpCode(OpCodeValues.Conv_Ovf_I1, 6953637);

		// Token: 0x040016DD RID: 5853
		public static readonly OpCode Conv_Ovf_U1 = new OpCode(OpCodeValues.Conv_Ovf_U1, 6953637);

		// Token: 0x040016DE RID: 5854
		public static readonly OpCode Conv_Ovf_I2 = new OpCode(OpCodeValues.Conv_Ovf_I2, 6953637);

		// Token: 0x040016DF RID: 5855
		public static readonly OpCode Conv_Ovf_U2 = new OpCode(OpCodeValues.Conv_Ovf_U2, 6953637);

		// Token: 0x040016E0 RID: 5856
		public static readonly OpCode Conv_Ovf_I4 = new OpCode(OpCodeValues.Conv_Ovf_I4, 6953637);

		// Token: 0x040016E1 RID: 5857
		public static readonly OpCode Conv_Ovf_U4 = new OpCode(OpCodeValues.Conv_Ovf_U4, 6953637);

		// Token: 0x040016E2 RID: 5858
		public static readonly OpCode Conv_Ovf_I8 = new OpCode(OpCodeValues.Conv_Ovf_I8, 7084709);

		// Token: 0x040016E3 RID: 5859
		public static readonly OpCode Conv_Ovf_U8 = new OpCode(OpCodeValues.Conv_Ovf_U8, 7084709);

		// Token: 0x040016E4 RID: 5860
		public static readonly OpCode Refanyval = new OpCode(OpCodeValues.Refanyval, 6953645);

		// Token: 0x040016E5 RID: 5861
		public static readonly OpCode Ckfinite = new OpCode(OpCodeValues.Ckfinite, 7346853);

		// Token: 0x040016E6 RID: 5862
		public static readonly OpCode Mkrefany = new OpCode(OpCodeValues.Mkrefany, 6699693);

		// Token: 0x040016E7 RID: 5863
		public static readonly OpCode Ldtoken = new OpCode(OpCodeValues.Ldtoken, 275385004);

		// Token: 0x040016E8 RID: 5864
		public static readonly OpCode Conv_U2 = new OpCode(OpCodeValues.Conv_U2, 6953637);

		// Token: 0x040016E9 RID: 5865
		public static readonly OpCode Conv_U1 = new OpCode(OpCodeValues.Conv_U1, 6953637);

		// Token: 0x040016EA RID: 5866
		public static readonly OpCode Conv_I = new OpCode(OpCodeValues.Conv_I, 6953637);

		// Token: 0x040016EB RID: 5867
		public static readonly OpCode Conv_Ovf_I = new OpCode(OpCodeValues.Conv_Ovf_I, 6953637);

		// Token: 0x040016EC RID: 5868
		public static readonly OpCode Conv_Ovf_U = new OpCode(OpCodeValues.Conv_Ovf_U, 6953637);

		// Token: 0x040016ED RID: 5869
		public static readonly OpCode Add_Ovf = new OpCode(OpCodeValues.Add_Ovf, -261739867);

		// Token: 0x040016EE RID: 5870
		public static readonly OpCode Add_Ovf_Un = new OpCode(OpCodeValues.Add_Ovf_Un, -261739867);

		// Token: 0x040016EF RID: 5871
		public static readonly OpCode Mul_Ovf = new OpCode(OpCodeValues.Mul_Ovf, -261739867);

		// Token: 0x040016F0 RID: 5872
		public static readonly OpCode Mul_Ovf_Un = new OpCode(OpCodeValues.Mul_Ovf_Un, -261739867);

		// Token: 0x040016F1 RID: 5873
		public static readonly OpCode Sub_Ovf = new OpCode(OpCodeValues.Sub_Ovf, -261739867);

		// Token: 0x040016F2 RID: 5874
		public static readonly OpCode Sub_Ovf_Un = new OpCode(OpCodeValues.Sub_Ovf_Un, -261739867);

		// Token: 0x040016F3 RID: 5875
		public static readonly OpCode Endfinally = new OpCode(OpCodeValues.Endfinally, 23333605);

		// Token: 0x040016F4 RID: 5876
		public static readonly OpCode Leave = new OpCode(OpCodeValues.Leave, 23333376);

		// Token: 0x040016F5 RID: 5877
		public static readonly OpCode Leave_S = new OpCode(OpCodeValues.Leave_S, 23333391);

		// Token: 0x040016F6 RID: 5878
		public static readonly OpCode Stind_I = new OpCode(OpCodeValues.Stind_I, -530294107);

		// Token: 0x040016F7 RID: 5879
		public static readonly OpCode Conv_U = new OpCode(OpCodeValues.Conv_U, 6953637);

		// Token: 0x040016F8 RID: 5880
		public static readonly OpCode Prefix7 = new OpCode(OpCodeValues.Prefix7, 6554757);

		// Token: 0x040016F9 RID: 5881
		public static readonly OpCode Prefix6 = new OpCode(OpCodeValues.Prefix6, 6554757);

		// Token: 0x040016FA RID: 5882
		public static readonly OpCode Prefix5 = new OpCode(OpCodeValues.Prefix5, 6554757);

		// Token: 0x040016FB RID: 5883
		public static readonly OpCode Prefix4 = new OpCode(OpCodeValues.Prefix4, 6554757);

		// Token: 0x040016FC RID: 5884
		public static readonly OpCode Prefix3 = new OpCode(OpCodeValues.Prefix3, 6554757);

		// Token: 0x040016FD RID: 5885
		public static readonly OpCode Prefix2 = new OpCode(OpCodeValues.Prefix2, 6554757);

		// Token: 0x040016FE RID: 5886
		public static readonly OpCode Prefix1 = new OpCode(OpCodeValues.Prefix1, 6554757);

		// Token: 0x040016FF RID: 5887
		public static readonly OpCode Prefixref = new OpCode(OpCodeValues.Prefixref, 6554757);

		// Token: 0x04001700 RID: 5888
		public static readonly OpCode Arglist = new OpCode(OpCodeValues.Arglist, 279579301);

		// Token: 0x04001701 RID: 5889
		public static readonly OpCode Ceq = new OpCode(OpCodeValues.Ceq, -257283419);

		// Token: 0x04001702 RID: 5890
		public static readonly OpCode Cgt = new OpCode(OpCodeValues.Cgt, -257283419);

		// Token: 0x04001703 RID: 5891
		public static readonly OpCode Cgt_Un = new OpCode(OpCodeValues.Cgt_Un, -257283419);

		// Token: 0x04001704 RID: 5892
		public static readonly OpCode Clt = new OpCode(OpCodeValues.Clt, -257283419);

		// Token: 0x04001705 RID: 5893
		public static readonly OpCode Clt_Un = new OpCode(OpCodeValues.Clt_Un, -257283419);

		// Token: 0x04001706 RID: 5894
		public static readonly OpCode Ldftn = new OpCode(OpCodeValues.Ldftn, 279579300);

		// Token: 0x04001707 RID: 5895
		public static readonly OpCode Ldvirtftn = new OpCode(OpCodeValues.Ldvirtftn, 11184804);

		// Token: 0x04001708 RID: 5896
		public static readonly OpCode Ldarg = new OpCode(OpCodeValues.Ldarg, 279317166);

		// Token: 0x04001709 RID: 5897
		public static readonly OpCode Ldarga = new OpCode(OpCodeValues.Ldarga, 279579310);

		// Token: 0x0400170A RID: 5898
		public static readonly OpCode Starg = new OpCode(OpCodeValues.Starg, -257680722);

		// Token: 0x0400170B RID: 5899
		public static readonly OpCode Ldloc = new OpCode(OpCodeValues.Ldloc, 279317166);

		// Token: 0x0400170C RID: 5900
		public static readonly OpCode Ldloca = new OpCode(OpCodeValues.Ldloca, 279579310);

		// Token: 0x0400170D RID: 5901
		public static readonly OpCode Stloc = new OpCode(OpCodeValues.Stloc, -257680722);

		// Token: 0x0400170E RID: 5902
		public static readonly OpCode Localloc = new OpCode(OpCodeValues.Localloc, 11156133);

		// Token: 0x0400170F RID: 5903
		public static readonly OpCode Endfilter = new OpCode(OpCodeValues.Endfilter, -240895259);

		// Token: 0x04001710 RID: 5904
		public static readonly OpCode Unaligned = new OpCode(OpCodeValues.Unaligned_, 10750096);

		// Token: 0x04001711 RID: 5905
		public static readonly OpCode Volatile = new OpCode(OpCodeValues.Volatile_, 10750085);

		// Token: 0x04001712 RID: 5906
		public static readonly OpCode Tailcall = new OpCode(OpCodeValues.Tail_, 10750085);

		// Token: 0x04001713 RID: 5907
		public static readonly OpCode Initobj = new OpCode(OpCodeValues.Initobj, -257673555);

		// Token: 0x04001714 RID: 5908
		public static readonly OpCode Constrained = new OpCode(OpCodeValues.Constrained_, 10750093);

		// Token: 0x04001715 RID: 5909
		public static readonly OpCode Cpblk = new OpCode(OpCodeValues.Cpblk, -794527067);

		// Token: 0x04001716 RID: 5910
		public static readonly OpCode Initblk = new OpCode(OpCodeValues.Initblk, -794527067);

		// Token: 0x04001717 RID: 5911
		public static readonly OpCode Rethrow = new OpCode(OpCodeValues.Rethrow, 27526917);

		// Token: 0x04001718 RID: 5912
		public static readonly OpCode Sizeof = new OpCode(OpCodeValues.Sizeof, 279579309);

		// Token: 0x04001719 RID: 5913
		public static readonly OpCode Refanytype = new OpCode(OpCodeValues.Refanytype, 11147941);

		// Token: 0x0400171A RID: 5914
		public static readonly OpCode Readonly = new OpCode(OpCodeValues.Readonly_, 10750085);
	}
}
