using System;

namespace System.Reflection.Emit
{
	// Token: 0x02000662 RID: 1634
	internal enum OpCodeValues
	{
		// Token: 0x04001557 RID: 5463
		Nop,
		// Token: 0x04001558 RID: 5464
		Break,
		// Token: 0x04001559 RID: 5465
		Ldarg_0,
		// Token: 0x0400155A RID: 5466
		Ldarg_1,
		// Token: 0x0400155B RID: 5467
		Ldarg_2,
		// Token: 0x0400155C RID: 5468
		Ldarg_3,
		// Token: 0x0400155D RID: 5469
		Ldloc_0,
		// Token: 0x0400155E RID: 5470
		Ldloc_1,
		// Token: 0x0400155F RID: 5471
		Ldloc_2,
		// Token: 0x04001560 RID: 5472
		Ldloc_3,
		// Token: 0x04001561 RID: 5473
		Stloc_0,
		// Token: 0x04001562 RID: 5474
		Stloc_1,
		// Token: 0x04001563 RID: 5475
		Stloc_2,
		// Token: 0x04001564 RID: 5476
		Stloc_3,
		// Token: 0x04001565 RID: 5477
		Ldarg_S,
		// Token: 0x04001566 RID: 5478
		Ldarga_S,
		// Token: 0x04001567 RID: 5479
		Starg_S,
		// Token: 0x04001568 RID: 5480
		Ldloc_S,
		// Token: 0x04001569 RID: 5481
		Ldloca_S,
		// Token: 0x0400156A RID: 5482
		Stloc_S,
		// Token: 0x0400156B RID: 5483
		Ldnull,
		// Token: 0x0400156C RID: 5484
		Ldc_I4_M1,
		// Token: 0x0400156D RID: 5485
		Ldc_I4_0,
		// Token: 0x0400156E RID: 5486
		Ldc_I4_1,
		// Token: 0x0400156F RID: 5487
		Ldc_I4_2,
		// Token: 0x04001570 RID: 5488
		Ldc_I4_3,
		// Token: 0x04001571 RID: 5489
		Ldc_I4_4,
		// Token: 0x04001572 RID: 5490
		Ldc_I4_5,
		// Token: 0x04001573 RID: 5491
		Ldc_I4_6,
		// Token: 0x04001574 RID: 5492
		Ldc_I4_7,
		// Token: 0x04001575 RID: 5493
		Ldc_I4_8,
		// Token: 0x04001576 RID: 5494
		Ldc_I4_S,
		// Token: 0x04001577 RID: 5495
		Ldc_I4,
		// Token: 0x04001578 RID: 5496
		Ldc_I8,
		// Token: 0x04001579 RID: 5497
		Ldc_R4,
		// Token: 0x0400157A RID: 5498
		Ldc_R8,
		// Token: 0x0400157B RID: 5499
		Dup = 37,
		// Token: 0x0400157C RID: 5500
		Pop,
		// Token: 0x0400157D RID: 5501
		Jmp,
		// Token: 0x0400157E RID: 5502
		Call,
		// Token: 0x0400157F RID: 5503
		Calli,
		// Token: 0x04001580 RID: 5504
		Ret,
		// Token: 0x04001581 RID: 5505
		Br_S,
		// Token: 0x04001582 RID: 5506
		Brfalse_S,
		// Token: 0x04001583 RID: 5507
		Brtrue_S,
		// Token: 0x04001584 RID: 5508
		Beq_S,
		// Token: 0x04001585 RID: 5509
		Bge_S,
		// Token: 0x04001586 RID: 5510
		Bgt_S,
		// Token: 0x04001587 RID: 5511
		Ble_S,
		// Token: 0x04001588 RID: 5512
		Blt_S,
		// Token: 0x04001589 RID: 5513
		Bne_Un_S,
		// Token: 0x0400158A RID: 5514
		Bge_Un_S,
		// Token: 0x0400158B RID: 5515
		Bgt_Un_S,
		// Token: 0x0400158C RID: 5516
		Ble_Un_S,
		// Token: 0x0400158D RID: 5517
		Blt_Un_S,
		// Token: 0x0400158E RID: 5518
		Br,
		// Token: 0x0400158F RID: 5519
		Brfalse,
		// Token: 0x04001590 RID: 5520
		Brtrue,
		// Token: 0x04001591 RID: 5521
		Beq,
		// Token: 0x04001592 RID: 5522
		Bge,
		// Token: 0x04001593 RID: 5523
		Bgt,
		// Token: 0x04001594 RID: 5524
		Ble,
		// Token: 0x04001595 RID: 5525
		Blt,
		// Token: 0x04001596 RID: 5526
		Bne_Un,
		// Token: 0x04001597 RID: 5527
		Bge_Un,
		// Token: 0x04001598 RID: 5528
		Bgt_Un,
		// Token: 0x04001599 RID: 5529
		Ble_Un,
		// Token: 0x0400159A RID: 5530
		Blt_Un,
		// Token: 0x0400159B RID: 5531
		Switch,
		// Token: 0x0400159C RID: 5532
		Ldind_I1,
		// Token: 0x0400159D RID: 5533
		Ldind_U1,
		// Token: 0x0400159E RID: 5534
		Ldind_I2,
		// Token: 0x0400159F RID: 5535
		Ldind_U2,
		// Token: 0x040015A0 RID: 5536
		Ldind_I4,
		// Token: 0x040015A1 RID: 5537
		Ldind_U4,
		// Token: 0x040015A2 RID: 5538
		Ldind_I8,
		// Token: 0x040015A3 RID: 5539
		Ldind_I,
		// Token: 0x040015A4 RID: 5540
		Ldind_R4,
		// Token: 0x040015A5 RID: 5541
		Ldind_R8,
		// Token: 0x040015A6 RID: 5542
		Ldind_Ref,
		// Token: 0x040015A7 RID: 5543
		Stind_Ref,
		// Token: 0x040015A8 RID: 5544
		Stind_I1,
		// Token: 0x040015A9 RID: 5545
		Stind_I2,
		// Token: 0x040015AA RID: 5546
		Stind_I4,
		// Token: 0x040015AB RID: 5547
		Stind_I8,
		// Token: 0x040015AC RID: 5548
		Stind_R4,
		// Token: 0x040015AD RID: 5549
		Stind_R8,
		// Token: 0x040015AE RID: 5550
		Add,
		// Token: 0x040015AF RID: 5551
		Sub,
		// Token: 0x040015B0 RID: 5552
		Mul,
		// Token: 0x040015B1 RID: 5553
		Div,
		// Token: 0x040015B2 RID: 5554
		Div_Un,
		// Token: 0x040015B3 RID: 5555
		Rem,
		// Token: 0x040015B4 RID: 5556
		Rem_Un,
		// Token: 0x040015B5 RID: 5557
		And,
		// Token: 0x040015B6 RID: 5558
		Or,
		// Token: 0x040015B7 RID: 5559
		Xor,
		// Token: 0x040015B8 RID: 5560
		Shl,
		// Token: 0x040015B9 RID: 5561
		Shr,
		// Token: 0x040015BA RID: 5562
		Shr_Un,
		// Token: 0x040015BB RID: 5563
		Neg,
		// Token: 0x040015BC RID: 5564
		Not,
		// Token: 0x040015BD RID: 5565
		Conv_I1,
		// Token: 0x040015BE RID: 5566
		Conv_I2,
		// Token: 0x040015BF RID: 5567
		Conv_I4,
		// Token: 0x040015C0 RID: 5568
		Conv_I8,
		// Token: 0x040015C1 RID: 5569
		Conv_R4,
		// Token: 0x040015C2 RID: 5570
		Conv_R8,
		// Token: 0x040015C3 RID: 5571
		Conv_U4,
		// Token: 0x040015C4 RID: 5572
		Conv_U8,
		// Token: 0x040015C5 RID: 5573
		Callvirt,
		// Token: 0x040015C6 RID: 5574
		Cpobj,
		// Token: 0x040015C7 RID: 5575
		Ldobj,
		// Token: 0x040015C8 RID: 5576
		Ldstr,
		// Token: 0x040015C9 RID: 5577
		Newobj,
		// Token: 0x040015CA RID: 5578
		Castclass,
		// Token: 0x040015CB RID: 5579
		Isinst,
		// Token: 0x040015CC RID: 5580
		Conv_R_Un,
		// Token: 0x040015CD RID: 5581
		Unbox = 121,
		// Token: 0x040015CE RID: 5582
		Throw,
		// Token: 0x040015CF RID: 5583
		Ldfld,
		// Token: 0x040015D0 RID: 5584
		Ldflda,
		// Token: 0x040015D1 RID: 5585
		Stfld,
		// Token: 0x040015D2 RID: 5586
		Ldsfld,
		// Token: 0x040015D3 RID: 5587
		Ldsflda,
		// Token: 0x040015D4 RID: 5588
		Stsfld,
		// Token: 0x040015D5 RID: 5589
		Stobj,
		// Token: 0x040015D6 RID: 5590
		Conv_Ovf_I1_Un,
		// Token: 0x040015D7 RID: 5591
		Conv_Ovf_I2_Un,
		// Token: 0x040015D8 RID: 5592
		Conv_Ovf_I4_Un,
		// Token: 0x040015D9 RID: 5593
		Conv_Ovf_I8_Un,
		// Token: 0x040015DA RID: 5594
		Conv_Ovf_U1_Un,
		// Token: 0x040015DB RID: 5595
		Conv_Ovf_U2_Un,
		// Token: 0x040015DC RID: 5596
		Conv_Ovf_U4_Un,
		// Token: 0x040015DD RID: 5597
		Conv_Ovf_U8_Un,
		// Token: 0x040015DE RID: 5598
		Conv_Ovf_I_Un,
		// Token: 0x040015DF RID: 5599
		Conv_Ovf_U_Un,
		// Token: 0x040015E0 RID: 5600
		Box,
		// Token: 0x040015E1 RID: 5601
		Newarr,
		// Token: 0x040015E2 RID: 5602
		Ldlen,
		// Token: 0x040015E3 RID: 5603
		Ldelema,
		// Token: 0x040015E4 RID: 5604
		Ldelem_I1,
		// Token: 0x040015E5 RID: 5605
		Ldelem_U1,
		// Token: 0x040015E6 RID: 5606
		Ldelem_I2,
		// Token: 0x040015E7 RID: 5607
		Ldelem_U2,
		// Token: 0x040015E8 RID: 5608
		Ldelem_I4,
		// Token: 0x040015E9 RID: 5609
		Ldelem_U4,
		// Token: 0x040015EA RID: 5610
		Ldelem_I8,
		// Token: 0x040015EB RID: 5611
		Ldelem_I,
		// Token: 0x040015EC RID: 5612
		Ldelem_R4,
		// Token: 0x040015ED RID: 5613
		Ldelem_R8,
		// Token: 0x040015EE RID: 5614
		Ldelem_Ref,
		// Token: 0x040015EF RID: 5615
		Stelem_I,
		// Token: 0x040015F0 RID: 5616
		Stelem_I1,
		// Token: 0x040015F1 RID: 5617
		Stelem_I2,
		// Token: 0x040015F2 RID: 5618
		Stelem_I4,
		// Token: 0x040015F3 RID: 5619
		Stelem_I8,
		// Token: 0x040015F4 RID: 5620
		Stelem_R4,
		// Token: 0x040015F5 RID: 5621
		Stelem_R8,
		// Token: 0x040015F6 RID: 5622
		Stelem_Ref,
		// Token: 0x040015F7 RID: 5623
		Ldelem,
		// Token: 0x040015F8 RID: 5624
		Stelem,
		// Token: 0x040015F9 RID: 5625
		Unbox_Any,
		// Token: 0x040015FA RID: 5626
		Conv_Ovf_I1 = 179,
		// Token: 0x040015FB RID: 5627
		Conv_Ovf_U1,
		// Token: 0x040015FC RID: 5628
		Conv_Ovf_I2,
		// Token: 0x040015FD RID: 5629
		Conv_Ovf_U2,
		// Token: 0x040015FE RID: 5630
		Conv_Ovf_I4,
		// Token: 0x040015FF RID: 5631
		Conv_Ovf_U4,
		// Token: 0x04001600 RID: 5632
		Conv_Ovf_I8,
		// Token: 0x04001601 RID: 5633
		Conv_Ovf_U8,
		// Token: 0x04001602 RID: 5634
		Refanyval = 194,
		// Token: 0x04001603 RID: 5635
		Ckfinite,
		// Token: 0x04001604 RID: 5636
		Mkrefany = 198,
		// Token: 0x04001605 RID: 5637
		Ldtoken = 208,
		// Token: 0x04001606 RID: 5638
		Conv_U2,
		// Token: 0x04001607 RID: 5639
		Conv_U1,
		// Token: 0x04001608 RID: 5640
		Conv_I,
		// Token: 0x04001609 RID: 5641
		Conv_Ovf_I,
		// Token: 0x0400160A RID: 5642
		Conv_Ovf_U,
		// Token: 0x0400160B RID: 5643
		Add_Ovf,
		// Token: 0x0400160C RID: 5644
		Add_Ovf_Un,
		// Token: 0x0400160D RID: 5645
		Mul_Ovf,
		// Token: 0x0400160E RID: 5646
		Mul_Ovf_Un,
		// Token: 0x0400160F RID: 5647
		Sub_Ovf,
		// Token: 0x04001610 RID: 5648
		Sub_Ovf_Un,
		// Token: 0x04001611 RID: 5649
		Endfinally,
		// Token: 0x04001612 RID: 5650
		Leave,
		// Token: 0x04001613 RID: 5651
		Leave_S,
		// Token: 0x04001614 RID: 5652
		Stind_I,
		// Token: 0x04001615 RID: 5653
		Conv_U,
		// Token: 0x04001616 RID: 5654
		Prefix7 = 248,
		// Token: 0x04001617 RID: 5655
		Prefix6,
		// Token: 0x04001618 RID: 5656
		Prefix5,
		// Token: 0x04001619 RID: 5657
		Prefix4,
		// Token: 0x0400161A RID: 5658
		Prefix3,
		// Token: 0x0400161B RID: 5659
		Prefix2,
		// Token: 0x0400161C RID: 5660
		Prefix1,
		// Token: 0x0400161D RID: 5661
		Prefixref,
		// Token: 0x0400161E RID: 5662
		Arglist = 65024,
		// Token: 0x0400161F RID: 5663
		Ceq,
		// Token: 0x04001620 RID: 5664
		Cgt,
		// Token: 0x04001621 RID: 5665
		Cgt_Un,
		// Token: 0x04001622 RID: 5666
		Clt,
		// Token: 0x04001623 RID: 5667
		Clt_Un,
		// Token: 0x04001624 RID: 5668
		Ldftn,
		// Token: 0x04001625 RID: 5669
		Ldvirtftn,
		// Token: 0x04001626 RID: 5670
		Ldarg = 65033,
		// Token: 0x04001627 RID: 5671
		Ldarga,
		// Token: 0x04001628 RID: 5672
		Starg,
		// Token: 0x04001629 RID: 5673
		Ldloc,
		// Token: 0x0400162A RID: 5674
		Ldloca,
		// Token: 0x0400162B RID: 5675
		Stloc,
		// Token: 0x0400162C RID: 5676
		Localloc,
		// Token: 0x0400162D RID: 5677
		Endfilter = 65041,
		// Token: 0x0400162E RID: 5678
		Unaligned_,
		// Token: 0x0400162F RID: 5679
		Volatile_,
		// Token: 0x04001630 RID: 5680
		Tail_,
		// Token: 0x04001631 RID: 5681
		Initobj,
		// Token: 0x04001632 RID: 5682
		Constrained_,
		// Token: 0x04001633 RID: 5683
		Cpblk,
		// Token: 0x04001634 RID: 5684
		Initblk,
		// Token: 0x04001635 RID: 5685
		Rethrow = 65050,
		// Token: 0x04001636 RID: 5686
		Sizeof = 65052,
		// Token: 0x04001637 RID: 5687
		Refanytype,
		// Token: 0x04001638 RID: 5688
		Readonly_
	}
}
