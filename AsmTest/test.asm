
;Test+Test.hadamardProduct(SliceMap`2<Int32,Double>, SliceMap`2<Int32,Decision>)
L0000: push r15
L0002: push r14
L0004: push r13
L0006: push r12
L0008: push rdi
L0009: push rsi
L000a: push rbp
L000b: push rbx
L000c: sub rsp, 0xd8
L0013: vzeroupper
L0016: vmovaps [rsp+0xc0], xmm6
L001f: vxorps xmm4, xmm4, xmm4
L0023: vmovdqa [rsp+0x70], xmm4
L0029: vmovdqa [rsp+0x80], xmm4
L0032: vmovdqa [rsp+0x90], xmm4
L003b: vmovdqa [rsp+0xa0], xmm4
L0044: mov rsi, rcx
L0047: mov rdi, rdx
L004a: lea rdx, [rsi+0x10]
L004e: mov rcx, [rdx]
L0051: mov ebx, [rdx+8]
L0054: mov ebp, [rdx+0xc]
L0057: xor r14d, r14d
L005a: xor r15d, r15d
L005d: test rcx, rcx
L0060: je short L00b9
L0062: mov rdx, [rcx]
L0065: test dword ptr [rdx], 0x80000000
L006b: je short L0077
L006d: lea r14, [rcx+0x10]
L0071: mov r15d, [rcx+8]
L0075: jmp short L0099
L0077: lea rdx, [rsp+0xa0]
L007f: mov rax, [rcx]
L0082: mov rax, [rax+0x40]
L0086: call qword ptr [rax+0x28]
L0089: mov r14, [rsp+0xa0]
L0091: mov r15d, [rsp+0xa8]
L0099: and ebx, 0x7fffffff
L009f: mov edx, ebx
L00a1: mov ecx, ebp
L00a3: add rcx, rdx
L00a6: mov eax, r15d
L00a9: cmp rcx, rax
L00ac: ja L051a
L00b2: lea r14, [r14+rdx*4]
L00b6: mov r15d, ebp
L00b9: lea rdx, [rsi+0x20]
L00bd: mov rcx, [rdx]
L00c0: mov ebx, [rdx+8]
L00c3: mov ebp, [rdx+0xc]
L00c6: xor r12d, r12d
L00c9: xor r13d, r13d
L00cc: test rcx, rcx
L00cf: je short L0128
L00d1: mov rdx, [rcx]
L00d4: test dword ptr [rdx], 0x80000000
L00da: je short L00e6
L00dc: lea r12, [rcx+0x10]
L00e0: mov r13d, [rcx+8]
L00e4: jmp short L0108
L00e6: lea rdx, [rsp+0x90]
L00ee: mov rax, [rcx]
L00f1: mov rax, [rax+0x40]
L00f5: call qword ptr [rax+0x28]
L00f8: mov r12, [rsp+0x90]
L0100: mov r13d, [rsp+0x98]
L0108: and ebx, 0x7fffffff
L010e: mov edx, ebx
L0110: mov ecx, ebp
L0112: add rcx, rdx
L0115: mov eax, r13d
L0118: cmp rcx, rax
L011b: ja L051a
L0121: lea r12, [r12+rdx*8]
L0125: mov r13d, ebp
L0128: lea rdx, [rdi+0x10]
L012c: mov rcx, [rdx]
L012f: mov ebx, [rdx+8]
L0132: mov ebp, [rdx+0xc]
L0135: xor eax, eax
L0137: xor r8d, r8d
L013a: test rcx, rcx
L013d: je short L0199
L013f: mov rax, [rcx]
L0142: test dword ptr [rax], 0x80000000
L0148: je short L0154
L014a: lea rax, [rcx+0x10]
L014e: mov r8d, [rcx+8]
L0152: jmp short L0176
L0154: lea rdx, [rsp+0x80]
L015c: mov rax, [rcx]
L015f: mov rax, [rax+0x40]
L0163: call qword ptr [rax+0x28]
L0166: mov rax, [rsp+0x80]
L016e: mov r8d, [rsp+0x88]
L0176: mov edx, ebx
L0178: and edx, 0x7fffffff
L017e: mov ecx, ebp
L0180: mov r9d, ecx
L0183: add r9, rdx
L0186: mov r8d, r8d
L0189: cmp r9, r8
L018c: ja L051a
L0192: lea rax, [rax+rdx*4]
L0196: mov r8d, ecx
L0199: mov rbx, rax
L019c: mov ebp, r8d
L019f: lea rdx, [rdi+0x20]
L01a3: mov rcx, [rdx]
L01a6: mov eax, [rdx+8]
L01a9: mov [rsp+0x64], eax
L01ad: mov r8d, [rdx+0xc]
L01b1: mov [rsp+0x60], r8d
L01b6: xor r9d, r9d
L01b9: xor r10d, r10d
L01bc: test rcx, rcx
L01bf: je short L021d
L01c1: mov r9, [rcx]
L01c4: test dword ptr [r9], 0x80000000
L01cb: je short L01d7
L01cd: lea r9, [rcx+0x10]
L01d1: mov r10d, [rcx+8]
L01d5: jmp short L01f1
L01d7: lea rdx, [rsp+0x70]
L01dc: mov r9, [rcx]
L01df: mov r9, [r9+0x40]
L01e3: call qword ptr [r9+0x28]
L01e7: mov r9, [rsp+0x70]
L01ec: mov r10d, [rsp+0x78]
L01f1: mov eax, [rsp+0x64]
L01f5: and eax, 0x7fffffff
L01fa: mov ecx, eax
L01fc: mov r8d, [rsp+0x60]
L0201: mov eax, r8d
L0204: add rax, rcx
L0207: mov r10d, r10d
L020a: cmp rax, r10
L020d: ja L051a
L0213: shl rcx, 4
L0217: add r9, rcx
L021a: mov r10d, r8d
L021d: mov rax, r9
L0220: mov edx, r10d
L0223: mov [rsp+0x40], rax
L0228: mov [rsp+0x68], edx
L022c: lea rcx, [rsi+0x10]
L0230: cmp [rcx], ecx
L0232: mov r8d, [rcx+8]
L0236: mov ecx, [rcx+0xc]
L0239: mov r8d, ecx
L023c: mov ecx, r8d
L023f: call Microsoft.FSharp.Collections.ArrayModule.ZeroCreate[[System.Int32, System.Private.CoreLib]](Int32)
L0244: mov [rsp+0x58], rax
L0249: add rdi, 0x10
L024d: cmp [rdi], edi
L024f: mov edx, [rdi+8]
L0252: mov r8d, [rdi+0xc]
L0256: mov edi, r8d
L0259: mov edx, edi
L025b: mov rcx, 0x7ff953af4790
L0265: call Microsoft.FSharp.Collections.ArrayModule.ZeroCreate[[System.__Canon, System.Private.CoreLib]](Int32)
L026a: mov rdi, rax
L026d: xor eax, eax
L026f: mov [rsp+0xbc], eax
L0276: xor r9d, r9d
L0279: xor r10d, r10d
L027c: cmp r9d, r15d
L027f: jge L0449
L0285: mov [rsp+0x6c], ebp
L0289: cmp r10d, ebp
L028c: setl cl
L028f: movzx ecx, cl
L0292: test ecx, ecx
L0294: je L0449
L029a: movsxd rcx, r9d
L029d: lea r8, [r14+rcx*4]
L02a1: mov r11d, [r8]
L02a4: mov [rsp+0xb8], r9d
L02ac: cmp r9d, r13d
L02af: jae L0520
L02b5: lea r8, [r12+rcx*8]
L02b9: vmovsd xmm6, [r8]
L02be: movsxd rcx, r10d
L02c1: mov [rsp+0x48], rbx
L02c6: lea r8, [rbx+rcx*4]
L02ca: mov r8d, [r8]
L02cd: mov [rsp+0xb4], r10d
L02d5: cmp r10d, [rsp+0x68]
L02da: jae L0520
L02e0: shl rcx, 4
L02e4: add rcx, [rsp+0x40]
L02e9: mov rbp, [rcx]
L02ec: mov rcx, [rcx+8]
L02f0: mov [rsp+0x38], rcx
L02f5: mov rcx, [rsi+8]
L02f9: mov [rsp+0xb0], r11d
L0301: mov edx, r11d
L0304: mov r11, 0x7ff953a27020
L030e: mov rbx, 0x7ff953a27020
L0318: call qword ptr [rbx]
L031a: test eax, eax
L031c: jne L0402
L0322: mov ebx, [rsp+0xbc]
L0329: mov rdx, [rsp+0x58]
L032e: cmp ebx, [rdx+8]
L0331: jae L0520
L0337: movsxd rcx, ebx
L033a: mov [rsp+0x58], rdx
L033f: mov r11d, [rsp+0xb0]
L0347: mov [rdx+rcx*4+0x10], r11d
L034c: mov rax, [rsp+0x38]
L0351: mov [rsp+0x30], rax
L0356: mov rcx, 0x7ff953af1178
L0360: call 0x00007ff9b182a470
L0365: mov rdx, rbp
L0368: mov rbp, [rsp+0x30]
L036d: mov dword ptr [rax+8], 1
L0374: mov [rsp+0x50], rax
L0379: lea r8, [rax+0x10]
L037d: mov rcx, r8
L0380: mov [rsp+0x28], rcx
L0385: call 0x00007ff9b182a050
L038a: mov rcx, [rsp+0x28]
L038f: lea rcx, [rcx+8]
L0393: mov rdx, rbp
L0396: call 0x00007ff9b182a050
L039b: mov rcx, 0x7ff953af1320
L03a5: call 0x00007ff9b182a470
L03aa: mov rbp, rax
L03ad: mov dword ptr [rbp+8], 2
L03b4: vmovsd [rbp+0x18], xmm6
L03b9: lea rcx, [rbp+0x10]
L03bd: mov rdx, [rsp+0x50]
L03c2: call 0x00007ff9b182a080
L03c7: mov rcx, rdi
L03ca: mov edx, ebx
L03cc: mov r8, rbp
L03cf: call System.Runtime.CompilerServices.CastHelpers.StelemRef(System.Array, Int32, System.Object)
L03d4: inc ebx
L03d6: mov ebp, [rsp+0xb8]
L03dd: inc ebp
L03df: mov r10d, [rsp+0xb4]
L03e7: inc r10d
L03ea: mov [rsp+0xbc], ebx
L03f1: mov r9d, ebp
L03f4: mov rbx, [rsp+0x48]
L03f9: mov ebp, [rsp+0x6c]
L03fd: jmp L027c
L0402: test eax, eax
L0404: jge short L0428
L0406: mov ebp, [rsp+0xb8]
L040d: inc ebp
L040f: mov r9d, ebp
L0412: mov rbx, [rsp+0x48]
L0417: mov ebp, [rsp+0x6c]
L041b: mov r10d, [rsp+0xb4]
L0423: jmp L027c
L0428: mov r10d, [rsp+0xb4]
L0430: inc r10d
L0433: mov rbx, [rsp+0x48]
L0438: mov ebp, [rsp+0x6c]
L043c: mov r9d, [rsp+0xb8]
L0444: jmp L027c
L0449: mov rsi, [rsi+8]
L044d: mov rbx, [rsp+0x58]
L0452: test rbx, rbx
L0455: jne short L046d
L0457: mov eax, [rsp+0xbc]
L045e: test eax, eax
L0460: jne L051a
L0466: xor ebp, ebp
L0468: xor r14d, r14d
L046b: jmp short L0488
L046d: mov ecx, [rbx+8]
L0470: mov eax, [rsp+0xbc]
L0477: mov edx, eax
L0479: cmp rcx, rdx
L047c: jb L051a
L0482: mov rbp, rbx
L0485: mov r14d, eax
L0488: test rdi, rdi
L048b: jne short L049c
L048d: test eax, eax
L048f: jne L051a
L0495: xor ebx, ebx
L0497: xor r15d, r15d
L049a: jmp short L04ac
L049c: mov ecx, [rdi+8]
L049f: mov edx, eax
L04a1: cmp rcx, rdx
L04a4: jb short L051a
L04a6: mov rbx, rdi
L04a9: mov r15d, eax
L04ac: mov rcx, 0x7ff953af35d8
L04b6: call 0x00007ff9b182a470
L04bb: mov rdi, rax
L04be: lea rcx, [rdi+8]
L04c2: mov rdx, rsi
L04c5: call 0x00007ff9b182a080
L04ca: lea rsi, [rdi+0x10]
L04ce: mov rcx, rsi
L04d1: mov rdx, rbp
L04d4: call 0x00007ff9b182a050
L04d9: xor edx, edx
L04db: mov [rsi+8], edx
L04de: mov [rsi+0xc], r14d
L04e2: lea rsi, [rdi+0x20]
L04e6: mov rcx, rsi
L04e9: mov rdx, rbx
L04ec: call 0x00007ff9b182a050
L04f1: xor eax, eax
L04f3: mov [rsi+8], eax
L04f6: mov [rsi+0xc], r15d
L04fa: mov rax, rdi
L04fd: vmovaps xmm6, [rsp+0xc0]
L0506: add rsp, 0xd8
L050d: pop rbx
L050e: pop rbp
L050f: pop rsi
L0510: pop rdi
L0511: pop r12
L0513: pop r13
L0515: pop r14
L0517: pop r15
L0519: ret
L051a: call System.ThrowHelper.ThrowArgumentOutOfRangeException()
L051f: int3
L0520: call 0x00007ff9b196a370
L0525: int3

;Test+Test.hadamardProduct(SliceMap`2<Int32,Double>, SliceMap`2<Int32,Decision>)
L0000: push r15
L0002: push r14
L0004: push r13
L0006: push r12
L0008: push rdi
L0009: push rsi
L000a: push rbp
L000b: push rbx
L000c: sub rsp, 0xd8
L0013: vzeroupper
L0016: vmovaps [rsp+0xc0], xmm6
L001f: vxorps xmm4, xmm4, xmm4
L0023: vmovdqa [rsp+0x70], xmm4
L0029: vmovdqa [rsp+0x80], xmm4
L0032: vmovdqa [rsp+0x90], xmm4
L003b: vmovdqa [rsp+0xa0], xmm4
L0044: mov rsi, rcx
L0047: mov rdi, rdx
L004a: lea rdx, [rsi+0x10]
L004e: mov rcx, [rdx]
L0051: mov ebx, [rdx+8]
L0054: mov ebp, [rdx+0xc]
L0057: xor r14d, r14d
L005a: xor r15d, r15d
L005d: test rcx, rcx
L0060: je short L00b9
L0062: mov rdx, [rcx]
L0065: test dword ptr [rdx], 0x80000000
L006b: je short L0077
L006d: lea r14, [rcx+0x10]
L0071: mov r15d, [rcx+8]
L0075: jmp short L0099
L0077: lea rdx, [rsp+0xa0]
L007f: mov rax, [rcx]
L0082: mov rax, [rax+0x40]
L0086: call qword ptr [rax+0x28]
L0089: mov r14, [rsp+0xa0]
L0091: mov r15d, [rsp+0xa8]
L0099: and ebx, 0x7fffffff
L009f: mov edx, ebx
L00a1: mov ecx, ebp
L00a3: add rcx, rdx
L00a6: mov eax, r15d
L00a9: cmp rcx, rax
L00ac: ja L051a
L00b2: lea r14, [r14+rdx*4]
L00b6: mov r15d, ebp
L00b9: lea rdx, [rsi+0x20]
L00bd: mov rcx, [rdx]
L00c0: mov ebx, [rdx+8]
L00c3: mov ebp, [rdx+0xc]
L00c6: xor r12d, r12d
L00c9: xor r13d, r13d
L00cc: test rcx, rcx
L00cf: je short L0128
L00d1: mov rdx, [rcx]
L00d4: test dword ptr [rdx], 0x80000000
L00da: je short L00e6
L00dc: lea r12, [rcx+0x10]
L00e0: mov r13d, [rcx+8]
L00e4: jmp short L0108
L00e6: lea rdx, [rsp+0x90]
L00ee: mov rax, [rcx]
L00f1: mov rax, [rax+0x40]
L00f5: call qword ptr [rax+0x28]
L00f8: mov r12, [rsp+0x90]
L0100: mov r13d, [rsp+0x98]
L0108: and ebx, 0x7fffffff
L010e: mov edx, ebx
L0110: mov ecx, ebp
L0112: add rcx, rdx
L0115: mov eax, r13d
L0118: cmp rcx, rax
L011b: ja L051a
L0121: lea r12, [r12+rdx*8]
L0125: mov r13d, ebp
L0128: lea rdx, [rdi+0x10]
L012c: mov rcx, [rdx]
L012f: mov ebx, [rdx+8]
L0132: mov ebp, [rdx+0xc]
L0135: xor eax, eax
L0137: xor r8d, r8d
L013a: test rcx, rcx
L013d: je short L0199
L013f: mov rax, [rcx]
L0142: test dword ptr [rax], 0x80000000
L0148: je short L0154
L014a: lea rax, [rcx+0x10]
L014e: mov r8d, [rcx+8]
L0152: jmp short L0176
L0154: lea rdx, [rsp+0x80]
L015c: mov rax, [rcx]
L015f: mov rax, [rax+0x40]
L0163: call qword ptr [rax+0x28]
L0166: mov rax, [rsp+0x80]
L016e: mov r8d, [rsp+0x88]
L0176: mov edx, ebx
L0178: and edx, 0x7fffffff
L017e: mov ecx, ebp
L0180: mov r9d, ecx
L0183: add r9, rdx
L0186: mov r8d, r8d
L0189: cmp r9, r8
L018c: ja L051a
L0192: lea rax, [rax+rdx*4]
L0196: mov r8d, ecx
L0199: mov rbx, rax
L019c: mov ebp, r8d
L019f: lea rdx, [rdi+0x20]
L01a3: mov rcx, [rdx]
L01a6: mov eax, [rdx+8]
L01a9: mov [rsp+0x64], eax
L01ad: mov r8d, [rdx+0xc]
L01b1: mov [rsp+0x60], r8d
L01b6: xor r9d, r9d
L01b9: xor r10d, r10d
L01bc: test rcx, rcx
L01bf: je short L021d
L01c1: mov r9, [rcx]
L01c4: test dword ptr [r9], 0x80000000
L01cb: je short L01d7
L01cd: lea r9, [rcx+0x10]
L01d1: mov r10d, [rcx+8]
L01d5: jmp short L01f1
L01d7: lea rdx, [rsp+0x70]
L01dc: mov r9, [rcx]
L01df: mov r9, [r9+0x40]
L01e3: call qword ptr [r9+0x28]
L01e7: mov r9, [rsp+0x70]
L01ec: mov r10d, [rsp+0x78]
L01f1: mov eax, [rsp+0x64]
L01f5: and eax, 0x7fffffff
L01fa: mov ecx, eax
L01fc: mov r8d, [rsp+0x60]
L0201: mov eax, r8d
L0204: add rax, rcx
L0207: mov r10d, r10d
L020a: cmp rax, r10
L020d: ja L051a
L0213: shl rcx, 4
L0217: add r9, rcx
L021a: mov r10d, r8d
L021d: mov rax, r9
L0220: mov edx, r10d
L0223: mov [rsp+0x40], rax
L0228: mov [rsp+0x68], edx
L022c: lea rcx, [rsi+0x10]
L0230: cmp [rcx], ecx
L0232: mov r8d, [rcx+8]
L0236: mov ecx, [rcx+0xc]
L0239: mov r8d, ecx
L023c: mov ecx, r8d
L023f: call Microsoft.FSharp.Collections.ArrayModule.ZeroCreate[[System.Int32, System.Private.CoreLib]](Int32)
L0244: mov [rsp+0x58], rax
L0249: add rdi, 0x10
L024d: cmp [rdi], edi
L024f: mov edx, [rdi+8]
L0252: mov r8d, [rdi+0xc]
L0256: mov edi, r8d
L0259: mov edx, edi
L025b: mov rcx, 0x7ff953af4790
L0265: call Microsoft.FSharp.Collections.ArrayModule.ZeroCreate[[System.__Canon, System.Private.CoreLib]](Int32)
L026a: mov rdi, rax
L026d: xor eax, eax
L026f: mov [rsp+0xbc], eax
L0276: xor r9d, r9d
L0279: xor r10d, r10d
L027c: cmp r9d, r15d
L027f: jge L0449
L0285: mov [rsp+0x6c], ebp
L0289: cmp r10d, ebp
L028c: setl cl
L028f: movzx ecx, cl
L0292: test ecx, ecx
L0294: je L0449
L029a: movsxd rcx, r9d
L029d: lea r8, [r14+rcx*4]
L02a1: mov r11d, [r8]
L02a4: mov [rsp+0xb8], r9d
L02ac: cmp r9d, r13d
L02af: jae L0520
L02b5: lea r8, [r12+rcx*8]
L02b9: vmovsd xmm6, [r8]
L02be: movsxd rcx, r10d
L02c1: mov [rsp+0x48], rbx
L02c6: lea r8, [rbx+rcx*4]
L02ca: mov r8d, [r8]
L02cd: mov [rsp+0xb4], r10d
L02d5: cmp r10d, [rsp+0x68]
L02da: jae L0520
L02e0: shl rcx, 4
L02e4: add rcx, [rsp+0x40]
L02e9: mov rbp, [rcx]
L02ec: mov rcx, [rcx+8]
L02f0: mov [rsp+0x38], rcx
L02f5: mov rcx, [rsi+8]
L02f9: mov [rsp+0xb0], r11d
L0301: mov edx, r11d
L0304: mov r11, 0x7ff953a27020
L030e: mov rbx, 0x7ff953a27020
L0318: call qword ptr [rbx]
L031a: test eax, eax
L031c: jne L0402
L0322: mov ebx, [rsp+0xbc]
L0329: mov rdx, [rsp+0x58]
L032e: cmp ebx, [rdx+8]
L0331: jae L0520
L0337: movsxd rcx, ebx
L033a: mov [rsp+0x58], rdx
L033f: mov r11d, [rsp+0xb0]
L0347: mov [rdx+rcx*4+0x10], r11d
L034c: mov rax, [rsp+0x38]
L0351: mov [rsp+0x30], rax
L0356: mov rcx, 0x7ff953af1178
L0360: call 0x00007ff9b182a470
L0365: mov rdx, rbp
L0368: mov rbp, [rsp+0x30]
L036d: mov dword ptr [rax+8], 1
L0374: mov [rsp+0x50], rax
L0379: lea r8, [rax+0x10]
L037d: mov rcx, r8
L0380: mov [rsp+0x28], rcx
L0385: call 0x00007ff9b182a050
L038a: mov rcx, [rsp+0x28]
L038f: lea rcx, [rcx+8]
L0393: mov rdx, rbp
L0396: call 0x00007ff9b182a050
L039b: mov rcx, 0x7ff953af1320
L03a5: call 0x00007ff9b182a470
L03aa: mov rbp, rax
L03ad: mov dword ptr [rbp+8], 2
L03b4: vmovsd [rbp+0x18], xmm6
L03b9: lea rcx, [rbp+0x10]
L03bd: mov rdx, [rsp+0x50]
L03c2: call 0x00007ff9b182a080
L03c7: mov rcx, rdi
L03ca: mov edx, ebx
L03cc: mov r8, rbp
L03cf: call System.Runtime.CompilerServices.CastHelpers.StelemRef(System.Array, Int32, System.Object)
L03d4: inc ebx
L03d6: mov ebp, [rsp+0xb8]
L03dd: inc ebp
L03df: mov r10d, [rsp+0xb4]
L03e7: inc r10d
L03ea: mov [rsp+0xbc], ebx
L03f1: mov r9d, ebp
L03f4: mov rbx, [rsp+0x48]
L03f9: mov ebp, [rsp+0x6c]
L03fd: jmp L027c
L0402: test eax, eax
L0404: jge short L0428
L0406: mov ebp, [rsp+0xb8]
L040d: inc ebp
L040f: mov r9d, ebp
L0412: mov rbx, [rsp+0x48]
L0417: mov ebp, [rsp+0x6c]
L041b: mov r10d, [rsp+0xb4]
L0423: jmp L027c
L0428: mov r10d, [rsp+0xb4]
L0430: inc r10d
L0433: mov rbx, [rsp+0x48]
L0438: mov ebp, [rsp+0x6c]
L043c: mov r9d, [rsp+0xb8]
L0444: jmp L027c
L0449: mov rsi, [rsi+8]
L044d: mov rbx, [rsp+0x58]
L0452: test rbx, rbx
L0455: jne short L046d
L0457: mov eax, [rsp+0xbc]
L045e: test eax, eax
L0460: jne L051a
L0466: xor ebp, ebp
L0468: xor r14d, r14d
L046b: jmp short L0488
L046d: mov ecx, [rbx+8]
L0470: mov eax, [rsp+0xbc]
L0477: mov edx, eax
L0479: cmp rcx, rdx
L047c: jb L051a
L0482: mov rbp, rbx
L0485: mov r14d, eax
L0488: test rdi, rdi
L048b: jne short L049c
L048d: test eax, eax
L048f: jne L051a
L0495: xor ebx, ebx
L0497: xor r15d, r15d
L049a: jmp short L04ac
L049c: mov ecx, [rdi+8]
L049f: mov edx, eax
L04a1: cmp rcx, rdx
L04a4: jb short L051a
L04a6: mov rbx, rdi
L04a9: mov r15d, eax
L04ac: mov rcx, 0x7ff953af35d8
L04b6: call 0x00007ff9b182a470
L04bb: mov rdi, rax
L04be: lea rcx, [rdi+8]
L04c2: mov rdx, rsi
L04c5: call 0x00007ff9b182a080
L04ca: lea rsi, [rdi+0x10]
L04ce: mov rcx, rsi
L04d1: mov rdx, rbp
L04d4: call 0x00007ff9b182a050
L04d9: xor edx, edx
L04db: mov [rsi+8], edx
L04de: mov [rsi+0xc], r14d
L04e2: lea rsi, [rdi+0x20]
L04e6: mov rcx, rsi
L04e9: mov rdx, rbx
L04ec: call 0x00007ff9b182a050
L04f1: xor eax, eax
L04f3: mov [rsi+8], eax
L04f6: mov [rsi+0xc], r15d
L04fa: mov rax, rdi
L04fd: vmovaps xmm6, [rsp+0xc0]
L0506: add rsp, 0xd8
L050d: pop rbx
L050e: pop rbp
L050f: pop rsi
L0510: pop rdi
L0511: pop r12
L0513: pop r13
L0515: pop r14
L0517: pop r15
L0519: ret
L051a: call System.ThrowHelper.ThrowArgumentOutOfRangeException()
L051f: int3
L0520: call 0x00007ff9b196a370
L0525: int3

;Test+Test+DecisionType.get_Boolean()
L0000: sub rsp, 0x28
L0004: mov rcx, 0x7ff953a2d838
L000e: xor edx, edx
L0010: call 0x00007ff9b1968990
L0015: mov rax, [rax]
L0018: add rsp, 0x28
L001c: ret

;Test+Test+DecisionType.get_IsBoolean()
L0000: sub rsp, 0x28
L0004: mov rdx, rcx
L0007: mov rcx, 0x7ff953a2e558
L0011: call System.Runtime.CompilerServices.CastHelpers.IsInstanceOfClass(Void*, System.Object)
L0016: test rax, rax
L0019: setne al
L001c: movzx eax, al
L001f: add rsp, 0x28
L0023: ret

;Test+Test+DecisionType.NewInteger(Double, Double)
L0000: sub rsp, 0x28
L0004: vzeroupper
L0007: vmovsd [rsp+0x30], xmm0
L000d: vmovsd [rsp+0x38], xmm1
L0013: mov rcx, 0x7ff953a2e700
L001d: call 0x00007ff9b182a470
L0022: vmovsd xmm0, [rsp+0x30]
L0028: vmovsd [rax+8], xmm0
L002d: vmovsd xmm1, [rsp+0x38]
L0033: vmovsd [rax+0x10], xmm1
L0038: add rsp, 0x28
L003c: ret

;Test+Test+DecisionType.get_IsInteger()
L0000: sub rsp, 0x28
L0004: mov rdx, rcx
L0007: mov rcx, 0x7ff953a2e700
L0011: call System.Runtime.CompilerServices.CastHelpers.IsInstanceOfClass(Void*, System.Object)
L0016: test rax, rax
L0019: setne al
L001c: movzx eax, al
L001f: add rsp, 0x28
L0023: ret

;Test+Test+DecisionType.NewContinuous(Double, Double)
L0000: sub rsp, 0x28
L0004: vzeroupper
L0007: vmovsd [rsp+0x30], xmm0
L000d: vmovsd [rsp+0x38], xmm1
L0013: mov rcx, 0x7ff953a2e8a8
L001d: call 0x00007ff9b182a470
L0022: vmovsd xmm0, [rsp+0x30]
L0028: vmovsd [rax+8], xmm0
L002d: vmovsd xmm1, [rsp+0x38]
L0033: vmovsd [rax+0x10], xmm1
L0038: add rsp, 0x28
L003c: ret

;Test+Test+DecisionType.get_IsContinuous()
L0000: sub rsp, 0x28
L0004: mov rdx, rcx
L0007: mov rcx, 0x7ff953a2e8a8
L0011: call System.Runtime.CompilerServices.CastHelpers.IsInstanceOfClass(Void*, System.Object)
L0016: test rax, rax
L0019: setne al
L001c: movzx eax, al
L001f: add rsp, 0x28
L0023: ret

;Test+Test+DecisionType.get_Tag()
L0000: push rsi
L0001: sub rsp, 0x20
L0005: mov rsi, rcx
L0008: mov rdx, rsi
L000b: mov rcx, 0x7ff953a2e8a8
L0015: call System.Runtime.CompilerServices.CastHelpers.IsInstanceOfClass(Void*, System.Object)
L001a: test rax, rax
L001d: je short L0026
L001f: mov eax, 2
L0024: jmp short L0046
L0026: mov rdx, rsi
L0029: mov rcx, 0x7ff953a2e700
L0033: call System.Runtime.CompilerServices.CastHelpers.IsInstanceOfClass(Void*, System.Object)
L0038: test rax, rax
L003b: je short L0044
L003d: mov eax, 1
L0042: jmp short L0046
L0044: xor eax, eax
L0046: add rsp, 0x20
L004a: pop rsi
L004b: ret

;Test+Test+DecisionType.ToString()
L0000: push rdi
L0001: push rsi
L0002: sub rsp, 0x28
L0006: mov rsi, rcx
L0009: mov rcx, 0x7ff953af5d58
L0013: call 0x00007ff9b182a470
L0018: mov rdi, rax
L001b: mov rdx, 0x191f2a9b3f0
L0025: mov rdx, [rdx]
L0028: lea rcx, [rdi+8]
L002c: call 0x00007ff9b182a080
L0031: xor edx, edx
L0033: mov [rdi+0x10], rdx
L0037: mov [rdi+0x18], rdx
L003b: mov rdx, rdi
L003e: mov rcx, 0x7ff953af6038
L0048: call Microsoft.FSharp.Core.PrintfModule.PrintFormatToStringThen[[System.__Canon, System.Private.CoreLib]](Microsoft.FSharp.Core.PrintfFormat`4<System.__Canon,Microsoft.FSharp.Core.Unit,System.String,System.String>)
L004d: mov rcx, rax
L0050: mov rdx, rsi
L0053: mov rax, [rax]
L0056: mov rax, [rax+0x40]
L005a: mov rax, [rax+0x20]
L005e: add rsp, 0x28
L0062: pop rsi
L0063: pop rdi
L0064: jmp rax

;Test+Test+DecisionType.CompareTo(DecisionType)
L0000: push rdi
L0001: push rsi
L0002: push rbp
L0003: push rbx
L0004: sub rsp, 0x28
L0008: mov rdi, rcx
L000b: mov rsi, rdx
L000e: test rdi, rdi
L0011: je L00ca
L0017: test rsi, rsi
L001a: je L00bc
L0020: mov rdx, rdi
L0023: mov rcx, 0x7ff953a2e8a8
L002d: call System.Runtime.CompilerServices.CastHelpers.IsInstanceOfClass(Void*, System.Object)
L0032: test rax, rax
L0035: je short L003e
L0037: mov ebx, 2
L003c: jmp short L005e
L003e: mov rdx, rdi
L0041: mov rcx, 0x7ff953a2e700
L004b: call System.Runtime.CompilerServices.CastHelpers.IsInstanceOfClass(Void*, System.Object)
L0050: test rax, rax
L0053: je short L005c
L0055: mov ebx, 1
L005a: jmp short L005e
L005c: xor ebx, ebx
L005e: mov rdx, rsi
L0061: mov rcx, 0x7ff953a2e8a8
L006b: call System.Runtime.CompilerServices.CastHelpers.IsInstanceOfClass(Void*, System.Object)
L0070: test rax, rax
L0073: je short L007c
L0075: mov ebp, 2
L007a: jmp short L009c
L007c: mov rdx, rsi
L007f: mov rcx, 0x7ff953a2e700
L0089: call System.Runtime.CompilerServices.CastHelpers.IsInstanceOfClass(Void*, System.Object)
L008e: test rax, rax
L0091: je short L009a
L0093: mov ebp, 1
L0098: jmp short L009c
L009a: xor ebp, ebp
L009c: cmp ebx, ebp
L009e: jne short L00b6
L00a0: mov rcx, rdi
L00a3: mov rdx, rsi
L00a6: xor r8d, r8d
L00a9: add rsp, 0x28
L00ad: pop rbx
L00ae: pop rbp
L00af: pop rsi
L00b0: pop rdi
L00b1: jmp Test+Test.CompareTo$cont@6(DecisionType, DecisionType, Microsoft.FSharp.Core.Unit)
L00b6: mov eax, ebx
L00b8: sub eax, ebp
L00ba: jmp short L00e8
L00bc: mov eax, 1
L00c1: add rsp, 0x28
L00c5: pop rbx
L00c6: pop rbp
L00c7: pop rsi
L00c8: pop rdi
L00c9: ret
L00ca: test rsi, rsi
L00cd: je short L00dd
L00cf: mov eax, 0xffffffff
L00d4: add rsp, 0x28
L00d8: pop rbx
L00d9: pop rbp
L00da: pop rsi
L00db: pop rdi
L00dc: ret
L00dd: xor eax, eax
L00df: add rsp, 0x28
L00e3: pop rbx
L00e4: pop rbp
L00e5: pop rsi
L00e6: pop rdi
L00e7: ret
L00e8: add rsp, 0x28
L00ec: pop rbx
L00ed: pop rbp
L00ee: pop rsi
L00ef: pop rdi
L00f0: ret

;Test+Test+DecisionType.CompareTo(System.Object)
L0000: push rsi
L0001: sub rsp, 0x20
L0005: mov rsi, rcx
L0008: mov rax, rdx
L000b: test rax, rax
L000e: je short L0024
L0010: mov rcx, 0x7ff953a2e0b8
L001a: cmp [rax], rcx
L001d: je short L0024
L001f: call System.Runtime.CompilerServices.CastHelpers.ChkCastClassSpecial(Void*, System.Object)
L0024: mov rcx, rsi
L0027: mov rdx, rax
L002a: cmp [rcx], ecx
L002c: add rsp, 0x20
L0030: pop rsi
L0031: jmp Test+Test+DecisionType.CompareTo(DecisionType)

;Test+Test+DecisionType.CompareTo(System.Object, System.Collections.IComparer)
L0000: push r14
L0002: push rdi
L0003: push rsi
L0004: push rbp
L0005: push rbx
L0006: sub rsp, 0x20
L000a: mov rdi, rcx
L000d: mov rsi, rdx
L0010: mov rbx, r8
L0013: mov rcx, rsi
L0016: test rcx, rcx
L0019: je short L0035
L001b: mov rdx, 0x7ff953a2e0b8
L0025: cmp [rcx], rdx
L0028: je short L0035
L002a: mov rcx, rdx
L002d: mov rdx, rsi
L0030: call System.Runtime.CompilerServices.CastHelpers.ChkCastClassSpecial(Void*, System.Object)
L0035: mov rbp, rsi
L0038: test rdi, rdi
L003b: je L011d
L0041: test rbp, rbp
L0044: je short L005e
L0046: mov rcx, 0x7ff953a2e0b8
L0050: cmp [rbp], rcx
L0054: je short L005e
L0056: mov rdx, rbp
L0059: call System.Runtime.CompilerServices.CastHelpers.ChkCastClassSpecial(Void*, System.Object)
L005e: test rbp, rbp
L0061: je L010d
L0067: mov rdx, rdi
L006a: mov rcx, 0x7ff953a2e8a8
L0074: call System.Runtime.CompilerServices.CastHelpers.IsInstanceOfClass(Void*, System.Object)
L0079: test rax, rax
L007c: je short L0085
L007e: mov esi, 2
L0083: jmp short L00a5
L0085: mov rdx, rdi
L0088: mov rcx, 0x7ff953a2e700
L0092: call System.Runtime.CompilerServices.CastHelpers.IsInstanceOfClass(Void*, System.Object)
L0097: test rax, rax
L009a: je short L00a3
L009c: mov esi, 1
L00a1: jmp short L00a5
L00a3: xor esi, esi
L00a5: mov rdx, rbp
L00a8: mov rcx, 0x7ff953a2e8a8
L00b2: call System.Runtime.CompilerServices.CastHelpers.IsInstanceOfClass(Void*, System.Object)
L00b7: test rax, rax
L00ba: je short L00c4
L00bc: mov r14d, 2
L00c2: jmp short L00e6
L00c4: mov rdx, rbp
L00c7: mov rcx, 0x7ff953a2e700
L00d1: call System.Runtime.CompilerServices.CastHelpers.IsInstanceOfClass(Void*, System.Object)
L00d6: test rax, rax
L00d9: je short L00e3
L00db: mov r14d, 1
L00e1: jmp short L00e6
L00e3: xor r14d, r14d
L00e6: cmp esi, r14d
L00e9: jne short L0106
L00eb: mov rcx, rbx
L00ee: mov rdx, rdi
L00f1: mov r8, rbp
L00f4: xor r9d, r9d
L00f7: add rsp, 0x20
L00fb: pop rbx
L00fc: pop rbp
L00fd: pop rsi
L00fe: pop rdi
L00ff: pop r14
L0101: jmp Test+Test.CompareTo$cont@6-1(System.Collections.IComparer, DecisionType, DecisionType, Microsoft.FSharp.Core.Unit)
L0106: mov eax, esi
L0108: sub eax, r14d
L010b: jmp short L015e
L010d: mov eax, 1
L0112: add rsp, 0x20
L0116: pop rbx
L0117: pop rbp
L0118: pop rsi
L0119: pop rdi
L011a: pop r14
L011c: ret
L011d: mov rax, rsi
L0120: test rax, rax
L0123: je short L013c
L0125: mov rcx, 0x7ff953a2e0b8
L012f: cmp [rax], rcx
L0132: je short L013c
L0134: mov rdx, rsi
L0137: call System.Runtime.CompilerServices.CastHelpers.ChkCastClassSpecial(Void*, System.Object)
L013c: test rax, rax
L013f: je short L0151
L0141: mov eax, 0xffffffff
L0146: add rsp, 0x20
L014a: pop rbx
L014b: pop rbp
L014c: pop rsi
L014d: pop rdi
L014e: pop r14
L0150: ret
L0151: xor eax, eax
L0153: add rsp, 0x20
L0157: pop rbx
L0158: pop rbp
L0159: pop rsi
L015a: pop rdi
L015b: pop r14
L015d: ret
L015e: add rsp, 0x20
L0162: pop rbx
L0163: pop rbp
L0164: pop rsi
L0165: pop rdi
L0166: pop r14
L0168: ret

;Test+Test+DecisionType.GetHashCode(System.Collections.IEqualityComparer)
L0000: push rdi
L0001: push rsi
L0002: push rbx
L0003: sub rsp, 0x20
L0007: vzeroupper
L000a: mov rsi, rcx
L000d: mov rdi, rdx
L0010: test rsi, rsi
L0013: je L0122
L0019: mov rdx, rsi
L001c: mov rcx, 0x7ff953a2e700
L0026: call System.Runtime.CompilerServices.CastHelpers.IsInstanceOfClass(Void*, System.Object)
L002b: mov rbx, rax
L002e: test rbx, rbx
L0031: jne short L0070
L0033: mov rdx, rsi
L0036: mov rcx, 0x7ff953a2e8a8
L0040: call System.Runtime.CompilerServices.CastHelpers.IsInstanceOfClass(Void*, System.Object)
L0045: test rax, rax
L0048: jne L00c8
L004e: test rax, rax
L0051: je short L005a
L0053: mov eax, 2
L0058: jmp short L0068
L005a: test rbx, rbx
L005d: je short L0066
L005f: mov eax, 1
L0064: jmp short L0068
L0066: xor eax, eax
L0068: add rsp, 0x20
L006c: pop rbx
L006d: pop rsi
L006e: pop rdi
L006f: ret
L0070: mov rcx, rsi
L0073: mov rdx, 0x7ff953a2e700
L007d: cmp [rcx], rdx
L0080: je short L008d
L0082: mov rcx, rdx
L0085: mov rdx, rsi
L0088: call System.Runtime.CompilerServices.CastHelpers.ChkCastClassSpecial(Void*, System.Object)
L008d: vmovsd xmm1, [rsi+0x10]
L0092: mov rcx, rdi
L0095: call Microsoft.FSharp.Core.LanguagePrimitives+HashCompare.GenericHashWithComparerIntrinsic[[System.Double, System.Private.CoreLib]](System.Collections.IEqualityComparer, Double)
L009a: lea ebx, [rax-0x61c88607]
L00a0: vmovsd xmm1, [rsi+8]
L00a5: mov rcx, rdi
L00a8: call Microsoft.FSharp.Core.LanguagePrimitives+HashCompare.GenericHashWithComparerIntrinsic[[System.Double, System.Private.CoreLib]](System.Collections.IEqualityComparer, Double)
L00ad: mov ecx, ebx
L00af: shl ecx, 6
L00b2: add eax, ecx
L00b4: sar ebx, 2
L00b7: lea ebx, [rax+rbx-0x61c88647]
L00be: mov eax, ebx
L00c0: add rsp, 0x20
L00c4: pop rbx
L00c5: pop rsi
L00c6: pop rdi
L00c7: ret
L00c8: mov rcx, rsi
L00cb: mov rdx, 0x7ff953a2e8a8
L00d5: cmp [rcx], rdx
L00d8: je short L00e5
L00da: mov rcx, rdx
L00dd: mov rdx, rsi
L00e0: call System.Runtime.CompilerServices.CastHelpers.ChkCastClassSpecial(Void*, System.Object)
L00e5: vmovsd xmm1, [rsi+0x10]
L00ea: mov rcx, rdi
L00ed: call Microsoft.FSharp.Core.LanguagePrimitives+HashCompare.GenericHashWithComparerIntrinsic[[System.Double, System.Private.CoreLib]](System.Collections.IEqualityComparer, Double)
L00f2: lea ebx, [rax-0x61c885c7]
L00f8: vmovsd xmm1, [rsi+8]
L00fd: mov rcx, rdi
L0100: call Microsoft.FSharp.Core.LanguagePrimitives+HashCompare.GenericHashWithComparerIntrinsic[[System.Double, System.Private.CoreLib]](System.Collections.IEqualityComparer, Double)
L0105: mov edx, ebx
L0107: shl edx, 6
L010a: add eax, edx
L010c: mov edx, ebx
L010e: sar edx, 2
L0111: lea ebx, [rax+rdx-0x61c88647]
L0118: mov eax, ebx
L011a: add rsp, 0x20
L011e: pop rbx
L011f: pop rsi
L0120: pop rdi
L0121: ret
L0122: xor eax, eax
L0124: add rsp, 0x20
L0128: pop rbx
L0129: pop rsi
L012a: pop rdi
L012b: ret

;Test+Test+DecisionType.GetHashCode()
L0000: mov rdx, 0x191f2a86fa8
L000a: mov rdx, [rdx]
L000d: cmp [rcx], ecx
L000f: jmp Test+Test+DecisionType.GetHashCode(System.Collections.IEqualityComparer)

;Test+Test+DecisionType.Equals(System.Object, System.Collections.IEqualityComparer)
L0000: push r14
L0002: push rdi
L0003: push rsi
L0004: push rbp
L0005: push rbx
L0006: sub rsp, 0x20
L000a: vzeroupper
L000d: mov rsi, rcx
L0010: test rsi, rsi
L0013: je L01a1
L0019: mov rcx, 0x7ff953a2e0b8
L0023: call System.Runtime.CompilerServices.CastHelpers.IsInstanceOfClass(Void*, System.Object)
L0028: mov rdi, rax
L002b: test rdi, rdi
L002e: je L0194
L0034: mov rdx, rsi
L0037: mov rcx, 0x7ff953a2e8a8
L0041: call System.Runtime.CompilerServices.CastHelpers.IsInstanceOfClass(Void*, System.Object)
L0046: mov rbx, rax
L0049: test rbx, rbx
L004c: je short L0055
L004e: mov ebp, 2
L0053: jmp short L0075
L0055: mov rdx, rsi
L0058: mov rcx, 0x7ff953a2e700
L0062: call System.Runtime.CompilerServices.CastHelpers.IsInstanceOfClass(Void*, System.Object)
L0067: test rax, rax
L006a: je short L0073
L006c: mov ebp, 1
L0071: jmp short L0075
L0073: xor ebp, ebp
L0075: mov rdx, rdi
L0078: mov rcx, 0x7ff953a2e8a8
L0082: call System.Runtime.CompilerServices.CastHelpers.IsInstanceOfClass(Void*, System.Object)
L0087: test rax, rax
L008a: je short L0094
L008c: mov r14d, 2
L0092: jmp short L00b6
L0094: mov rdx, rdi
L0097: mov rcx, 0x7ff953a2e700
L00a1: call System.Runtime.CompilerServices.CastHelpers.IsInstanceOfClass(Void*, System.Object)
L00a6: test rax, rax
L00a9: je short L00b3
L00ab: mov r14d, 1
L00b1: jmp short L00b6
L00b3: xor r14d, r14d
L00b6: cmp ebp, r14d
L00b9: jne L0194
L00bf: mov rdx, rsi
L00c2: mov rcx, 0x7ff953a2e700
L00cc: call System.Runtime.CompilerServices.CastHelpers.IsInstanceOfClass(Void*, System.Object)
L00d1: test rax, rax
L00d4: jne short L00eb
L00d6: test rbx, rbx
L00d9: jne short L0135
L00db: mov eax, 1
L00e0: add rsp, 0x20
L00e4: pop rbx
L00e5: pop rbp
L00e6: pop rsi
L00e7: pop rdi
L00e8: pop r14
L00ea: ret
L00eb: mov rbx, rdi
L00ee: mov rcx, 0x7ff953a2e700
L00f8: cmp [rbx], rcx
L00fb: je short L0108
L00fd: mov rdx, rdi
L0100: call System.Runtime.CompilerServices.CastHelpers.ChkCastClassSpecial(Void*, System.Object)
L0105: mov rbx, rax
L0108: vmovsd xmm0, [rsi+8]
L010d: vucomisd xmm0, [rbx+8]
L0112: jp L0194
L0118: jne L0194
L011e: vmovsd xmm0, [rsi+0x10]
L0123: vucomisd xmm0, [rbx+0x10]
L0128: setnp bl
L012b: jp short L0130
L012d: sete bl
L0130: movzx ebx, bl
L0133: jmp short L01aa
L0135: mov rcx, rsi
L0138: mov rdx, 0x7ff953a2e8a8
L0142: cmp [rcx], rdx
L0145: je short L0152
L0147: mov rcx, rdx
L014a: mov rdx, rsi
L014d: call System.Runtime.CompilerServices.CastHelpers.ChkCastClassSpecial(Void*, System.Object)
L0152: mov rbx, rdi
L0155: mov rcx, 0x7ff953a2e8a8
L015f: cmp [rbx], rcx
L0162: je short L016f
L0164: mov rdx, rdi
L0167: call System.Runtime.CompilerServices.CastHelpers.ChkCastClassSpecial(Void*, System.Object)
L016c: mov rbx, rax
L016f: vmovsd xmm0, [rsi+8]
L0174: vucomisd xmm0, [rbx+8]
L0179: jp short L0194
L017b: jne short L0194
L017d: vmovsd xmm0, [rsi+0x10]
L0182: vucomisd xmm0, [rbx+0x10]
L0187: setnp bl
L018a: jp short L018f
L018c: sete bl
L018f: movzx ebx, bl
L0192: jmp short L01aa
L0194: xor eax, eax
L0196: add rsp, 0x20
L019a: pop rbx
L019b: pop rbp
L019c: pop rsi
L019d: pop rdi
L019e: pop r14
L01a0: ret
L01a1: test rdx, rdx
L01a4: sete bl
L01a7: movzx ebx, bl
L01aa: movzx eax, bl
L01ad: add rsp, 0x20
L01b1: pop rbx
L01b2: pop rbp
L01b3: pop rsi
L01b4: pop rdi
L01b5: pop r14
L01b7: ret

;Test+Test+DecisionType.Equals(DecisionType)
L0000: push rdi
L0001: push rsi
L0002: push rbp
L0003: push rbx
L0004: sub rsp, 0x28
L0008: mov rdi, rcx
L000b: mov rsi, rdx
L000e: test rdi, rdi
L0011: je L00cc
L0017: test rsi, rsi
L001a: je L00c1
L0020: mov rdx, rdi
L0023: mov rcx, 0x7ff953a2e8a8
L002d: call System.Runtime.CompilerServices.CastHelpers.IsInstanceOfClass(Void*, System.Object)
L0032: test rax, rax
L0035: je short L003e
L0037: mov ebx, 2
L003c: jmp short L005e
L003e: mov rdx, rdi
L0041: mov rcx, 0x7ff953a2e700
L004b: call System.Runtime.CompilerServices.CastHelpers.IsInstanceOfClass(Void*, System.Object)
L0050: test rax, rax
L0053: je short L005c
L0055: mov ebx, 1
L005a: jmp short L005e
L005c: xor ebx, ebx
L005e: mov rdx, rsi
L0061: mov rcx, 0x7ff953a2e8a8
L006b: call System.Runtime.CompilerServices.CastHelpers.IsInstanceOfClass(Void*, System.Object)
L0070: test rax, rax
L0073: je short L007c
L0075: mov ebp, 2
L007a: jmp short L009c
L007c: mov rdx, rsi
L007f: mov rcx, 0x7ff953a2e700
L0089: call System.Runtime.CompilerServices.CastHelpers.IsInstanceOfClass(Void*, System.Object)
L008e: test rax, rax
L0091: je short L009a
L0093: mov ebp, 1
L0098: jmp short L009c
L009a: xor ebp, ebp
L009c: cmp ebx, ebp
L009e: jne short L00b6
L00a0: mov rcx, rdi
L00a3: mov rdx, rsi
L00a6: xor r8d, r8d
L00a9: add rsp, 0x28
L00ad: pop rbx
L00ae: pop rbp
L00af: pop rsi
L00b0: pop rdi
L00b1: jmp Test+Test.Equals$cont@6(DecisionType, DecisionType, Microsoft.FSharp.Core.Unit)
L00b6: xor eax, eax
L00b8: add rsp, 0x28
L00bc: pop rbx
L00bd: pop rbp
L00be: pop rsi
L00bf: pop rdi
L00c0: ret
L00c1: xor eax, eax
L00c3: add rsp, 0x28
L00c7: pop rbx
L00c8: pop rbp
L00c9: pop rsi
L00ca: pop rdi
L00cb: ret
L00cc: test rsi, rsi
L00cf: sete al
L00d2: movzx eax, al
L00d5: add rsp, 0x28
L00d9: pop rbx
L00da: pop rbp
L00db: pop rsi
L00dc: pop rdi
L00dd: ret

;Test+Test+DecisionType.Equals(System.Object)
L0000: push rsi
L0001: sub rsp, 0x20
L0005: mov rsi, rcx
L0008: mov rcx, 0x7ff953a2e0b8
L0012: call System.Runtime.CompilerServices.CastHelpers.IsInstanceOfClass(Void*, System.Object)
L0017: test rax, rax
L001a: je short L002e
L001c: mov rcx, rsi
L001f: mov rdx, rax
L0022: cmp [rcx], ecx
L0024: add rsp, 0x20
L0028: pop rsi
L0029: jmp Test+Test+DecisionType.Equals(DecisionType)
L002e: xor eax, eax
L0030: add rsp, 0x20
L0034: pop rsi
L0035: ret

;Test+Test+DecisionName.NewDecisionName(System.String)
L0000: push rdi
L0001: push rsi
L0002: sub rsp, 0x28
L0006: mov rsi, rcx
L0009: mov rcx, 0x7ff953af0038
L0013: call 0x00007ff9b182a470
L0018: mov rdi, rax
L001b: lea rcx, [rdi+8]
L001f: mov rdx, rsi
L0022: call 0x00007ff9b182a080
L0027: mov rax, rdi
L002a: add rsp, 0x28
L002e: pop rsi
L002f: pop rdi
L0030: ret

;Test+Test+DecisionName.get_Item()
L0000: mov rax, [rcx+8]
L0004: ret

;Test+Test+DecisionName.get_Tag()
L0000: xor eax, eax
L0002: ret

;Test+Test+DecisionName.ToString()
L0000: push rdi
L0001: push rsi
L0002: sub rsp, 0x28
L0006: mov rsi, rcx
L0009: mov rcx, 0x7ff953af67e0
L0013: call 0x00007ff9b182a470
L0018: mov rdi, rax
L001b: mov rdx, 0x191f2a9b3f0
L0025: mov rdx, [rdx]
L0028: lea rcx, [rdi+8]
L002c: call 0x00007ff9b182a080
L0031: xor edx, edx
L0033: mov [rdi+0x10], rdx
L0037: mov [rdi+0x18], rdx
L003b: mov rdx, rdi
L003e: mov rcx, 0x7ff953af6ac0
L0048: call Microsoft.FSharp.Core.PrintfModule.PrintFormatToStringThen[[System.__Canon, System.Private.CoreLib]](Microsoft.FSharp.Core.PrintfFormat`4<System.__Canon,Microsoft.FSharp.Core.Unit,System.String,System.String>)
L004d: mov rcx, rax
L0050: mov rdx, rsi
L0053: mov rax, [rax]
L0056: mov rax, [rax+0x40]
L005a: mov rax, [rax+0x20]
L005e: add rsp, 0x28
L0062: pop rsi
L0063: pop rdi
L0064: jmp rax

;Test+Test+DecisionName.CompareTo(DecisionName)
L0000: test rcx, rcx
L0003: je short L001d
L0005: test rdx, rdx
L0008: je short L0017
L000a: mov rcx, [rcx+8]
L000e: mov rdx, [rdx+8]
L0012: jmp System.String.CompareOrdinal(System.String, System.String)
L0017: mov eax, 1
L001c: ret
L001d: test rdx, rdx
L0020: je short L0028
L0022: mov eax, 0xffffffff
L0027: ret
L0028: xor eax, eax
L002a: ret

;Test+Test+DecisionName.CompareTo(System.Object)
L0000: push rsi
L0001: sub rsp, 0x20
L0005: mov rsi, rcx
L0008: mov rax, rdx
L000b: test rax, rax
L000e: je short L0024
L0010: mov rcx, 0x7ff953af0038
L001a: cmp [rax], rcx
L001d: je short L0024
L001f: call System.Runtime.CompilerServices.CastHelpers.ChkCastClassSpecial(Void*, System.Object)
L0024: mov rcx, rsi
L0027: mov rdx, rax
L002a: cmp [rcx], ecx
L002c: add rsp, 0x20
L0030: pop rsi
L0031: jmp Test+Test+DecisionName.CompareTo(DecisionName)

;Test+Test+DecisionName.CompareTo(System.Object, System.Collections.IComparer)
L0000: push rdi
L0001: push rsi
L0002: push rbx
L0003: sub rsp, 0x20
L0007: mov rdi, rcx
L000a: mov rsi, rdx
L000d: mov rcx, rsi
L0010: test rcx, rcx
L0013: je short L002f
L0015: mov rdx, 0x7ff953af0038
L001f: cmp [rcx], rdx
L0022: je short L002f
L0024: mov rcx, rdx
L0027: mov rdx, rsi
L002a: call System.Runtime.CompilerServices.CastHelpers.ChkCastClassSpecial(Void*, System.Object)
L002f: mov rbx, rsi
L0032: test rdi, rdi
L0035: je short L0079
L0037: test rbx, rbx
L003a: je short L0053
L003c: mov rcx, 0x7ff953af0038
L0046: cmp [rbx], rcx
L0049: je short L0053
L004b: mov rdx, rbx
L004e: call System.Runtime.CompilerServices.CastHelpers.ChkCastClassSpecial(Void*, System.Object)
L0053: test rbx, rbx
L0056: je short L006c
L0058: mov rcx, [rdi+8]
L005c: mov rdx, [rbx+8]
L0060: add rsp, 0x20
L0064: pop rbx
L0065: pop rsi
L0066: pop rdi
L0067: jmp System.String.CompareOrdinal(System.String, System.String)
L006c: mov eax, 1
L0071: add rsp, 0x20
L0075: pop rbx
L0076: pop rsi
L0077: pop rdi
L0078: ret
L0079: mov rax, rsi
L007c: test rax, rax
L007f: je short L0098
L0081: mov rcx, 0x7ff953af0038
L008b: cmp [rax], rcx
L008e: je short L0098
L0090: mov rdx, rsi
L0093: call System.Runtime.CompilerServices.CastHelpers.ChkCastClassSpecial(Void*, System.Object)
L0098: test rax, rax
L009b: je short L00aa
L009d: mov eax, 0xffffffff
L00a2: add rsp, 0x20
L00a6: pop rbx
L00a7: pop rsi
L00a8: pop rdi
L00a9: ret
L00aa: xor eax, eax
L00ac: add rsp, 0x20
L00b0: pop rbx
L00b1: pop rsi
L00b2: pop rdi
L00b3: ret

;Test+Test+DecisionName.GetHashCode(System.Collections.IEqualityComparer)
L0000: sub rsp, 0x28
L0004: test rcx, rcx
L0007: je short L003a
L0009: mov rdx, [rcx+8]
L000d: test rdx, rdx
L0010: je short L002e
L0012: lea rcx, [rdx+0xc]
L0016: mov edx, [rdx+8]
L0019: add edx, edx
L001b: mov r8d, 0x315ddb11
L0021: mov r9d, 0xa84d9600
L0027: call System.Marvin.ComputeHash32(Byte ByRef, UInt32, UInt32, UInt32)
L002c: jmp short L0030
L002e: xor eax, eax
L0030: add eax, 0x9e3779b9
L0035: add rsp, 0x28
L0039: ret
L003a: xor eax, eax
L003c: add rsp, 0x28
L0040: ret

;Test+Test+DecisionName.GetHashCode()
L0000: mov rdx, 0x191f2a86fa8
L000a: mov rdx, [rdx]
L000d: cmp [rcx], ecx
L000f: jmp Test+Test+DecisionName.GetHashCode(System.Collections.IEqualityComparer)

;Test+Test+DecisionName.Equals(System.Object, System.Collections.IEqualityComparer)
L0000: test rcx, rcx
L0003: je short L0030
L0005: test rdx, rdx
L0008: je short L001b
L000a: mov rax, 0x7ff953af0038
L0014: cmp [rdx], rax
L0017: je short L001b
L0019: xor edx, edx
L001b: test rdx, rdx
L001e: je short L002d
L0020: mov rcx, [rcx+8]
L0024: mov rdx, [rdx+8]
L0028: jmp System.String.Equals(System.String, System.String)
L002d: xor eax, eax
L002f: ret
L0030: test rdx, rdx
L0033: sete al
L0036: movzx eax, al
L0039: ret

;Test+Test+DecisionName.Equals(DecisionName)
L0000: test rcx, rcx
L0003: je short L001a
L0005: test rdx, rdx
L0008: je short L0017
L000a: mov rcx, [rcx+8]
L000e: mov rdx, [rdx+8]
L0012: jmp System.String.Equals(System.String, System.String)
L0017: xor eax, eax
L0019: ret
L001a: test rdx, rdx
L001d: sete al
L0020: movzx eax, al
L0023: ret

;Test+Test+DecisionName.Equals(System.Object)
L0000: test rdx, rdx
L0003: je short L0016
L0005: mov rax, 0x7ff953af0038
L000f: cmp [rdx], rax
L0012: je short L0016
L0014: xor edx, edx
L0016: test rdx, rdx
L0019: je short L0022
L001b: cmp [rcx], ecx
L001d: jmp Test+Test+DecisionName.Equals(DecisionName)
L0022: xor eax, eax
L0024: ret

;Test+Test+Decision..ctor(DecisionName, DecisionType)
L0000: push rdi
L0001: push rsi
L0002: mov rsi, rcx
L0005: mov rdi, r8
L0008: mov rcx, rsi
L000b: call 0x00007ff9b182a050
L0010: lea rcx, [rsi+8]
L0014: mov rdx, rdi
L0017: call 0x00007ff9b182a050
L001c: nop
L001d: pop rsi
L001e: pop rdi
L001f: ret

;Test+Test+Decision.get_Name()
L0000: mov rax, [rcx]
L0003: ret

;Test+Test+Decision.get_Type()
L0000: mov rax, [rcx+8]
L0004: ret

;Test+Test+Decision.op_Addition(Double, Decision)
L0000: push rdi
L0001: push rsi
L0002: push rbp
L0003: push rbx
L0004: sub rsp, 0x28
L0008: vzeroupper
L000b: mov rsi, rdx
L000e: vmovsd [rsp+0x50], xmm0
L0014: mov rcx, 0x7ff953af0ff8
L001e: call 0x00007ff9b182a470
L0023: mov rdi, rax
L0026: xor ecx, ecx
L0028: mov [rdi+8], ecx
L002b: vmovsd xmm0, [rsp+0x50]
L0031: vmovsd [rdi+0x10], xmm0
L0036: mov rbx, [rsi]
L0039: mov rsi, [rsi+8]
L003d: mov rcx, 0x7ff953af1178
L0047: call 0x00007ff9b182a470
L004c: mov rbp, rax
L004f: mov rdx, rbx
L0052: mov dword ptr [rbp+8], 1
L0059: lea rbx, [rbp+0x10]
L005d: mov rcx, rbx
L0060: call 0x00007ff9b182a050
L0065: lea rcx, [rbx+8]
L0069: mov rdx, rsi
L006c: call 0x00007ff9b182a050
L0071: mov rcx, 0x7ff953af14c8
L007b: call 0x00007ff9b182a470
L0080: mov rsi, rax
L0083: mov dword ptr [rsi+8], 3
L008a: lea rcx, [rsi+0x10]
L008e: mov rdx, rdi
L0091: call 0x00007ff9b182a080
L0096: lea rcx, [rsi+0x18]
L009a: mov rdx, rbp
L009d: call 0x00007ff9b182a080
L00a2: mov rax, rsi
L00a5: add rsp, 0x28
L00a9: pop rbx
L00aa: pop rbp
L00ab: pop rsi
L00ac: pop rdi
L00ad: ret

;Test+Test+Decision.op_Addition(Decision, Decision)
L0000: push rdi
L0001: push rsi
L0002: push rbp
L0003: push rbx
L0004: sub rsp, 0x28
L0008: mov rsi, rdx
L000b: mov rdi, [rcx]
L000e: mov rbx, [rcx+8]
L0012: mov rcx, 0x7ff953af1178
L001c: call 0x00007ff9b182a470
L0021: mov rbp, rax
L0024: mov rdx, rdi
L0027: mov dword ptr [rbp+8], 1
L002e: lea rdi, [rbp+0x10]
L0032: mov rcx, rdi
L0035: call 0x00007ff9b182a050
L003a: lea rcx, [rdi+8]
L003e: mov rdx, rbx
L0041: call 0x00007ff9b182a050
L0046: mov rdi, [rsi]
L0049: mov rsi, [rsi+8]
L004d: mov rcx, 0x7ff953af1178
L0057: call 0x00007ff9b182a470
L005c: mov rbx, rax
L005f: mov rdx, rdi
L0062: mov dword ptr [rbx+8], 1
L0069: lea rdi, [rbx+0x10]
L006d: mov rcx, rdi
L0070: call 0x00007ff9b182a050
L0075: lea rcx, [rdi+8]
L0079: mov rdx, rsi
L007c: call 0x00007ff9b182a050
L0081: mov rcx, 0x7ff953af14c8
L008b: call 0x00007ff9b182a470
L0090: mov rsi, rax
L0093: mov dword ptr [rsi+8], 3
L009a: lea rcx, [rsi+0x10]
L009e: mov rdx, rbp
L00a1: call 0x00007ff9b182a080
L00a6: lea rcx, [rsi+0x18]
L00aa: mov rdx, rbx
L00ad: call 0x00007ff9b182a080
L00b2: mov rax, rsi
L00b5: add rsp, 0x28
L00b9: pop rbx
L00ba: pop rbp
L00bb: pop rsi
L00bc: pop rdi
L00bd: ret

;Test+Test+Decision.op_Multiply(Double, Decision)
L0000: push rdi
L0001: push rsi
L0002: push rbx
L0003: sub rsp, 0x20
L0007: vzeroupper
L000a: vmovsd [rsp+0x40], xmm0
L0010: mov rsi, [rdx]
L0013: mov rdi, [rdx+8]
L0017: mov rcx, 0x7ff953af1178
L0021: call 0x00007ff9b182a470
L0026: mov rbx, rax
L0029: mov rdx, rsi
L002c: mov dword ptr [rbx+8], 1
L0033: lea rsi, [rbx+0x10]
L0037: mov rcx, rsi
L003a: call 0x00007ff9b182a050
L003f: lea rcx, [rsi+8]
L0043: mov rdx, rdi
L0046: call 0x00007ff9b182a050
L004b: mov rcx, 0x7ff953af1320
L0055: call 0x00007ff9b182a470
L005a: mov rsi, rax
L005d: mov dword ptr [rsi+8], 2
L0064: vmovsd xmm0, [rsp+0x40]
L006a: vmovsd [rsi+0x18], xmm0
L006f: lea rcx, [rsi+0x10]
L0073: mov rdx, rbx
L0076: call 0x00007ff9b182a080
L007b: mov rax, rsi
L007e: add rsp, 0x20
L0082: pop rbx
L0083: pop rsi
L0084: pop rdi
L0085: ret

;Test+Test+Decision.op_Multiply(Decision, Double)
L0000: push rdi
L0001: push rsi
L0002: push rbx
L0003: sub rsp, 0x20
L0007: vzeroupper
L000a: vmovsd [rsp+0x48], xmm1
L0010: mov rsi, [rcx]
L0013: mov rdi, [rcx+8]
L0017: mov rcx, 0x7ff953af1178
L0021: call 0x00007ff9b182a470
L0026: mov rbx, rax
L0029: mov rdx, rsi
L002c: mov dword ptr [rbx+8], 1
L0033: lea rsi, [rbx+0x10]
L0037: mov rcx, rsi
L003a: call 0x00007ff9b182a050
L003f: lea rcx, [rsi+8]
L0043: mov rdx, rdi
L0046: call 0x00007ff9b182a050
L004b: mov rcx, 0x7ff953af1320
L0055: call 0x00007ff9b182a470
L005a: mov rsi, rax
L005d: mov dword ptr [rsi+8], 2
L0064: vmovsd xmm1, [rsp+0x48]
L006a: vmovsd [rsi+0x18], xmm1
L006f: lea rcx, [rsi+0x10]
L0073: mov rdx, rbx
L0076: call 0x00007ff9b182a080
L007b: mov rax, rsi
L007e: add rsp, 0x20
L0082: pop rbx
L0083: pop rsi
L0084: pop rdi
L0085: ret

;Test+Test+LinearExpr.NewFloat(Double)
L0000: sub rsp, 0x28
L0004: vzeroupper
L0007: vmovsd [rsp+0x30], xmm0
L000d: mov rcx, 0x7ff953af0ff8
L0017: call 0x00007ff9b182a470
L001c: xor edx, edx
L001e: mov [rax+8], edx
L0021: vmovsd xmm0, [rsp+0x30]
L0027: vmovsd [rax+0x10], xmm0
L002c: add rsp, 0x28
L0030: ret

;Test+Test+LinearExpr.get_IsFloat()
L0000: cmp dword ptr [rcx+8], 0
L0004: sete al
L0007: movzx eax, al
L000a: ret

;Test+Test+LinearExpr.NewDecision(Decision)
L0000: push rdi
L0001: push rsi
L0002: push rbx
L0003: sub rsp, 0x20
L0007: mov rsi, rcx
L000a: mov rcx, 0x7ff953af1178
L0014: call 0x00007ff9b182a470
L0019: mov rdi, rax
L001c: mov rdx, [rsi]
L001f: mov rsi, [rsi+8]
L0023: mov dword ptr [rdi+8], 1
L002a: lea rbx, [rdi+0x10]
L002e: mov rcx, rbx
L0031: call 0x00007ff9b182a050
L0036: lea rcx, [rbx+8]
L003a: mov rdx, rsi
L003d: call 0x00007ff9b182a050
L0042: mov rax, rdi
L0045: add rsp, 0x20
L0049: pop rbx
L004a: pop rsi
L004b: pop rdi
L004c: ret

;Test+Test+LinearExpr.get_IsDecision()
L0000: cmp dword ptr [rcx+8], 1
L0004: sete al
L0007: movzx eax, al
L000a: ret

;Test+Test+LinearExpr.NewScale(Double, LinearExpr)
L0000: push rdi
L0001: push rsi
L0002: sub rsp, 0x28
L0006: vzeroupper
L0009: mov rsi, rdx
L000c: vmovsd [rsp+0x40], xmm0
L0012: mov rcx, 0x7ff953af1320
L001c: call 0x00007ff9b182a470
L0021: mov rdi, rax
L0024: mov dword ptr [rdi+8], 2
L002b: vmovsd xmm0, [rsp+0x40]
L0031: vmovsd [rdi+0x18], xmm0
L0036: lea rcx, [rdi+0x10]
L003a: mov rdx, rsi
L003d: call 0x00007ff9b182a080
L0042: mov rax, rdi
L0045: add rsp, 0x28
L0049: pop rsi
L004a: pop rdi
L004b: ret

;Test+Test+LinearExpr.get_IsScale()
L0000: cmp dword ptr [rcx+8], 2
L0004: sete al
L0007: movzx eax, al
L000a: ret

;Test+Test+LinearExpr.NewAdd(LinearExpr, LinearExpr)
L0000: push rdi
L0001: push rsi
L0002: push rbx
L0003: sub rsp, 0x20
L0007: mov rsi, rcx
L000a: mov rdi, rdx
L000d: mov rcx, 0x7ff953af14c8
L0017: call 0x00007ff9b182a470
L001c: mov rbx, rax
L001f: mov dword ptr [rbx+8], 3
L0026: lea rcx, [rbx+0x10]
L002a: mov rdx, rsi
L002d: call 0x00007ff9b182a080
L0032: lea rcx, [rbx+0x18]
L0036: mov rdx, rdi
L0039: call 0x00007ff9b182a080
L003e: mov rax, rbx
L0041: add rsp, 0x20
L0045: pop rbx
L0046: pop rsi
L0047: pop rdi
L0048: ret

;Test+Test+LinearExpr.get_IsAdd()
L0000: cmp dword ptr [rcx+8], 3
L0004: sete al
L0007: movzx eax, al
L000a: ret

;Test+Test+LinearExpr.get_Tag()
L0000: mov eax, [rcx+8]
L0003: ret

;Test+Test+LinearExpr.ToString()
L0000: push rdi
L0001: push rsi
L0002: sub rsp, 0x28
L0006: mov rsi, rcx
L0009: mov rcx, 0x7ff953af84a0
L0013: call 0x00007ff9b182a470
L0018: mov rdi, rax
L001b: mov rdx, 0x191f2a9b3f0
L0025: mov rdx, [rdx]
L0028: lea rcx, [rdi+8]
L002c: call 0x00007ff9b182a080
L0031: xor edx, edx
L0033: mov [rdi+0x10], rdx
L0037: mov [rdi+0x18], rdx
L003b: mov rdx, rdi
L003e: mov rcx, 0x7ff953af8780
L0048: call Microsoft.FSharp.Core.PrintfModule.PrintFormatToStringThen[[System.__Canon, System.Private.CoreLib]](Microsoft.FSharp.Core.PrintfFormat`4<System.__Canon,Microsoft.FSharp.Core.Unit,System.String,System.String>)
L004d: mov rcx, rax
L0050: mov rdx, rsi
L0053: mov rax, [rax]
L0056: mov rax, [rax+0x40]
L005a: mov rax, [rax+0x20]
L005e: add rsp, 0x28
L0062: pop rsi
L0063: pop rdi
L0064: jmp rax

;Test+Test+LinearExpr.CompareTo(LinearExpr)
L0000: push r14
L0002: push rdi
L0003: push rsi
L0004: push rbp
L0005: push rbx
L0006: sub rsp, 0x30
L000a: vzeroupper
L000d: xor eax, eax
L000f: mov [rsp+0x20], rax
L0014: mov [rsp+0x28], rax
L0019: mov rsi, rcx
L001c: mov rdi, rdx
L001f: test rsi, rsi
L0022: je L02e3
L0028: test rdi, rdi
L002b: je L02d3
L0031: mov ebx, [rsi+8]
L0034: mov eax, ebx
L0036: mov ebp, [rdi+8]
L0039: cmp eax, ebp
L003b: jne L02cf
L0041: cmp ebx, 3
L0044: ja short L005e
L0046: mov ecx, ebx
L0048: lea rdx, [Test+Test+LinearExpr.CompareTo(LinearExpr)]
L004f: mov edx, [rdx+rcx*4]
L0052: lea rax, [L001f]
L0059: add rdx, rax
L005c: jmp rdx
L005e: mov rcx, rsi
L0061: mov rdx, 0x7ff953af0ff8
L006b: cmp [rcx], rdx
L006e: je short L007b
L0070: mov rcx, rdx
L0073: mov rdx, rsi
L0076: call System.Runtime.CompilerServices.CastHelpers.ChkCastClassSpecial(Void*, System.Object)
L007b: mov rax, rdi
L007e: mov rcx, 0x7ff953af0ff8
L0088: cmp [rax], rcx
L008b: je short L0095
L008d: mov rdx, rdi
L0090: call System.Runtime.CompilerServices.CastHelpers.ChkCastClassSpecial(Void*, System.Object)
L0095: mov rcx, 0x191f2a86fa0
L009f: mov r14, [rcx]
L00a2: vmovsd xmm1, [rsi+0x10]
L00a7: vmovsd xmm2, [rax+0x10]
L00ac: vucomisd xmm2, xmm1
L00b0: ja L02e8
L00b6: vucomisd xmm1, xmm2
L00ba: ja L02d3
L00c0: vucomisd xmm1, xmm2
L00c4: jp short L00cc
L00c6: je L02f8
L00cc: mov rcx, r14
L00cf: add rsp, 0x30
L00d3: pop rbx
L00d4: pop rbp
L00d5: pop rsi
L00d6: pop rdi
L00d7: pop r14
L00d9: jmp Microsoft.FSharp.Core.LanguagePrimitives+HashCompare.GenericComparisonWithComparerIntrinsic[[System.Double, System.Private.CoreLib]](System.Collections.IComparer, Double, Double)
L00de: mov rcx, rsi
L00e1: mov rdx, 0x7ff953af1178
L00eb: cmp [rcx], rdx
L00ee: je short L00fb
L00f0: mov rcx, rdx
L00f3: mov rdx, rsi
L00f6: call System.Runtime.CompilerServices.CastHelpers.ChkCastClassSpecial(Void*, System.Object)
L00fb: mov rbx, rdi
L00fe: mov rcx, 0x7ff953af1178
L0108: cmp [rbx], rcx
L010b: je short L0118
L010d: mov rdx, rdi
L0110: call System.Runtime.CompilerServices.CastHelpers.ChkCastClassSpecial(Void*, System.Object)
L0115: mov rbx, rax
L0118: mov rcx, 0x191f2a86fa0
L0122: mov r14, [rcx]
L0125: add rsi, 0x10
L0129: mov rcx, [rsi]
L012c: mov [rsp+0x20], rcx
L0131: mov rcx, [rsi+8]
L0135: mov [rsp+0x28], rcx
L013a: add rbx, 0x10
L013e: mov rdi, [rbx]
L0141: mov rsi, [rbx+8]
L0145: mov rcx, 0x7ff953af0538
L014f: call 0x00007ff9b182a470
L0154: mov rbx, rax
L0157: lea rbp, [rbx+8]
L015b: mov rcx, rbp
L015e: mov rdx, rdi
L0161: call 0x00007ff9b182a050
L0166: lea rcx, [rbp+8]
L016a: mov rdx, rsi
L016d: call 0x00007ff9b182a050
L0172: mov rdx, rbx
L0175: lea rcx, [rsp+0x20]
L017a: mov r8, r14
L017d: call Test+Test+Decision.CompareTo(System.Object, System.Collections.IComparer)
L0182: jmp L0305
L0187: mov rcx, rsi
L018a: mov rdx, 0x7ff953af1320
L0194: cmp [rcx], rdx
L0197: je short L01a4
L0199: mov rcx, rdx
L019c: mov rdx, rsi
L019f: call System.Runtime.CompilerServices.CastHelpers.ChkCastClassSpecial(Void*, System.Object)
L01a4: mov rbx, rdi
L01a7: mov rcx, 0x7ff953af1320
L01b1: cmp [rbx], rcx
L01b4: je short L01c1
L01b6: mov rdx, rdi
L01b9: call System.Runtime.CompilerServices.CastHelpers.ChkCastClassSpecial(Void*, System.Object)
L01be: mov rbx, rax
L01c1: mov rcx, 0x191f2a86fa0
L01cb: mov r14, [rcx]
L01ce: vmovsd xmm1, [rsi+0x18]
L01d3: vmovsd xmm2, [rbx+0x18]
L01d8: vucomisd xmm2, xmm1
L01dc: jbe short L01e5
L01de: mov eax, 0xffffffff
L01e3: jmp short L0206
L01e5: vucomisd xmm1, xmm2
L01e9: jbe short L01f2
L01eb: mov eax, 1
L01f0: jmp short L0206
L01f2: vucomisd xmm1, xmm2
L01f6: jp short L01fe
L01f8: jne short L01fe
L01fa: xor eax, eax
L01fc: jmp short L0206
L01fe: mov rcx, r14
L0201: call Microsoft.FSharp.Core.LanguagePrimitives+HashCompare.GenericComparisonWithComparerIntrinsic[[System.Double, System.Private.CoreLib]](System.Collections.IComparer, Double, Double)
L0206: test eax, eax
L0208: jge short L020f
L020a: jmp L0305
L020f: test eax, eax
L0211: jle short L0218
L0213: jmp L0305
L0218: mov rcx, 0x191f2a86fa0
L0222: mov r14, [rcx]
L0225: mov rcx, [rsi+0x10]
L0229: mov rdx, [rbx+0x10]
L022d: mov r8, r14
L0230: cmp [rcx], ecx
L0232: add rsp, 0x30
L0236: pop rbx
L0237: pop rbp
L0238: pop rsi
L0239: pop rdi
L023a: pop r14
L023c: jmp Test+Test+LinearExpr.CompareTo(System.Object, System.Collections.IComparer)
L0241: mov rcx, rsi
L0244: mov rdx, 0x7ff953af14c8
L024e: cmp [rcx], rdx
L0251: je short L025e
L0253: mov rcx, rdx
L0256: mov rdx, rsi
L0259: call System.Runtime.CompilerServices.CastHelpers.ChkCastClassSpecial(Void*, System.Object)
L025e: mov rbx, rdi
L0261: mov rcx, 0x7ff953af14c8
L026b: cmp [rbx], rcx
L026e: je short L027b
L0270: mov rdx, rdi
L0273: call System.Runtime.CompilerServices.CastHelpers.ChkCastClassSpecial(Void*, System.Object)
L0278: mov rbx, rax
L027b: mov rcx, 0x191f2a86fa0
L0285: mov r14, [rcx]
L0288: mov rcx, [rsi+0x10]
L028c: mov rdx, [rbx+0x10]
L0290: mov r8, r14
L0293: cmp [rcx], ecx
L0295: call Test+Test+LinearExpr.CompareTo(System.Object, System.Collections.IComparer)
L029a: test eax, eax
L029c: jge short L02a0
L029e: jmp short L0305
L02a0: test eax, eax
L02a2: jle short L02a6
L02a4: jmp short L0305
L02a6: mov rcx, 0x191f2a86fa0
L02b0: mov r14, [rcx]
L02b3: mov rcx, [rsi+0x18]
L02b7: mov rdx, [rbx+0x18]
L02bb: mov r8, r14
L02be: cmp [rcx], ecx
L02c0: add rsp, 0x30
L02c4: pop rbx
L02c5: pop rbp
L02c6: pop rsi
L02c7: pop rdi
L02c8: pop r14
L02ca: jmp Test+Test+LinearExpr.CompareTo(System.Object, System.Collections.IComparer)
L02cf: sub eax, ebp
L02d1: jmp short L0305
L02d3: mov eax, 1
L02d8: add rsp, 0x30
L02dc: pop rbx
L02dd: pop rbp
L02de: pop rsi
L02df: pop rdi
L02e0: pop r14
L02e2: ret
L02e3: test rdi, rdi
L02e6: je short L02f8
L02e8: mov eax, 0xffffffff
L02ed: add rsp, 0x30
L02f1: pop rbx
L02f2: pop rbp
L02f3: pop rsi
L02f4: pop rdi
L02f5: pop r14
L02f7: ret
L02f8: xor eax, eax
L02fa: add rsp, 0x30
L02fe: pop rbx
L02ff: pop rbp
L0300: pop rsi
L0301: pop rdi
L0302: pop r14
L0304: ret
L0305: add rsp, 0x30
L0309: pop rbx
L030a: pop rbp
L030b: pop rsi
L030c: pop rdi
L030d: pop r14
L030f: ret

;Test+Test+LinearExpr.CompareTo(System.Object)
L0000: push rsi
L0001: sub rsp, 0x20
L0005: mov rsi, rcx
L0008: mov rax, rdx
L000b: test rax, rax
L000e: je short L0024
L0010: mov rcx, 0x7ff953af0bf0
L001a: cmp [rax], rcx
L001d: je short L0024
L001f: call System.Runtime.CompilerServices.CastHelpers.ChkCastClassSpecial(Void*, System.Object)
L0024: mov rcx, rsi
L0027: mov rdx, rax
L002a: cmp [rcx], ecx
L002c: add rsp, 0x20
L0030: pop rsi
L0031: jmp Test+Test+LinearExpr.CompareTo(LinearExpr)

;Test+Test+LinearExpr.CompareTo(System.Object, System.Collections.IComparer)
L0000: push r14
L0002: push rdi
L0003: push rsi
L0004: push rbp
L0005: push rbx
L0006: sub rsp, 0x30
L000a: vzeroupper
L000d: xor eax, eax
L000f: mov [rsp+0x20], rax
L0014: mov [rsp+0x28], rax
L0019: mov rsi, rdx
L001c: mov rdi, r8
L001f: mov rbx, rcx
L0022: mov rbp, rsi
L0025: test rbp, rbp
L0028: je short L0045
L002a: mov rcx, 0x7ff953af0bf0
L0034: cmp [rbp], rcx
L0038: je short L0045
L003a: mov rdx, rsi
L003d: call System.Runtime.CompilerServices.CastHelpers.ChkCastClassSpecial(Void*, System.Object)
L0042: mov rbp, rax
L0045: test rbx, rbx
L0048: je L02d5
L004e: test rbp, rbp
L0051: je short L006b
L0053: mov rcx, 0x7ff953af0bf0
L005d: cmp [rbp], rcx
L0061: je short L006b
L0063: mov rdx, rbp
L0066: call System.Runtime.CompilerServices.CastHelpers.ChkCastClassSpecial(Void*, System.Object)
L006b: test rbp, rbp
L006e: je L02c5
L0074: mov r14d, [rbx+8]
L0078: mov eax, r14d
L007b: mov esi, [rbp+8]
L007e: cmp eax, esi
L0080: jne L02c1
L0086: cmp r14d, 3
L008a: ja L01ad
L0090: mov ecx, r14d
L0093: lea rdx, [Test+Test+LinearExpr.CompareTo(System.Object, System.Collections.IComparer)]
L009a: mov edx, [rdx+rcx*4]
L009d: lea rax, [L001f]
L00a4: add rdx, rax
L00a7: jmp rdx
L00a9: mov rsi, rbx
L00ac: mov rcx, 0x7ff953af1320
L00b6: cmp [rsi], rcx
L00b9: je short L00c6
L00bb: mov rdx, rbx
L00be: call System.Runtime.CompilerServices.CastHelpers.ChkCastClassSpecial(Void*, System.Object)
L00c3: mov rsi, rax
L00c6: mov rbx, rbp
L00c9: mov rcx, 0x7ff953af1320
L00d3: cmp [rbx], rcx
L00d6: je short L00e3
L00d8: mov rdx, rbp
L00db: call System.Runtime.CompilerServices.CastHelpers.ChkCastClassSpecial(Void*, System.Object)
L00e0: mov rbx, rax
L00e3: vmovsd xmm1, [rsi+0x18]
L00e8: vmovsd xmm2, [rbx+0x18]
L00ed: vucomisd xmm2, xmm1
L00f1: jbe short L00fa
L00f3: mov eax, 0xffffffff
L00f8: jmp short L011b
L00fa: vucomisd xmm1, xmm2
L00fe: jbe short L0107
L0100: mov eax, 1
L0105: jmp short L011b
L0107: vucomisd xmm1, xmm2
L010b: jp short L0113
L010d: jne short L0113
L010f: xor eax, eax
L0111: jmp short L011b
L0113: mov rcx, rdi
L0116: call Microsoft.FSharp.Core.LanguagePrimitives+HashCompare.GenericComparisonWithComparerIntrinsic[[System.Double, System.Private.CoreLib]](System.Collections.IComparer, Double, Double)
L011b: test eax, eax
L011d: jl L02b9
L0123: test eax, eax
L0125: jg L02bb
L012b: mov rcx, [rsi+0x10]
L012f: mov rdx, [rbx+0x10]
L0133: mov rsi, rdx
L0136: mov rbx, rcx
L0139: jmp L0022
L013e: mov rsi, rbx
L0141: mov rcx, 0x7ff953af14c8
L014b: cmp [rsi], rcx
L014e: je short L015b
L0150: mov rdx, rbx
L0153: call System.Runtime.CompilerServices.CastHelpers.ChkCastClassSpecial(Void*, System.Object)
L0158: mov rsi, rax
L015b: mov rbx, rbp
L015e: mov rcx, 0x7ff953af14c8
L0168: cmp [rbx], rcx
L016b: je short L0178
L016d: mov rdx, rbp
L0170: call System.Runtime.CompilerServices.CastHelpers.ChkCastClassSpecial(Void*, System.Object)
L0175: mov rbx, rax
L0178: mov rcx, [rsi+0x10]
L017c: mov rdx, [rbx+0x10]
L0180: mov r8, rdi
L0183: cmp [rcx], ecx
L0185: call Test+Test+LinearExpr.CompareTo(System.Object, System.Collections.IComparer)
L018a: test eax, eax
L018c: jl L02bd
L0192: test eax, eax
L0194: jg L02bf
L019a: mov rcx, [rsi+0x18]
L019e: mov rdx, [rbx+0x18]
L01a2: mov rsi, rdx
L01a5: mov rbx, rcx
L01a8: jmp L0022
L01ad: mov rsi, rbx
L01b0: mov rcx, 0x7ff953af0ff8
L01ba: cmp [rsi], rcx
L01bd: je short L01ca
L01bf: mov rdx, rbx
L01c2: call System.Runtime.CompilerServices.CastHelpers.ChkCastClassSpecial(Void*, System.Object)
L01c7: mov rsi, rax
L01ca: mov rax, rbp
L01cd: mov rcx, 0x7ff953af0ff8
L01d7: cmp [rax], rcx
L01da: je short L01e4
L01dc: mov rdx, rbp
L01df: call System.Runtime.CompilerServices.CastHelpers.ChkCastClassSpecial(Void*, System.Object)
L01e4: vmovsd xmm1, [rsi+0x10]
L01e9: vmovsd xmm2, [rax+0x10]
L01ee: vucomisd xmm2, xmm1
L01f2: ja L02f9
L01f8: vucomisd xmm1, xmm2
L01fc: ja L02c5
L0202: vucomisd xmm1, xmm2
L0206: jp short L020e
L0208: je L0309
L020e: mov rcx, rdi
L0211: add rsp, 0x30
L0215: pop rbx
L0216: pop rbp
L0217: pop rsi
L0218: pop rdi
L0219: pop r14
L021b: jmp Microsoft.FSharp.Core.LanguagePrimitives+HashCompare.GenericComparisonWithComparerIntrinsic[[System.Double, System.Private.CoreLib]](System.Collections.IComparer, Double, Double)
L0220: mov rsi, rbx
L0223: mov rcx, 0x7ff953af1178
L022d: cmp [rsi], rcx
L0230: je short L023d
L0232: mov rdx, rbx
L0235: call System.Runtime.CompilerServices.CastHelpers.ChkCastClassSpecial(Void*, System.Object)
L023a: mov rsi, rax
L023d: mov rbx, rbp
L0240: mov rcx, 0x7ff953af1178
L024a: cmp [rbx], rcx
L024d: je short L025a
L024f: mov rdx, rbp
L0252: call System.Runtime.CompilerServices.CastHelpers.ChkCastClassSpecial(Void*, System.Object)
L0257: mov rbx, rax
L025a: add rsi, 0x10
L025e: mov rcx, [rsi]
L0261: mov [rsp+0x20], rcx
L0266: mov rcx, [rsi+8]
L026a: mov [rsp+0x28], rcx
L026f: add rbx, 0x10
L0273: mov rsi, [rbx]
L0276: mov rbx, [rbx+8]
L027a: mov rcx, 0x7ff953af0538
L0284: call 0x00007ff9b182a470
L0289: mov rbp, rax
L028c: lea r14, [rbp+8]
L0290: mov rcx, r14
L0293: mov rdx, rsi
L0296: call 0x00007ff9b182a050
L029b: lea rcx, [r14+8]
L029f: mov rdx, rbx
L02a2: call 0x00007ff9b182a050
L02a7: mov rdx, rbp
L02aa: lea rcx, [rsp+0x20]
L02af: mov r8, rdi
L02b2: call Test+Test+Decision.CompareTo(System.Object, System.Collections.IComparer)
L02b7: jmp short L0316
L02b9: jmp short L0316
L02bb: jmp short L0316
L02bd: jmp short L0316
L02bf: jmp short L0316
L02c1: sub eax, esi
L02c3: jmp short L0316
L02c5: mov eax, 1
L02ca: add rsp, 0x30
L02ce: pop rbx
L02cf: pop rbp
L02d0: pop rsi
L02d1: pop rdi
L02d2: pop r14
L02d4: ret
L02d5: mov rax, rsi
L02d8: test rax, rax
L02db: je short L02f4
L02dd: mov rcx, 0x7ff953af0bf0
L02e7: cmp [rax], rcx
L02ea: je short L02f4
L02ec: mov rdx, rsi
L02ef: call System.Runtime.CompilerServices.CastHelpers.ChkCastClassSpecial(Void*, System.Object)
L02f4: test rax, rax
L02f7: je short L0309
L02f9: mov eax, 0xffffffff
L02fe: add rsp, 0x30
L0302: pop rbx
L0303: pop rbp
L0304: pop rsi
L0305: pop rdi
L0306: pop r14
L0308: ret
L0309: xor eax, eax
L030b: add rsp, 0x30
L030f: pop rbx
L0310: pop rbp
L0311: pop rsi
L0312: pop rdi
L0313: pop r14
L0315: ret
L0316: add rsp, 0x30
L031a: pop rbx
L031b: pop rbp
L031c: pop rsi
L031d: pop rdi
L031e: pop r14
L0320: ret

;Test+Test+LinearExpr.GetHashCode(System.Collections.IEqualityComparer)
L0000: push rdi
L0001: push rsi
L0002: push rbp
L0003: push rbx
L0004: sub rsp, 0x38
L0008: vzeroupper
L000b: xor eax, eax
L000d: mov [rsp+0x28], rax
L0012: mov [rsp+0x30], rax
L0017: mov rsi, rcx
L001a: mov rdi, rdx
L001d: test rsi, rsi
L0020: je L017b
L0026: mov ebx, [rsi+8]
L0029: cmp ebx, 3
L002c: ja short L0046
L002e: mov ecx, ebx
L0030: lea rdx, [Test+Test+LinearExpr.GetHashCode(System.Collections.IEqualityComparer)]
L0037: mov edx, [rdx+rcx*4]
L003a: lea rax, [L001d]
L0041: add rdx, rax
L0044: jmp rdx
L0046: mov rcx, rsi
L0049: mov rdx, 0x7ff953af0ff8
L0053: cmp [rcx], rdx
L0056: je short L0063
L0058: mov rcx, rdx
L005b: mov rdx, rsi
L005e: call System.Runtime.CompilerServices.CastHelpers.ChkCastClassSpecial(Void*, System.Object)
L0063: vmovsd xmm1, [rsi+0x10]
L0068: mov rcx, rdi
L006b: call Microsoft.FSharp.Core.LanguagePrimitives+HashCompare.GenericHashWithComparerIntrinsic[[System.Double, System.Private.CoreLib]](System.Collections.IEqualityComparer, Double)
L0070: lea ebp, [rax-0x61c88647]
L0076: mov eax, ebp
L0078: jmp L0172
L007d: mov rcx, rsi
L0080: mov rdx, 0x7ff953af1178
L008a: cmp [rcx], rdx
L008d: je short L009a
L008f: mov rcx, rdx
L0092: mov rdx, rsi
L0095: call System.Runtime.CompilerServices.CastHelpers.ChkCastClassSpecial(Void*, System.Object)
L009a: add rsi, 0x10
L009e: mov rcx, [rsi]
L00a1: mov [rsp+0x28], rcx
L00a6: mov rcx, [rsi+8]
L00aa: mov [rsp+0x30], rcx
L00af: lea rcx, [rsp+0x28]
L00b4: mov rdx, rdi
L00b7: call Test+Test+Decision.GetHashCode(System.Collections.IEqualityComparer)
L00bc: lea ebp, [rax-0x61c88607]
L00c2: mov eax, ebp
L00c4: jmp L0172
L00c9: mov rcx, rsi
L00cc: mov rdx, 0x7ff953af1320
L00d6: cmp [rcx], rdx
L00d9: je short L00e6
L00db: mov rcx, rdx
L00de: mov rdx, rsi
L00e1: call System.Runtime.CompilerServices.CastHelpers.ChkCastClassSpecial(Void*, System.Object)
L00e6: mov rcx, [rsi+0x10]
L00ea: mov rdx, rdi
L00ed: cmp [rcx], ecx
L00ef: call Test+Test+LinearExpr.GetHashCode(System.Collections.IEqualityComparer)
L00f4: lea ebp, [rax-0x61c885c7]
L00fa: vmovsd xmm1, [rsi+0x18]
L00ff: mov rcx, rdi
L0102: call Microsoft.FSharp.Core.LanguagePrimitives+HashCompare.GenericHashWithComparerIntrinsic[[System.Double, System.Private.CoreLib]](System.Collections.IEqualityComparer, Double)
L0107: mov ecx, ebp
L0109: shl ecx, 6
L010c: add ecx, eax
L010e: mov edx, ebp
L0110: sar edx, 2
L0113: lea ebp, [rcx+rdx-0x61c88647]
L011a: mov eax, ebp
L011c: jmp short L0172
L011e: mov rcx, rsi
L0121: mov rdx, 0x7ff953af14c8
L012b: cmp [rcx], rdx
L012e: je short L013b
L0130: mov rcx, rdx
L0133: mov rdx, rsi
L0136: call System.Runtime.CompilerServices.CastHelpers.ChkCastClassSpecial(Void*, System.Object)
L013b: mov rcx, [rsi+0x18]
L013f: mov rdx, rdi
L0142: cmp [rcx], ecx
L0144: call Test+Test+LinearExpr.GetHashCode(System.Collections.IEqualityComparer)
L0149: lea ebp, [rax-0x61c88587]
L014f: mov rcx, [rsi+0x10]
L0153: mov rdx, rdi
L0156: cmp [rcx], ecx
L0158: call Test+Test+LinearExpr.GetHashCode(System.Collections.IEqualityComparer)
L015d: mov edx, ebp
L015f: shl edx, 6
L0162: add eax, edx
L0164: mov edx, ebp
L0166: sar edx, 2
L0169: lea ebp, [rax+rdx-0x61c88647]
L0170: mov eax, ebp
L0172: add rsp, 0x38
L0176: pop rbx
L0177: pop rbp
L0178: pop rsi
L0179: pop rdi
L017a: ret
L017b: xor eax, eax
L017d: add rsp, 0x38
L0181: pop rbx
L0182: pop rbp
L0183: pop rsi
L0184: pop rdi
L0185: ret

;Test+Test+LinearExpr.GetHashCode()
L0000: mov rdx, 0x191f2a86fa8
L000a: mov rdx, [rdx]
L000d: cmp [rcx], ecx
L000f: jmp Test+Test+LinearExpr.GetHashCode(System.Collections.IEqualityComparer)

;Test+Test+LinearExpr.Equals(System.Object, System.Collections.IEqualityComparer)
L0000: push r14
L0002: push rdi
L0003: push rsi
L0004: push rbp
L0005: push rbx
L0006: sub rsp, 0x30
L000a: vzeroupper
L000d: xor eax, eax
L000f: mov [rsp+0x20], rax
L0014: mov [rsp+0x28], rax
L0019: mov rsi, r8
L001c: mov rdi, rcx
L001f: test rdi, rdi
L0022: je L023a
L0028: mov rcx, 0x7ff953af0bf0
L0032: call System.Runtime.CompilerServices.CastHelpers.IsInstanceOfClass(Void*, System.Object)
L0037: mov rbx, rax
L003a: test rbx, rbx
L003d: je L022d
L0043: mov ebp, [rdi+8]
L0046: mov ecx, ebp
L0048: mov edx, [rbx+8]
L004b: cmp ecx, edx
L004d: jne L022d
L0053: cmp ebp, 3
L0056: ja L013a
L005c: mov ecx, ebp
L005e: lea rdx, [Test+Test+LinearExpr.Equals(System.Object, System.Collections.IEqualityComparer)]
L0065: mov edx, [rdx+rcx*4]
L0068: lea rax, [L001c]
L006f: add rdx, rax
L0072: jmp rdx
L0074: mov r14, rdi
L0077: mov rcx, 0x7ff953af1320
L0081: cmp [r14], rcx
L0084: je short L0091
L0086: mov rdx, rdi
L0089: call System.Runtime.CompilerServices.CastHelpers.ChkCastClassSpecial(Void*, System.Object)
L008e: mov r14, rax
L0091: mov rdi, rbx
L0094: mov rcx, 0x7ff953af1320
L009e: cmp [rdi], rcx
L00a1: je short L00ae
L00a3: mov rdx, rbx
L00a6: call System.Runtime.CompilerServices.CastHelpers.ChkCastClassSpecial(Void*, System.Object)
L00ab: mov rdi, rax
L00ae: vmovsd xmm0, [r14+0x18]
L00b4: vucomisd xmm0, [rdi+0x18]
L00b9: jp L022d
L00bf: jne L022d
L00c5: mov rcx, [r14+0x10]
L00c9: mov rdx, [rdi+0x10]
L00cd: mov rdi, rcx
L00d0: jmp L001f
L00d5: mov rbp, rdi
L00d8: mov rcx, 0x7ff953af14c8
L00e2: cmp [rbp], rcx
L00e6: je short L00f3
L00e8: mov rdx, rdi
L00eb: call System.Runtime.CompilerServices.CastHelpers.ChkCastClassSpecial(Void*, System.Object)
L00f0: mov rbp, rax
L00f3: mov rdi, rbx
L00f6: mov rcx, 0x7ff953af14c8
L0100: cmp [rdi], rcx
L0103: je short L0110
L0105: mov rdx, rbx
L0108: call System.Runtime.CompilerServices.CastHelpers.ChkCastClassSpecial(Void*, System.Object)
L010d: mov rdi, rax
L0110: mov rcx, [rbp+0x10]
L0114: mov rdx, [rdi+0x10]
L0118: mov r8, rsi
L011b: cmp [rcx], ecx
L011d: call Test+Test+LinearExpr.Equals(System.Object, System.Collections.IEqualityComparer)
L0122: test eax, eax
L0124: je L022d
L012a: mov rcx, [rbp+0x18]
L012e: mov rdx, [rdi+0x18]
L0132: mov rdi, rcx
L0135: jmp L001f
L013a: mov rbp, rdi
L013d: mov rcx, 0x7ff953af0ff8
L0147: cmp [rbp], rcx
L014b: je short L0158
L014d: mov rdx, rdi
L0150: call System.Runtime.CompilerServices.CastHelpers.ChkCastClassSpecial(Void*, System.Object)
L0155: mov rbp, rax
L0158: mov rax, rbx
L015b: mov rcx, 0x7ff953af0ff8
L0165: cmp [rax], rcx
L0168: je short L0172
L016a: mov rdx, rbx
L016d: call System.Runtime.CompilerServices.CastHelpers.ChkCastClassSpecial(Void*, System.Object)
L0172: vmovsd xmm0, [rbp+0x10]
L0177: vucomisd xmm0, [rax+0x10]
L017c: setnp r14b
L0180: jp short L0186
L0182: sete r14b
L0186: movzx r14d, r14b
L018a: jmp L0245
L018f: mov rbp, rdi
L0192: mov rcx, 0x7ff953af1178
L019c: cmp [rbp], rcx
L01a0: je short L01ad
L01a2: mov rdx, rdi
L01a5: call System.Runtime.CompilerServices.CastHelpers.ChkCastClassSpecial(Void*, System.Object)
L01aa: mov rbp, rax
L01ad: mov rdi, rbx
L01b0: mov rcx, 0x7ff953af1178
L01ba: cmp [rdi], rcx
L01bd: je short L01ca
L01bf: mov rdx, rbx
L01c2: call System.Runtime.CompilerServices.CastHelpers.ChkCastClassSpecial(Void*, System.Object)
L01c7: mov rdi, rax
L01ca: add rbp, 0x10
L01ce: mov rcx, [rbp]
L01d2: mov [rsp+0x20], rcx
L01d7: mov rcx, [rbp+8]
L01db: mov [rsp+0x28], rcx
L01e0: add rdi, 0x10
L01e4: mov rbx, [rdi]
L01e7: mov rdi, [rdi+8]
L01eb: mov rcx, 0x7ff953af0538
L01f5: call 0x00007ff9b182a470
L01fa: mov rbp, rax
L01fd: lea r14, [rbp+8]
L0201: mov rcx, r14
L0204: mov rdx, rbx
L0207: call 0x00007ff9b182a050
L020c: lea rcx, [r14+8]
L0210: mov rdx, rdi
L0213: call 0x00007ff9b182a050
L0218: mov rdx, rbp
L021b: lea rcx, [rsp+0x20]
L0220: mov r8, rsi
L0223: call Test+Test+Decision.Equals(System.Object, System.Collections.IEqualityComparer)
L0228: mov r14d, eax
L022b: jmp short L0245
L022d: xor eax, eax
L022f: add rsp, 0x30
L0233: pop rbx
L0234: pop rbp
L0235: pop rsi
L0236: pop rdi
L0237: pop r14
L0239: ret
L023a: test rdx, rdx
L023d: sete r14b
L0241: movzx r14d, r14b
L0245: movzx eax, r14b
L0249: add rsp, 0x30
L024d: pop rbx
L024e: pop rbp
L024f: pop rsi
L0250: pop rdi
L0251: pop r14
L0253: ret

;Test+Test+LinearExpr.op_Addition(Double, LinearExpr)
L0000: push rdi
L0001: push rsi
L0002: push rbx
L0003: sub rsp, 0x20
L0007: vzeroupper
L000a: mov rsi, rdx
L000d: vmovsd [rsp+0x40], xmm0
L0013: mov rcx, 0x7ff953af0ff8
L001d: call 0x00007ff9b182a470
L0022: mov rdi, rax
L0025: xor ecx, ecx
L0027: mov [rdi+8], ecx
L002a: vmovsd xmm0, [rsp+0x40]
L0030: vmovsd [rdi+0x10], xmm0
L0035: mov rcx, 0x7ff953af14c8
L003f: call 0x00007ff9b182a470
L0044: mov rbx, rax
L0047: mov dword ptr [rbx+8], 3
L004e: lea rcx, [rbx+0x10]
L0052: mov rdx, rdi
L0055: call 0x00007ff9b182a080
L005a: lea rcx, [rbx+0x18]
L005e: mov rdx, rsi
L0061: call 0x00007ff9b182a080
L0066: mov rax, rbx
L0069: add rsp, 0x20
L006d: pop rbx
L006e: pop rsi
L006f: pop rdi
L0070: ret

;Test+Test+LinearExpr.op_Addition(Decision, LinearExpr)
L0000: push rdi
L0001: push rsi
L0002: push rbp
L0003: push rbx
L0004: sub rsp, 0x28
L0008: mov rsi, rdx
L000b: mov rdi, [rcx]
L000e: mov rbx, [rcx+8]
L0012: mov rcx, 0x7ff953af1178
L001c: call 0x00007ff9b182a470
L0021: mov rbp, rax
L0024: mov rdx, rdi
L0027: mov dword ptr [rbp+8], 1
L002e: lea rdi, [rbp+0x10]
L0032: mov rcx, rdi
L0035: call 0x00007ff9b182a050
L003a: lea rcx, [rdi+8]
L003e: mov rdx, rbx
L0041: call 0x00007ff9b182a050
L0046: mov rcx, 0x7ff953af14c8
L0050: call 0x00007ff9b182a470
L0055: mov rdi, rax
L0058: mov dword ptr [rdi+8], 3
L005f: lea rcx, [rdi+0x10]
L0063: mov rdx, rbp
L0066: call 0x00007ff9b182a080
L006b: lea rcx, [rdi+0x18]
L006f: mov rdx, rsi
L0072: call 0x00007ff9b182a080
L0077: mov rax, rdi
L007a: add rsp, 0x28
L007e: pop rbx
L007f: pop rbp
L0080: pop rsi
L0081: pop rdi
L0082: ret

;Test+Test+LinearExpr.op_Addition(LinearExpr, LinearExpr)
L0000: push rdi
L0001: push rsi
L0002: push rbx
L0003: sub rsp, 0x20
L0007: mov rsi, rcx
L000a: mov rdi, rdx
L000d: mov rcx, 0x7ff953af14c8
L0017: call 0x00007ff9b182a470
L001c: mov rbx, rax
L001f: mov dword ptr [rbx+8], 3
L0026: lea rcx, [rbx+0x10]
L002a: mov rdx, rsi
L002d: call 0x00007ff9b182a080
L0032: lea rcx, [rbx+0x18]
L0036: mov rdx, rdi
L0039: call 0x00007ff9b182a080
L003e: mov rax, rbx
L0041: add rsp, 0x20
L0045: pop rbx
L0046: pop rsi
L0047: pop rdi
L0048: ret

;Test+Test+LinearExpr.op_Multiply(Double, LinearExpr)
L0000: push rdi
L0001: push rsi
L0002: sub rsp, 0x28
L0006: vzeroupper
L0009: mov rsi, rdx
L000c: vmovsd [rsp+0x40], xmm0
L0012: mov rcx, 0x7ff953af1320
L001c: call 0x00007ff9b182a470
L0021: mov rdi, rax
L0024: mov dword ptr [rdi+8], 2
L002b: vmovsd xmm0, [rsp+0x40]
L0031: vmovsd [rdi+0x18], xmm0
L0036: lea rcx, [rdi+0x10]
L003a: mov rdx, rsi
L003d: call 0x00007ff9b182a080
L0042: mov rax, rdi
L0045: add rsp, 0x28
L0049: pop rsi
L004a: pop rdi
L004b: ret

;Test+Test+LinearExpr.op_Multiply(LinearExpr, Double)
L0000: push rdi
L0001: push rsi
L0002: sub rsp, 0x28
L0006: vzeroupper
L0009: mov rsi, rcx
L000c: vmovsd [rsp+0x48], xmm1
L0012: mov rcx, 0x7ff953af1320
L001c: call 0x00007ff9b182a470
L0021: mov rdi, rax
L0024: mov dword ptr [rdi+8], 2
L002b: vmovsd xmm1, [rsp+0x48]
L0031: vmovsd [rdi+0x18], xmm1
L0036: lea rcx, [rdi+0x10]
L003a: mov rdx, rsi
L003d: call 0x00007ff9b182a080
L0042: mov rax, rdi
L0045: add rsp, 0x28
L0049: pop rsi
L004a: pop rdi
L004b: ret

;Test+Test+LinearExpr.get_Zero()
L0000: sub rsp, 0x28
L0004: vzeroupper
L0007: mov rcx, 0x7ff953af0ff8
L0011: call 0x00007ff9b182a470
L0016: xor edx, edx
L0018: mov [rax+8], edx
L001b: vxorps xmm0, xmm0, xmm0
L001f: vmovsd [rax+0x10], xmm0
L0024: add rsp, 0x28
L0028: ret

;Test+Test+LinearExpr.Equals(LinearExpr)
L0000: push rdi
L0001: push rsi
L0002: push rbx
L0003: sub rsp, 0x40
L0007: vzeroupper
L000a: vmovaps [rsp+0x30], xmm6
L0010: vmovaps [rsp+0x20], xmm7
L0016: mov rsi, rdx
L0019: mov rdx, rcx
L001c: test rdx, rdx
L001f: je L022c
L0025: test rsi, rsi
L0028: je L0216
L002e: mov edi, [rdx+8]
L0031: mov ecx, edi
L0033: mov eax, [rsi+8]
L0036: cmp ecx, eax
L0038: jne L0216
L003e: cmp edi, 3
L0041: ja L012e
L0047: mov ecx, edi
L0049: lea rax, [Test+Test+LinearExpr.Equals(LinearExpr)]
L0050: mov eax, [rax+rcx*4]
L0053: lea r8, [L0019]
L005a: add rax, r8
L005d: jmp rax
L005f: mov rbx, rdx
L0062: mov rcx, 0x7ff953af1320
L006c: cmp [rbx], rcx
L006f: je short L0079
L0071: call System.Runtime.CompilerServices.CastHelpers.ChkCastClassSpecial(Void*, System.Object)
L0076: mov rbx, rax
L0079: mov rdi, rsi
L007c: mov rcx, 0x7ff953af1320
L0086: cmp [rdi], rcx
L0089: je short L0096
L008b: mov rdx, rsi
L008e: call System.Runtime.CompilerServices.CastHelpers.ChkCastClassSpecial(Void*, System.Object)
L0093: mov rdi, rax
L0096: vmovsd xmm6, [rbx+0x18]
L009b: vmovsd xmm7, [rdi+0x18]
L00a0: vucomisd xmm6, xmm7
L00a4: jp short L00a8
L00a6: je short L00c6
L00a8: vucomisd xmm6, xmm6
L00ac: jp short L00b4
L00ae: je L0216
L00b4: vucomisd xmm7, xmm7
L00b8: setp cl
L00bb: movzx ecx, cl
L00be: test ecx, ecx
L00c0: je L0216
L00c6: mov rdx, [rbx+0x10]
L00ca: mov rsi, [rdi+0x10]
L00ce: jmp L001c
L00d3: mov rdi, rdx
L00d6: mov rcx, 0x7ff953af14c8
L00e0: cmp [rdi], rcx
L00e3: je short L00ed
L00e5: call System.Runtime.CompilerServices.CastHelpers.ChkCastClassSpecial(Void*, System.Object)
L00ea: mov rdi, rax
L00ed: mov rbx, rsi
L00f0: mov rcx, 0x7ff953af14c8
L00fa: cmp [rbx], rcx
L00fd: je short L010a
L00ff: mov rdx, rsi
L0102: call System.Runtime.CompilerServices.CastHelpers.ChkCastClassSpecial(Void*, System.Object)
L0107: mov rbx, rax
L010a: mov rcx, [rdi+0x10]
L010e: mov rdx, [rbx+0x10]
L0112: cmp [rcx], ecx
L0114: call Test+Test+LinearExpr.Equals(LinearExpr)
L0119: test eax, eax
L011b: je L0216
L0121: mov rdx, [rdi+0x18]
L0125: mov rsi, [rbx+0x18]
L0129: jmp L001c
L012e: mov rdi, rdx
L0131: mov rcx, 0x7ff953af0ff8
L013b: cmp [rdi], rcx
L013e: je short L0148
L0140: call System.Runtime.CompilerServices.CastHelpers.ChkCastClassSpecial(Void*, System.Object)
L0145: mov rdi, rax
L0148: mov rax, rsi
L014b: mov rcx, 0x7ff953af0ff8
L0155: cmp [rax], rcx
L0158: je short L0162
L015a: mov rdx, rsi
L015d: call System.Runtime.CompilerServices.CastHelpers.ChkCastClassSpecial(Void*, System.Object)
L0162: vmovsd xmm6, [rdi+0x10]
L0167: vmovsd xmm7, [rax+0x10]
L016c: vucomisd xmm6, xmm7
L0170: jp short L018d
L0172: jne short L018d
L0174: mov eax, 1
L0179: vmovaps xmm6, [rsp+0x30]
L017f: vmovaps xmm7, [rsp+0x20]
L0185: add rsp, 0x40
L0189: pop rbx
L018a: pop rsi
L018b: pop rdi
L018c: ret
L018d: vucomisd xmm6, xmm6
L0191: jp short L0199
L0193: je L0216
L0199: vucomisd xmm7, xmm7
L019d: setp bl
L01a0: movzx ebx, bl
L01a3: jmp L0235
L01a8: mov rdi, rdx
L01ab: mov rcx, 0x7ff953af1178
L01b5: cmp [rdi], rcx
L01b8: je short L01c2
L01ba: call System.Runtime.CompilerServices.CastHelpers.ChkCastClassSpecial(Void*, System.Object)
L01bf: mov rdi, rax
L01c2: mov rbx, rsi
L01c5: mov rcx, 0x7ff953af1178
L01cf: cmp [rbx], rcx
L01d2: je short L01df
L01d4: mov rdx, rsi
L01d7: call System.Runtime.CompilerServices.CastHelpers.ChkCastClassSpecial(Void*, System.Object)
L01dc: mov rbx, rax
L01df: add rdi, 0x10
L01e3: mov rcx, [rdi]
L01e6: mov rsi, [rdi+8]
L01ea: add rbx, 0x10
L01ee: mov rdx, [rbx]
L01f1: mov rdi, [rbx+8]
L01f5: cmp [rcx], ecx
L01f7: call Test+Test+DecisionName.Equals(DecisionName)
L01fc: test eax, eax
L01fe: je short L0212
L0200: mov rcx, rsi
L0203: mov rdx, rdi
L0206: cmp [rcx], ecx
L0208: call Test+Test+DecisionType.Equals(DecisionType)
L020d: movzx ebx, al
L0210: jmp short L0214
L0212: xor ebx, ebx
L0214: jmp short L0235
L0216: xor eax, eax
L0218: vmovaps xmm6, [rsp+0x30]
L021e: vmovaps xmm7, [rsp+0x20]
L0224: add rsp, 0x40
L0228: pop rbx
L0229: pop rsi
L022a: pop rdi
L022b: ret
L022c: test rsi, rsi
L022f: sete bl
L0232: movzx ebx, bl
L0235: movzx eax, bl
L0238: vmovaps xmm6, [rsp+0x30]
L023e: vmovaps xmm7, [rsp+0x20]
L0244: add rsp, 0x40
L0248: pop rbx
L0249: pop rsi
L024a: pop rdi
L024b: ret

;Test+Test+LinearExpr.Equals(System.Object)
L0000: push rsi
L0001: sub rsp, 0x20
L0005: mov rsi, rcx
L0008: mov rcx, 0x7ff953af0bf0
L0012: call System.Runtime.CompilerServices.CastHelpers.IsInstanceOfClass(Void*, System.Object)
L0017: test rax, rax
L001a: je short L002e
L001c: mov rcx, rsi
L001f: mov rdx, rax
L0022: cmp [rcx], ecx
L0024: add rsp, 0x20
L0028: pop rsi
L0029: jmp Test+Test+LinearExpr.Equals(LinearExpr)
L002e: xor eax, eax
L0030: add rsp, 0x20
L0034: pop rsi
L0035: ret

Test+Test+SliceMap`2..ctor(System.Collections.Generic.IComparer`1<!0>, System.ReadOnlyMemory`1<!0>, System.ReadOnlyMemory`1<!1>)
; generic method cannot be jitted. provide explicit types

Test+Test+SliceMap`2..ctor(System.Collections.Generic.IEnumerable`1<System.Tuple`2<!0,!1>>)
; generic method cannot be jitted. provide explicit types

Test+Test+SliceMap`2.get_Keys()
; generic method cannot be jitted. provide explicit types

Test+Test+SliceMap`2.get_Values()
; generic method cannot be jitted. provide explicit types

Test+Test+SliceMap`2.get_Comparer()
; generic method cannot be jitted. provide explicit types

;Test+Test+DecisionType.get_Boolean()
L0000: sub rsp, 0x28
L0004: mov rcx, 0x7ff953a2d838
L000e: xor edx, edx
L0010: call 0x00007ff9b1968990
L0015: mov rax, [rax]
L0018: add rsp, 0x28
L001c: ret

;Test+Test+DecisionType.get_IsBoolean()
L0000: sub rsp, 0x28
L0004: mov rdx, rcx
L0007: mov rcx, 0x7ff953a2e558
L0011: call System.Runtime.CompilerServices.CastHelpers.IsInstanceOfClass(Void*, System.Object)
L0016: test rax, rax
L0019: setne al
L001c: movzx eax, al
L001f: add rsp, 0x28
L0023: ret

;Test+Test+DecisionType.NewInteger(Double, Double)
L0000: sub rsp, 0x28
L0004: vzeroupper
L0007: vmovsd [rsp+0x30], xmm0
L000d: vmovsd [rsp+0x38], xmm1
L0013: mov rcx, 0x7ff953a2e700
L001d: call 0x00007ff9b182a470
L0022: vmovsd xmm0, [rsp+0x30]
L0028: vmovsd [rax+8], xmm0
L002d: vmovsd xmm1, [rsp+0x38]
L0033: vmovsd [rax+0x10], xmm1
L0038: add rsp, 0x28
L003c: ret

;Test+Test+DecisionType.get_IsInteger()
L0000: sub rsp, 0x28
L0004: mov rdx, rcx
L0007: mov rcx, 0x7ff953a2e700
L0011: call System.Runtime.CompilerServices.CastHelpers.IsInstanceOfClass(Void*, System.Object)
L0016: test rax, rax
L0019: setne al
L001c: movzx eax, al
L001f: add rsp, 0x28
L0023: ret

;Test+Test+DecisionType.NewContinuous(Double, Double)
L0000: sub rsp, 0x28
L0004: vzeroupper
L0007: vmovsd [rsp+0x30], xmm0
L000d: vmovsd [rsp+0x38], xmm1
L0013: mov rcx, 0x7ff953a2e8a8
L001d: call 0x00007ff9b182a470
L0022: vmovsd xmm0, [rsp+0x30]
L0028: vmovsd [rax+8], xmm0
L002d: vmovsd xmm1, [rsp+0x38]
L0033: vmovsd [rax+0x10], xmm1
L0038: add rsp, 0x28
L003c: ret

;Test+Test+DecisionType.get_IsContinuous()
L0000: sub rsp, 0x28
L0004: mov rdx, rcx
L0007: mov rcx, 0x7ff953a2e8a8
L0011: call System.Runtime.CompilerServices.CastHelpers.IsInstanceOfClass(Void*, System.Object)
L0016: test rax, rax
L0019: setne al
L001c: movzx eax, al
L001f: add rsp, 0x28
L0023: ret

;Test+Test+DecisionType.get_Tag()
L0000: push rsi
L0001: sub rsp, 0x20
L0005: mov rsi, rcx
L0008: mov rdx, rsi
L000b: mov rcx, 0x7ff953a2e8a8
L0015: call System.Runtime.CompilerServices.CastHelpers.IsInstanceOfClass(Void*, System.Object)
L001a: test rax, rax
L001d: je short L0026
L001f: mov eax, 2
L0024: jmp short L0046
L0026: mov rdx, rsi
L0029: mov rcx, 0x7ff953a2e700
L0033: call System.Runtime.CompilerServices.CastHelpers.IsInstanceOfClass(Void*, System.Object)
L0038: test rax, rax
L003b: je short L0044
L003d: mov eax, 1
L0042: jmp short L0046
L0044: xor eax, eax
L0046: add rsp, 0x20
L004a: pop rsi
L004b: ret

;Test+Test+DecisionType.ToString()
L0000: push rdi
L0001: push rsi
L0002: sub rsp, 0x28
L0006: mov rsi, rcx
L0009: mov rcx, 0x7ff953af5d58
L0013: call 0x00007ff9b182a470
L0018: mov rdi, rax
L001b: mov rdx, 0x191f2a9b3f0
L0025: mov rdx, [rdx]
L0028: lea rcx, [rdi+8]
L002c: call 0x00007ff9b182a080
L0031: xor edx, edx
L0033: mov [rdi+0x10], rdx
L0037: mov [rdi+0x18], rdx
L003b: mov rdx, rdi
L003e: mov rcx, 0x7ff953af6038
L0048: call Microsoft.FSharp.Core.PrintfModule.PrintFormatToStringThen[[System.__Canon, System.Private.CoreLib]](Microsoft.FSharp.Core.PrintfFormat`4<System.__Canon,Microsoft.FSharp.Core.Unit,System.String,System.String>)
L004d: mov rcx, rax
L0050: mov rdx, rsi
L0053: mov rax, [rax]
L0056: mov rax, [rax+0x40]
L005a: mov rax, [rax+0x20]
L005e: add rsp, 0x28
L0062: pop rsi
L0063: pop rdi
L0064: jmp rax

;Test+Test+DecisionType.CompareTo(DecisionType)
L0000: push rdi
L0001: push rsi
L0002: push rbp
L0003: push rbx
L0004: sub rsp, 0x28
L0008: mov rdi, rcx
L000b: mov rsi, rdx
L000e: test rdi, rdi
L0011: je L00ca
L0017: test rsi, rsi
L001a: je L00bc
L0020: mov rdx, rdi
L0023: mov rcx, 0x7ff953a2e8a8
L002d: call System.Runtime.CompilerServices.CastHelpers.IsInstanceOfClass(Void*, System.Object)
L0032: test rax, rax
L0035: je short L003e
L0037: mov ebx, 2
L003c: jmp short L005e
L003e: mov rdx, rdi
L0041: mov rcx, 0x7ff953a2e700
L004b: call System.Runtime.CompilerServices.CastHelpers.IsInstanceOfClass(Void*, System.Object)
L0050: test rax, rax
L0053: je short L005c
L0055: mov ebx, 1
L005a: jmp short L005e
L005c: xor ebx, ebx
L005e: mov rdx, rsi
L0061: mov rcx, 0x7ff953a2e8a8
L006b: call System.Runtime.CompilerServices.CastHelpers.IsInstanceOfClass(Void*, System.Object)
L0070: test rax, rax
L0073: je short L007c
L0075: mov ebp, 2
L007a: jmp short L009c
L007c: mov rdx, rsi
L007f: mov rcx, 0x7ff953a2e700
L0089: call System.Runtime.CompilerServices.CastHelpers.IsInstanceOfClass(Void*, System.Object)
L008e: test rax, rax
L0091: je short L009a
L0093: mov ebp, 1
L0098: jmp short L009c
L009a: xor ebp, ebp
L009c: cmp ebx, ebp
L009e: jne short L00b6
L00a0: mov rcx, rdi
L00a3: mov rdx, rsi
L00a6: xor r8d, r8d
L00a9: add rsp, 0x28
L00ad: pop rbx
L00ae: pop rbp
L00af: pop rsi
L00b0: pop rdi
L00b1: jmp Test+Test.CompareTo$cont@6(DecisionType, DecisionType, Microsoft.FSharp.Core.Unit)
L00b6: mov eax, ebx
L00b8: sub eax, ebp
L00ba: jmp short L00e8
L00bc: mov eax, 1
L00c1: add rsp, 0x28
L00c5: pop rbx
L00c6: pop rbp
L00c7: pop rsi
L00c8: pop rdi
L00c9: ret
L00ca: test rsi, rsi
L00cd: je short L00dd
L00cf: mov eax, 0xffffffff
L00d4: add rsp, 0x28
L00d8: pop rbx
L00d9: pop rbp
L00da: pop rsi
L00db: pop rdi
L00dc: ret
L00dd: xor eax, eax
L00df: add rsp, 0x28
L00e3: pop rbx
L00e4: pop rbp
L00e5: pop rsi
L00e6: pop rdi
L00e7: ret
L00e8: add rsp, 0x28
L00ec: pop rbx
L00ed: pop rbp
L00ee: pop rsi
L00ef: pop rdi
L00f0: ret

;Test+Test+DecisionType.CompareTo(System.Object)
L0000: push rsi
L0001: sub rsp, 0x20
L0005: mov rsi, rcx
L0008: mov rax, rdx
L000b: test rax, rax
L000e: je short L0024
L0010: mov rcx, 0x7ff953a2e0b8
L001a: cmp [rax], rcx
L001d: je short L0024
L001f: call System.Runtime.CompilerServices.CastHelpers.ChkCastClassSpecial(Void*, System.Object)
L0024: mov rcx, rsi
L0027: mov rdx, rax
L002a: cmp [rcx], ecx
L002c: add rsp, 0x20
L0030: pop rsi
L0031: jmp Test+Test+DecisionType.CompareTo(DecisionType)

;Test+Test+DecisionType.CompareTo(System.Object, System.Collections.IComparer)
L0000: push r14
L0002: push rdi
L0003: push rsi
L0004: push rbp
L0005: push rbx
L0006: sub rsp, 0x20
L000a: mov rdi, rcx
L000d: mov rsi, rdx
L0010: mov rbx, r8
L0013: mov rcx, rsi
L0016: test rcx, rcx
L0019: je short L0035
L001b: mov rdx, 0x7ff953a2e0b8
L0025: cmp [rcx], rdx
L0028: je short L0035
L002a: mov rcx, rdx
L002d: mov rdx, rsi
L0030: call System.Runtime.CompilerServices.CastHelpers.ChkCastClassSpecial(Void*, System.Object)
L0035: mov rbp, rsi
L0038: test rdi, rdi
L003b: je L011d
L0041: test rbp, rbp
L0044: je short L005e
L0046: mov rcx, 0x7ff953a2e0b8
L0050: cmp [rbp], rcx
L0054: je short L005e
L0056: mov rdx, rbp
L0059: call System.Runtime.CompilerServices.CastHelpers.ChkCastClassSpecial(Void*, System.Object)
L005e: test rbp, rbp
L0061: je L010d
L0067: mov rdx, rdi
L006a: mov rcx, 0x7ff953a2e8a8
L0074: call System.Runtime.CompilerServices.CastHelpers.IsInstanceOfClass(Void*, System.Object)
L0079: test rax, rax
L007c: je short L0085
L007e: mov esi, 2
L0083: jmp short L00a5
L0085: mov rdx, rdi
L0088: mov rcx, 0x7ff953a2e700
L0092: call System.Runtime.CompilerServices.CastHelpers.IsInstanceOfClass(Void*, System.Object)
L0097: test rax, rax
L009a: je short L00a3
L009c: mov esi, 1
L00a1: jmp short L00a5
L00a3: xor esi, esi
L00a5: mov rdx, rbp
L00a8: mov rcx, 0x7ff953a2e8a8
L00b2: call System.Runtime.CompilerServices.CastHelpers.IsInstanceOfClass(Void*, System.Object)
L00b7: test rax, rax
L00ba: je short L00c4
L00bc: mov r14d, 2
L00c2: jmp short L00e6
L00c4: mov rdx, rbp
L00c7: mov rcx, 0x7ff953a2e700
L00d1: call System.Runtime.CompilerServices.CastHelpers.IsInstanceOfClass(Void*, System.Object)
L00d6: test rax, rax
L00d9: je short L00e3
L00db: mov r14d, 1
L00e1: jmp short L00e6
L00e3: xor r14d, r14d
L00e6: cmp esi, r14d
L00e9: jne short L0106
L00eb: mov rcx, rbx
L00ee: mov rdx, rdi
L00f1: mov r8, rbp
L00f4: xor r9d, r9d
L00f7: add rsp, 0x20
L00fb: pop rbx
L00fc: pop rbp
L00fd: pop rsi
L00fe: pop rdi
L00ff: pop r14
L0101: jmp Test+Test.CompareTo$cont@6-1(System.Collections.IComparer, DecisionType, DecisionType, Microsoft.FSharp.Core.Unit)
L0106: mov eax, esi
L0108: sub eax, r14d
L010b: jmp short L015e
L010d: mov eax, 1
L0112: add rsp, 0x20
L0116: pop rbx
L0117: pop rbp
L0118: pop rsi
L0119: pop rdi
L011a: pop r14
L011c: ret
L011d: mov rax, rsi
L0120: test rax, rax
L0123: je short L013c
L0125: mov rcx, 0x7ff953a2e0b8
L012f: cmp [rax], rcx
L0132: je short L013c
L0134: mov rdx, rsi
L0137: call System.Runtime.CompilerServices.CastHelpers.ChkCastClassSpecial(Void*, System.Object)
L013c: test rax, rax
L013f: je short L0151
L0141: mov eax, 0xffffffff
L0146: add rsp, 0x20
L014a: pop rbx
L014b: pop rbp
L014c: pop rsi
L014d: pop rdi
L014e: pop r14
L0150: ret
L0151: xor eax, eax
L0153: add rsp, 0x20
L0157: pop rbx
L0158: pop rbp
L0159: pop rsi
L015a: pop rdi
L015b: pop r14
L015d: ret
L015e: add rsp, 0x20
L0162: pop rbx
L0163: pop rbp
L0164: pop rsi
L0165: pop rdi
L0166: pop r14
L0168: ret

;Test+Test+DecisionType.GetHashCode(System.Collections.IEqualityComparer)
L0000: push rdi
L0001: push rsi
L0002: push rbx
L0003: sub rsp, 0x20
L0007: vzeroupper
L000a: mov rsi, rcx
L000d: mov rdi, rdx
L0010: test rsi, rsi
L0013: je L0122
L0019: mov rdx, rsi
L001c: mov rcx, 0x7ff953a2e700
L0026: call System.Runtime.CompilerServices.CastHelpers.IsInstanceOfClass(Void*, System.Object)
L002b: mov rbx, rax
L002e: test rbx, rbx
L0031: jne short L0070
L0033: mov rdx, rsi
L0036: mov rcx, 0x7ff953a2e8a8
L0040: call System.Runtime.CompilerServices.CastHelpers.IsInstanceOfClass(Void*, System.Object)
L0045: test rax, rax
L0048: jne L00c8
L004e: test rax, rax
L0051: je short L005a
L0053: mov eax, 2
L0058: jmp short L0068
L005a: test rbx, rbx
L005d: je short L0066
L005f: mov eax, 1
L0064: jmp short L0068
L0066: xor eax, eax
L0068: add rsp, 0x20
L006c: pop rbx
L006d: pop rsi
L006e: pop rdi
L006f: ret
L0070: mov rcx, rsi
L0073: mov rdx, 0x7ff953a2e700
L007d: cmp [rcx], rdx
L0080: je short L008d
L0082: mov rcx, rdx
L0085: mov rdx, rsi
L0088: call System.Runtime.CompilerServices.CastHelpers.ChkCastClassSpecial(Void*, System.Object)
L008d: vmovsd xmm1, [rsi+0x10]
L0092: mov rcx, rdi
L0095: call Microsoft.FSharp.Core.LanguagePrimitives+HashCompare.GenericHashWithComparerIntrinsic[[System.Double, System.Private.CoreLib]](System.Collections.IEqualityComparer, Double)
L009a: lea ebx, [rax-0x61c88607]
L00a0: vmovsd xmm1, [rsi+8]
L00a5: mov rcx, rdi
L00a8: call Microsoft.FSharp.Core.LanguagePrimitives+HashCompare.GenericHashWithComparerIntrinsic[[System.Double, System.Private.CoreLib]](System.Collections.IEqualityComparer, Double)
L00ad: mov ecx, ebx
L00af: shl ecx, 6
L00b2: add eax, ecx
L00b4: sar ebx, 2
L00b7: lea ebx, [rax+rbx-0x61c88647]
L00be: mov eax, ebx
L00c0: add rsp, 0x20
L00c4: pop rbx
L00c5: pop rsi
L00c6: pop rdi
L00c7: ret
L00c8: mov rcx, rsi
L00cb: mov rdx, 0x7ff953a2e8a8
L00d5: cmp [rcx], rdx
L00d8: je short L00e5
L00da: mov rcx, rdx
L00dd: mov rdx, rsi
L00e0: call System.Runtime.CompilerServices.CastHelpers.ChkCastClassSpecial(Void*, System.Object)
L00e5: vmovsd xmm1, [rsi+0x10]
L00ea: mov rcx, rdi
L00ed: call Microsoft.FSharp.Core.LanguagePrimitives+HashCompare.GenericHashWithComparerIntrinsic[[System.Double, System.Private.CoreLib]](System.Collections.IEqualityComparer, Double)
L00f2: lea ebx, [rax-0x61c885c7]
L00f8: vmovsd xmm1, [rsi+8]
L00fd: mov rcx, rdi
L0100: call Microsoft.FSharp.Core.LanguagePrimitives+HashCompare.GenericHashWithComparerIntrinsic[[System.Double, System.Private.CoreLib]](System.Collections.IEqualityComparer, Double)
L0105: mov edx, ebx
L0107: shl edx, 6
L010a: add eax, edx
L010c: mov edx, ebx
L010e: sar edx, 2
L0111: lea ebx, [rax+rdx-0x61c88647]
L0118: mov eax, ebx
L011a: add rsp, 0x20
L011e: pop rbx
L011f: pop rsi
L0120: pop rdi
L0121: ret
L0122: xor eax, eax
L0124: add rsp, 0x20
L0128: pop rbx
L0129: pop rsi
L012a: pop rdi
L012b: ret

;Test+Test+DecisionType.GetHashCode()
L0000: mov rdx, 0x191f2a86fa8
L000a: mov rdx, [rdx]
L000d: cmp [rcx], ecx
L000f: jmp Test+Test+DecisionType.GetHashCode(System.Collections.IEqualityComparer)

;Test+Test+DecisionType.Equals(System.Object, System.Collections.IEqualityComparer)
L0000: push r14
L0002: push rdi
L0003: push rsi
L0004: push rbp
L0005: push rbx
L0006: sub rsp, 0x20
L000a: vzeroupper
L000d: mov rsi, rcx
L0010: test rsi, rsi
L0013: je L01a1
L0019: mov rcx, 0x7ff953a2e0b8
L0023: call System.Runtime.CompilerServices.CastHelpers.IsInstanceOfClass(Void*, System.Object)
L0028: mov rdi, rax
L002b: test rdi, rdi
L002e: je L0194
L0034: mov rdx, rsi
L0037: mov rcx, 0x7ff953a2e8a8
L0041: call System.Runtime.CompilerServices.CastHelpers.IsInstanceOfClass(Void*, System.Object)
L0046: mov rbx, rax
L0049: test rbx, rbx
L004c: je short L0055
L004e: mov ebp, 2
L0053: jmp short L0075
L0055: mov rdx, rsi
L0058: mov rcx, 0x7ff953a2e700
L0062: call System.Runtime.CompilerServices.CastHelpers.IsInstanceOfClass(Void*, System.Object)
L0067: test rax, rax
L006a: je short L0073
L006c: mov ebp, 1
L0071: jmp short L0075
L0073: xor ebp, ebp
L0075: mov rdx, rdi
L0078: mov rcx, 0x7ff953a2e8a8
L0082: call System.Runtime.CompilerServices.CastHelpers.IsInstanceOfClass(Void*, System.Object)
L0087: test rax, rax
L008a: je short L0094
L008c: mov r14d, 2
L0092: jmp short L00b6
L0094: mov rdx, rdi
L0097: mov rcx, 0x7ff953a2e700
L00a1: call System.Runtime.CompilerServices.CastHelpers.IsInstanceOfClass(Void*, System.Object)
L00a6: test rax, rax
L00a9: je short L00b3
L00ab: mov r14d, 1
L00b1: jmp short L00b6
L00b3: xor r14d, r14d
L00b6: cmp ebp, r14d
L00b9: jne L0194
L00bf: mov rdx, rsi
L00c2: mov rcx, 0x7ff953a2e700
L00cc: call System.Runtime.CompilerServices.CastHelpers.IsInstanceOfClass(Void*, System.Object)
L00d1: test rax, rax
L00d4: jne short L00eb
L00d6: test rbx, rbx
L00d9: jne short L0135
L00db: mov eax, 1
L00e0: add rsp, 0x20
L00e4: pop rbx
L00e5: pop rbp
L00e6: pop rsi
L00e7: pop rdi
L00e8: pop r14
L00ea: ret
L00eb: mov rbx, rdi
L00ee: mov rcx, 0x7ff953a2e700
L00f8: cmp [rbx], rcx
L00fb: je short L0108
L00fd: mov rdx, rdi
L0100: call System.Runtime.CompilerServices.CastHelpers.ChkCastClassSpecial(Void*, System.Object)
L0105: mov rbx, rax
L0108: vmovsd xmm0, [rsi+8]
L010d: vucomisd xmm0, [rbx+8]
L0112: jp L0194
L0118: jne L0194
L011e: vmovsd xmm0, [rsi+0x10]
L0123: vucomisd xmm0, [rbx+0x10]
L0128: setnp bl
L012b: jp short L0130
L012d: sete bl
L0130: movzx ebx, bl
L0133: jmp short L01aa
L0135: mov rcx, rsi
L0138: mov rdx, 0x7ff953a2e8a8
L0142: cmp [rcx], rdx
L0145: je short L0152
L0147: mov rcx, rdx
L014a: mov rdx, rsi
L014d: call System.Runtime.CompilerServices.CastHelpers.ChkCastClassSpecial(Void*, System.Object)
L0152: mov rbx, rdi
L0155: mov rcx, 0x7ff953a2e8a8
L015f: cmp [rbx], rcx
L0162: je short L016f
L0164: mov rdx, rdi
L0167: call System.Runtime.CompilerServices.CastHelpers.ChkCastClassSpecial(Void*, System.Object)
L016c: mov rbx, rax
L016f: vmovsd xmm0, [rsi+8]
L0174: vucomisd xmm0, [rbx+8]
L0179: jp short L0194
L017b: jne short L0194
L017d: vmovsd xmm0, [rsi+0x10]
L0182: vucomisd xmm0, [rbx+0x10]
L0187: setnp bl
L018a: jp short L018f
L018c: sete bl
L018f: movzx ebx, bl
L0192: jmp short L01aa
L0194: xor eax, eax
L0196: add rsp, 0x20
L019a: pop rbx
L019b: pop rbp
L019c: pop rsi
L019d: pop rdi
L019e: pop r14
L01a0: ret
L01a1: test rdx, rdx
L01a4: sete bl
L01a7: movzx ebx, bl
L01aa: movzx eax, bl
L01ad: add rsp, 0x20
L01b1: pop rbx
L01b2: pop rbp
L01b3: pop rsi
L01b4: pop rdi
L01b5: pop r14
L01b7: ret

;Test+Test+DecisionType.Equals(DecisionType)
L0000: push rdi
L0001: push rsi
L0002: push rbp
L0003: push rbx
L0004: sub rsp, 0x28
L0008: mov rdi, rcx
L000b: mov rsi, rdx
L000e: test rdi, rdi
L0011: je L00cc
L0017: test rsi, rsi
L001a: je L00c1
L0020: mov rdx, rdi
L0023: mov rcx, 0x7ff953a2e8a8
L002d: call System.Runtime.CompilerServices.CastHelpers.IsInstanceOfClass(Void*, System.Object)
L0032: test rax, rax
L0035: je short L003e
L0037: mov ebx, 2
L003c: jmp short L005e
L003e: mov rdx, rdi
L0041: mov rcx, 0x7ff953a2e700
L004b: call System.Runtime.CompilerServices.CastHelpers.IsInstanceOfClass(Void*, System.Object)
L0050: test rax, rax
L0053: je short L005c
L0055: mov ebx, 1
L005a: jmp short L005e
L005c: xor ebx, ebx
L005e: mov rdx, rsi
L0061: mov rcx, 0x7ff953a2e8a8
L006b: call System.Runtime.CompilerServices.CastHelpers.IsInstanceOfClass(Void*, System.Object)
L0070: test rax, rax
L0073: je short L007c
L0075: mov ebp, 2
L007a: jmp short L009c
L007c: mov rdx, rsi
L007f: mov rcx, 0x7ff953a2e700
L0089: call System.Runtime.CompilerServices.CastHelpers.IsInstanceOfClass(Void*, System.Object)
L008e: test rax, rax
L0091: je short L009a
L0093: mov ebp, 1
L0098: jmp short L009c
L009a: xor ebp, ebp
L009c: cmp ebx, ebp
L009e: jne short L00b6
L00a0: mov rcx, rdi
L00a3: mov rdx, rsi
L00a6: xor r8d, r8d
L00a9: add rsp, 0x28
L00ad: pop rbx
L00ae: pop rbp
L00af: pop rsi
L00b0: pop rdi
L00b1: jmp Test+Test.Equals$cont@6(DecisionType, DecisionType, Microsoft.FSharp.Core.Unit)
L00b6: xor eax, eax
L00b8: add rsp, 0x28
L00bc: pop rbx
L00bd: pop rbp
L00be: pop rsi
L00bf: pop rdi
L00c0: ret
L00c1: xor eax, eax
L00c3: add rsp, 0x28
L00c7: pop rbx
L00c8: pop rbp
L00c9: pop rsi
L00ca: pop rdi
L00cb: ret
L00cc: test rsi, rsi
L00cf: sete al
L00d2: movzx eax, al
L00d5: add rsp, 0x28
L00d9: pop rbx
L00da: pop rbp
L00db: pop rsi
L00dc: pop rdi
L00dd: ret

;Test+Test+DecisionType.Equals(System.Object)
L0000: push rsi
L0001: sub rsp, 0x20
L0005: mov rsi, rcx
L0008: mov rcx, 0x7ff953a2e0b8
L0012: call System.Runtime.CompilerServices.CastHelpers.IsInstanceOfClass(Void*, System.Object)
L0017: test rax, rax
L001a: je short L002e
L001c: mov rcx, rsi
L001f: mov rdx, rax
L0022: cmp [rcx], ecx
L0024: add rsp, 0x20
L0028: pop rsi
L0029: jmp Test+Test+DecisionType.Equals(DecisionType)
L002e: xor eax, eax
L0030: add rsp, 0x20
L0034: pop rsi
L0035: ret

;Test+Test+DecisionType+Integer.get_LowerBound()
L0000: vzeroupper
L0003: vmovsd xmm0, [rcx+8]
L0008: ret

;Test+Test+DecisionType+Integer.get_UpperBound()
L0000: vzeroupper
L0003: vmovsd xmm0, [rcx+0x10]
L0008: ret

;Test+Test+DecisionType.get_IsBoolean()
L0000: sub rsp, 0x28
L0004: mov rdx, rcx
L0007: mov rcx, 0x7ff953a2e558
L0011: call System.Runtime.CompilerServices.CastHelpers.IsInstanceOfClass(Void*, System.Object)
L0016: test rax, rax
L0019: setne al
L001c: movzx eax, al
L001f: add rsp, 0x28
L0023: ret

;Test+Test+DecisionType.get_IsInteger()
L0000: sub rsp, 0x28
L0004: mov rdx, rcx
L0007: mov rcx, 0x7ff953a2e700
L0011: call System.Runtime.CompilerServices.CastHelpers.IsInstanceOfClass(Void*, System.Object)
L0016: test rax, rax
L0019: setne al
L001c: movzx eax, al
L001f: add rsp, 0x28
L0023: ret

;Test+Test+DecisionType.get_IsContinuous()
L0000: sub rsp, 0x28
L0004: mov rdx, rcx
L0007: mov rcx, 0x7ff953a2e8a8
L0011: call System.Runtime.CompilerServices.CastHelpers.IsInstanceOfClass(Void*, System.Object)
L0016: test rax, rax
L0019: setne al
L001c: movzx eax, al
L001f: add rsp, 0x28
L0023: ret

;Test+Test+DecisionType.get_Tag()
L0000: push rsi
L0001: sub rsp, 0x20
L0005: mov rsi, rcx
L0008: mov rdx, rsi
L000b: mov rcx, 0x7ff953a2e8a8
L0015: call System.Runtime.CompilerServices.CastHelpers.IsInstanceOfClass(Void*, System.Object)
L001a: test rax, rax
L001d: je short L0026
L001f: mov eax, 2
L0024: jmp short L0046
L0026: mov rdx, rsi
L0029: mov rcx, 0x7ff953a2e700
L0033: call System.Runtime.CompilerServices.CastHelpers.IsInstanceOfClass(Void*, System.Object)
L0038: test rax, rax
L003b: je short L0044
L003d: mov eax, 1
L0042: jmp short L0046
L0044: xor eax, eax
L0046: add rsp, 0x20
L004a: pop rsi
L004b: ret

;Test+Test+DecisionType.ToString()
L0000: push rdi
L0001: push rsi
L0002: sub rsp, 0x28
L0006: mov rsi, rcx
L0009: mov rcx, 0x7ff953af5d58
L0013: call 0x00007ff9b182a470
L0018: mov rdi, rax
L001b: mov rdx, 0x191f2a9b3f0
L0025: mov rdx, [rdx]
L0028: lea rcx, [rdi+8]
L002c: call 0x00007ff9b182a080
L0031: xor edx, edx
L0033: mov [rdi+0x10], rdx
L0037: mov [rdi+0x18], rdx
L003b: mov rdx, rdi
L003e: mov rcx, 0x7ff953af6038
L0048: call Microsoft.FSharp.Core.PrintfModule.PrintFormatToStringThen[[System.__Canon, System.Private.CoreLib]](Microsoft.FSharp.Core.PrintfFormat`4<System.__Canon,Microsoft.FSharp.Core.Unit,System.String,System.String>)
L004d: mov rcx, rax
L0050: mov rdx, rsi
L0053: mov rax, [rax]
L0056: mov rax, [rax+0x40]
L005a: mov rax, [rax+0x20]
L005e: add rsp, 0x28
L0062: pop rsi
L0063: pop rdi
L0064: jmp rax

;Test+Test+DecisionType.CompareTo(DecisionType)
L0000: push rdi
L0001: push rsi
L0002: push rbp
L0003: push rbx
L0004: sub rsp, 0x28
L0008: mov rdi, rcx
L000b: mov rsi, rdx
L000e: test rdi, rdi
L0011: je L00ca
L0017: test rsi, rsi
L001a: je L00bc
L0020: mov rdx, rdi
L0023: mov rcx, 0x7ff953a2e8a8
L002d: call System.Runtime.CompilerServices.CastHelpers.IsInstanceOfClass(Void*, System.Object)
L0032: test rax, rax
L0035: je short L003e
L0037: mov ebx, 2
L003c: jmp short L005e
L003e: mov rdx, rdi
L0041: mov rcx, 0x7ff953a2e700
L004b: call System.Runtime.CompilerServices.CastHelpers.IsInstanceOfClass(Void*, System.Object)
L0050: test rax, rax
L0053: je short L005c
L0055: mov ebx, 1
L005a: jmp short L005e
L005c: xor ebx, ebx
L005e: mov rdx, rsi
L0061: mov rcx, 0x7ff953a2e8a8
L006b: call System.Runtime.CompilerServices.CastHelpers.IsInstanceOfClass(Void*, System.Object)
L0070: test rax, rax
L0073: je short L007c
L0075: mov ebp, 2
L007a: jmp short L009c
L007c: mov rdx, rsi
L007f: mov rcx, 0x7ff953a2e700
L0089: call System.Runtime.CompilerServices.CastHelpers.IsInstanceOfClass(Void*, System.Object)
L008e: test rax, rax
L0091: je short L009a
L0093: mov ebp, 1
L0098: jmp short L009c
L009a: xor ebp, ebp
L009c: cmp ebx, ebp
L009e: jne short L00b6
L00a0: mov rcx, rdi
L00a3: mov rdx, rsi
L00a6: xor r8d, r8d
L00a9: add rsp, 0x28
L00ad: pop rbx
L00ae: pop rbp
L00af: pop rsi
L00b0: pop rdi
L00b1: jmp Test+Test.CompareTo$cont@6(DecisionType, DecisionType, Microsoft.FSharp.Core.Unit)
L00b6: mov eax, ebx
L00b8: sub eax, ebp
L00ba: jmp short L00e8
L00bc: mov eax, 1
L00c1: add rsp, 0x28
L00c5: pop rbx
L00c6: pop rbp
L00c7: pop rsi
L00c8: pop rdi
L00c9: ret
L00ca: test rsi, rsi
L00cd: je short L00dd
L00cf: mov eax, 0xffffffff
L00d4: add rsp, 0x28
L00d8: pop rbx
L00d9: pop rbp
L00da: pop rsi
L00db: pop rdi
L00dc: ret
L00dd: xor eax, eax
L00df: add rsp, 0x28
L00e3: pop rbx
L00e4: pop rbp
L00e5: pop rsi
L00e6: pop rdi
L00e7: ret
L00e8: add rsp, 0x28
L00ec: pop rbx
L00ed: pop rbp
L00ee: pop rsi
L00ef: pop rdi
L00f0: ret

;Test+Test+DecisionType.CompareTo(System.Object)
L0000: push rsi
L0001: sub rsp, 0x20
L0005: mov rsi, rcx
L0008: mov rax, rdx
L000b: test rax, rax
L000e: je short L0024
L0010: mov rcx, 0x7ff953a2e0b8
L001a: cmp [rax], rcx
L001d: je short L0024
L001f: call System.Runtime.CompilerServices.CastHelpers.ChkCastClassSpecial(Void*, System.Object)
L0024: mov rcx, rsi
L0027: mov rdx, rax
L002a: cmp [rcx], ecx
L002c: add rsp, 0x20
L0030: pop rsi
L0031: jmp Test+Test+DecisionType.CompareTo(DecisionType)

;Test+Test+DecisionType.CompareTo(System.Object, System.Collections.IComparer)
L0000: push r14
L0002: push rdi
L0003: push rsi
L0004: push rbp
L0005: push rbx
L0006: sub rsp, 0x20
L000a: mov rdi, rcx
L000d: mov rsi, rdx
L0010: mov rbx, r8
L0013: mov rcx, rsi
L0016: test rcx, rcx
L0019: je short L0035
L001b: mov rdx, 0x7ff953a2e0b8
L0025: cmp [rcx], rdx
L0028: je short L0035
L002a: mov rcx, rdx
L002d: mov rdx, rsi
L0030: call System.Runtime.CompilerServices.CastHelpers.ChkCastClassSpecial(Void*, System.Object)
L0035: mov rbp, rsi
L0038: test rdi, rdi
L003b: je L011d
L0041: test rbp, rbp
L0044: je short L005e
L0046: mov rcx, 0x7ff953a2e0b8
L0050: cmp [rbp], rcx
L0054: je short L005e
L0056: mov rdx, rbp
L0059: call System.Runtime.CompilerServices.CastHelpers.ChkCastClassSpecial(Void*, System.Object)
L005e: test rbp, rbp
L0061: je L010d
L0067: mov rdx, rdi
L006a: mov rcx, 0x7ff953a2e8a8
L0074: call System.Runtime.CompilerServices.CastHelpers.IsInstanceOfClass(Void*, System.Object)
L0079: test rax, rax
L007c: je short L0085
L007e: mov esi, 2
L0083: jmp short L00a5
L0085: mov rdx, rdi
L0088: mov rcx, 0x7ff953a2e700
L0092: call System.Runtime.CompilerServices.CastHelpers.IsInstanceOfClass(Void*, System.Object)
L0097: test rax, rax
L009a: je short L00a3
L009c: mov esi, 1
L00a1: jmp short L00a5
L00a3: xor esi, esi
L00a5: mov rdx, rbp
L00a8: mov rcx, 0x7ff953a2e8a8
L00b2: call System.Runtime.CompilerServices.CastHelpers.IsInstanceOfClass(Void*, System.Object)
L00b7: test rax, rax
L00ba: je short L00c4
L00bc: mov r14d, 2
L00c2: jmp short L00e6
L00c4: mov rdx, rbp
L00c7: mov rcx, 0x7ff953a2e700
L00d1: call System.Runtime.CompilerServices.CastHelpers.IsInstanceOfClass(Void*, System.Object)
L00d6: test rax, rax
L00d9: je short L00e3
L00db: mov r14d, 1
L00e1: jmp short L00e6
L00e3: xor r14d, r14d
L00e6: cmp esi, r14d
L00e9: jne short L0106
L00eb: mov rcx, rbx
L00ee: mov rdx, rdi
L00f1: mov r8, rbp
L00f4: xor r9d, r9d
L00f7: add rsp, 0x20
L00fb: pop rbx
L00fc: pop rbp
L00fd: pop rsi
L00fe: pop rdi
L00ff: pop r14
L0101: jmp Test+Test.CompareTo$cont@6-1(System.Collections.IComparer, DecisionType, DecisionType, Microsoft.FSharp.Core.Unit)
L0106: mov eax, esi
L0108: sub eax, r14d
L010b: jmp short L015e
L010d: mov eax, 1
L0112: add rsp, 0x20
L0116: pop rbx
L0117: pop rbp
L0118: pop rsi
L0119: pop rdi
L011a: pop r14
L011c: ret
L011d: mov rax, rsi
L0120: test rax, rax
L0123: je short L013c
L0125: mov rcx, 0x7ff953a2e0b8
L012f: cmp [rax], rcx
L0132: je short L013c
L0134: mov rdx, rsi
L0137: call System.Runtime.CompilerServices.CastHelpers.ChkCastClassSpecial(Void*, System.Object)
L013c: test rax, rax
L013f: je short L0151
L0141: mov eax, 0xffffffff
L0146: add rsp, 0x20
L014a: pop rbx
L014b: pop rbp
L014c: pop rsi
L014d: pop rdi
L014e: pop r14
L0150: ret
L0151: xor eax, eax
L0153: add rsp, 0x20
L0157: pop rbx
L0158: pop rbp
L0159: pop rsi
L015a: pop rdi
L015b: pop r14
L015d: ret
L015e: add rsp, 0x20
L0162: pop rbx
L0163: pop rbp
L0164: pop rsi
L0165: pop rdi
L0166: pop r14
L0168: ret

;Test+Test+DecisionType.GetHashCode(System.Collections.IEqualityComparer)
L0000: push rdi
L0001: push rsi
L0002: push rbx
L0003: sub rsp, 0x20
L0007: vzeroupper
L000a: mov rsi, rcx
L000d: mov rdi, rdx
L0010: test rsi, rsi
L0013: je L0122
L0019: mov rdx, rsi
L001c: mov rcx, 0x7ff953a2e700
L0026: call System.Runtime.CompilerServices.CastHelpers.IsInstanceOfClass(Void*, System.Object)
L002b: mov rbx, rax
L002e: test rbx, rbx
L0031: jne short L0070
L0033: mov rdx, rsi
L0036: mov rcx, 0x7ff953a2e8a8
L0040: call System.Runtime.CompilerServices.CastHelpers.IsInstanceOfClass(Void*, System.Object)
L0045: test rax, rax
L0048: jne L00c8
L004e: test rax, rax
L0051: je short L005a
L0053: mov eax, 2
L0058: jmp short L0068
L005a: test rbx, rbx
L005d: je short L0066
L005f: mov eax, 1
L0064: jmp short L0068
L0066: xor eax, eax
L0068: add rsp, 0x20
L006c: pop rbx
L006d: pop rsi
L006e: pop rdi
L006f: ret
L0070: mov rcx, rsi
L0073: mov rdx, 0x7ff953a2e700
L007d: cmp [rcx], rdx
L0080: je short L008d
L0082: mov rcx, rdx
L0085: mov rdx, rsi
L0088: call System.Runtime.CompilerServices.CastHelpers.ChkCastClassSpecial(Void*, System.Object)
L008d: vmovsd xmm1, [rsi+0x10]
L0092: mov rcx, rdi
L0095: call Microsoft.FSharp.Core.LanguagePrimitives+HashCompare.GenericHashWithComparerIntrinsic[[System.Double, System.Private.CoreLib]](System.Collections.IEqualityComparer, Double)
L009a: lea ebx, [rax-0x61c88607]
L00a0: vmovsd xmm1, [rsi+8]
L00a5: mov rcx, rdi
L00a8: call Microsoft.FSharp.Core.LanguagePrimitives+HashCompare.GenericHashWithComparerIntrinsic[[System.Double, System.Private.CoreLib]](System.Collections.IEqualityComparer, Double)
L00ad: mov ecx, ebx
L00af: shl ecx, 6
L00b2: add eax, ecx
L00b4: sar ebx, 2
L00b7: lea ebx, [rax+rbx-0x61c88647]
L00be: mov eax, ebx
L00c0: add rsp, 0x20
L00c4: pop rbx
L00c5: pop rsi
L00c6: pop rdi
L00c7: ret
L00c8: mov rcx, rsi
L00cb: mov rdx, 0x7ff953a2e8a8
L00d5: cmp [rcx], rdx
L00d8: je short L00e5
L00da: mov rcx, rdx
L00dd: mov rdx, rsi
L00e0: call System.Runtime.CompilerServices.CastHelpers.ChkCastClassSpecial(Void*, System.Object)
L00e5: vmovsd xmm1, [rsi+0x10]
L00ea: mov rcx, rdi
L00ed: call Microsoft.FSharp.Core.LanguagePrimitives+HashCompare.GenericHashWithComparerIntrinsic[[System.Double, System.Private.CoreLib]](System.Collections.IEqualityComparer, Double)
L00f2: lea ebx, [rax-0x61c885c7]
L00f8: vmovsd xmm1, [rsi+8]
L00fd: mov rcx, rdi
L0100: call Microsoft.FSharp.Core.LanguagePrimitives+HashCompare.GenericHashWithComparerIntrinsic[[System.Double, System.Private.CoreLib]](System.Collections.IEqualityComparer, Double)
L0105: mov edx, ebx
L0107: shl edx, 6
L010a: add eax, edx
L010c: mov edx, ebx
L010e: sar edx, 2
L0111: lea ebx, [rax+rdx-0x61c88647]
L0118: mov eax, ebx
L011a: add rsp, 0x20
L011e: pop rbx
L011f: pop rsi
L0120: pop rdi
L0121: ret
L0122: xor eax, eax
L0124: add rsp, 0x20
L0128: pop rbx
L0129: pop rsi
L012a: pop rdi
L012b: ret

;Test+Test+DecisionType.GetHashCode()
L0000: mov rdx, 0x191f2a86fa8
L000a: mov rdx, [rdx]
L000d: cmp [rcx], ecx
L000f: jmp Test+Test+DecisionType.GetHashCode(System.Collections.IEqualityComparer)

;Test+Test+DecisionType.Equals(System.Object, System.Collections.IEqualityComparer)
L0000: push r14
L0002: push rdi
L0003: push rsi
L0004: push rbp
L0005: push rbx
L0006: sub rsp, 0x20
L000a: vzeroupper
L000d: mov rsi, rcx
L0010: test rsi, rsi
L0013: je L01a1
L0019: mov rcx, 0x7ff953a2e0b8
L0023: call System.Runtime.CompilerServices.CastHelpers.IsInstanceOfClass(Void*, System.Object)
L0028: mov rdi, rax
L002b: test rdi, rdi
L002e: je L0194
L0034: mov rdx, rsi
L0037: mov rcx, 0x7ff953a2e8a8
L0041: call System.Runtime.CompilerServices.CastHelpers.IsInstanceOfClass(Void*, System.Object)
L0046: mov rbx, rax
L0049: test rbx, rbx
L004c: je short L0055
L004e: mov ebp, 2
L0053: jmp short L0075
L0055: mov rdx, rsi
L0058: mov rcx, 0x7ff953a2e700
L0062: call System.Runtime.CompilerServices.CastHelpers.IsInstanceOfClass(Void*, System.Object)
L0067: test rax, rax
L006a: je short L0073
L006c: mov ebp, 1
L0071: jmp short L0075
L0073: xor ebp, ebp
L0075: mov rdx, rdi
L0078: mov rcx, 0x7ff953a2e8a8
L0082: call System.Runtime.CompilerServices.CastHelpers.IsInstanceOfClass(Void*, System.Object)
L0087: test rax, rax
L008a: je short L0094
L008c: mov r14d, 2
L0092: jmp short L00b6
L0094: mov rdx, rdi
L0097: mov rcx, 0x7ff953a2e700
L00a1: call System.Runtime.CompilerServices.CastHelpers.IsInstanceOfClass(Void*, System.Object)
L00a6: test rax, rax
L00a9: je short L00b3
L00ab: mov r14d, 1
L00b1: jmp short L00b6
L00b3: xor r14d, r14d
L00b6: cmp ebp, r14d
L00b9: jne L0194
L00bf: mov rdx, rsi
L00c2: mov rcx, 0x7ff953a2e700
L00cc: call System.Runtime.CompilerServices.CastHelpers.IsInstanceOfClass(Void*, System.Object)
L00d1: test rax, rax
L00d4: jne short L00eb
L00d6: test rbx, rbx
L00d9: jne short L0135
L00db: mov eax, 1
L00e0: add rsp, 0x20
L00e4: pop rbx
L00e5: pop rbp
L00e6: pop rsi
L00e7: pop rdi
L00e8: pop r14
L00ea: ret
L00eb: mov rbx, rdi
L00ee: mov rcx, 0x7ff953a2e700
L00f8: cmp [rbx], rcx
L00fb: je short L0108
L00fd: mov rdx, rdi
L0100: call System.Runtime.CompilerServices.CastHelpers.ChkCastClassSpecial(Void*, System.Object)
L0105: mov rbx, rax
L0108: vmovsd xmm0, [rsi+8]
L010d: vucomisd xmm0, [rbx+8]
L0112: jp L0194
L0118: jne L0194
L011e: vmovsd xmm0, [rsi+0x10]
L0123: vucomisd xmm0, [rbx+0x10]
L0128: setnp bl
L012b: jp short L0130
L012d: sete bl
L0130: movzx ebx, bl
L0133: jmp short L01aa
L0135: mov rcx, rsi
L0138: mov rdx, 0x7ff953a2e8a8
L0142: cmp [rcx], rdx
L0145: je short L0152
L0147: mov rcx, rdx
L014a: mov rdx, rsi
L014d: call System.Runtime.CompilerServices.CastHelpers.ChkCastClassSpecial(Void*, System.Object)
L0152: mov rbx, rdi
L0155: mov rcx, 0x7ff953a2e8a8
L015f: cmp [rbx], rcx
L0162: je short L016f
L0164: mov rdx, rdi
L0167: call System.Runtime.CompilerServices.CastHelpers.ChkCastClassSpecial(Void*, System.Object)
L016c: mov rbx, rax
L016f: vmovsd xmm0, [rsi+8]
L0174: vucomisd xmm0, [rbx+8]
L0179: jp short L0194
L017b: jne short L0194
L017d: vmovsd xmm0, [rsi+0x10]
L0182: vucomisd xmm0, [rbx+0x10]
L0187: setnp bl
L018a: jp short L018f
L018c: sete bl
L018f: movzx ebx, bl
L0192: jmp short L01aa
L0194: xor eax, eax
L0196: add rsp, 0x20
L019a: pop rbx
L019b: pop rbp
L019c: pop rsi
L019d: pop rdi
L019e: pop r14
L01a0: ret
L01a1: test rdx, rdx
L01a4: sete bl
L01a7: movzx ebx, bl
L01aa: movzx eax, bl
L01ad: add rsp, 0x20
L01b1: pop rbx
L01b2: pop rbp
L01b3: pop rsi
L01b4: pop rdi
L01b5: pop r14
L01b7: ret

;Test+Test+DecisionType.Equals(DecisionType)
L0000: push rdi
L0001: push rsi
L0002: push rbp
L0003: push rbx
L0004: sub rsp, 0x28
L0008: mov rdi, rcx
L000b: mov rsi, rdx
L000e: test rdi, rdi
L0011: je L00cc
L0017: test rsi, rsi
L001a: je L00c1
L0020: mov rdx, rdi
L0023: mov rcx, 0x7ff953a2e8a8
L002d: call System.Runtime.CompilerServices.CastHelpers.IsInstanceOfClass(Void*, System.Object)
L0032: test rax, rax
L0035: je short L003e
L0037: mov ebx, 2
L003c: jmp short L005e
L003e: mov rdx, rdi
L0041: mov rcx, 0x7ff953a2e700
L004b: call System.Runtime.CompilerServices.CastHelpers.IsInstanceOfClass(Void*, System.Object)
L0050: test rax, rax
L0053: je short L005c
L0055: mov ebx, 1
L005a: jmp short L005e
L005c: xor ebx, ebx
L005e: mov rdx, rsi
L0061: mov rcx, 0x7ff953a2e8a8
L006b: call System.Runtime.CompilerServices.CastHelpers.IsInstanceOfClass(Void*, System.Object)
L0070: test rax, rax
L0073: je short L007c
L0075: mov ebp, 2
L007a: jmp short L009c
L007c: mov rdx, rsi
L007f: mov rcx, 0x7ff953a2e700
L0089: call System.Runtime.CompilerServices.CastHelpers.IsInstanceOfClass(Void*, System.Object)
L008e: test rax, rax
L0091: je short L009a
L0093: mov ebp, 1
L0098: jmp short L009c
L009a: xor ebp, ebp
L009c: cmp ebx, ebp
L009e: jne short L00b6
L00a0: mov rcx, rdi
L00a3: mov rdx, rsi
L00a6: xor r8d, r8d
L00a9: add rsp, 0x28
L00ad: pop rbx
L00ae: pop rbp
L00af: pop rsi
L00b0: pop rdi
L00b1: jmp Test+Test.Equals$cont@6(DecisionType, DecisionType, Microsoft.FSharp.Core.Unit)
L00b6: xor eax, eax
L00b8: add rsp, 0x28
L00bc: pop rbx
L00bd: pop rbp
L00be: pop rsi
L00bf: pop rdi
L00c0: ret
L00c1: xor eax, eax
L00c3: add rsp, 0x28
L00c7: pop rbx
L00c8: pop rbp
L00c9: pop rsi
L00ca: pop rdi
L00cb: ret
L00cc: test rsi, rsi
L00cf: sete al
L00d2: movzx eax, al
L00d5: add rsp, 0x28
L00d9: pop rbx
L00da: pop rbp
L00db: pop rsi
L00dc: pop rdi
L00dd: ret

;Test+Test+DecisionType.Equals(System.Object)
L0000: push rsi
L0001: sub rsp, 0x20
L0005: mov rsi, rcx
L0008: mov rcx, 0x7ff953a2e0b8
L0012: call System.Runtime.CompilerServices.CastHelpers.IsInstanceOfClass(Void*, System.Object)
L0017: test rax, rax
L001a: je short L002e
L001c: mov rcx, rsi
L001f: mov rdx, rax
L0022: cmp [rcx], ecx
L0024: add rsp, 0x20
L0028: pop rsi
L0029: jmp Test+Test+DecisionType.Equals(DecisionType)
L002e: xor eax, eax
L0030: add rsp, 0x20
L0034: pop rsi
L0035: ret

;Test+Test+DecisionType+Continuous.get_LowerBound()
L0000: vzeroupper
L0003: vmovsd xmm0, [rcx+8]
L0008: ret

;Test+Test+DecisionType+Continuous.get_UpperBound()
L0000: vzeroupper
L0003: vmovsd xmm0, [rcx+0x10]
L0008: ret

;Test+Test+DecisionType.get_IsBoolean()
L0000: sub rsp, 0x28
L0004: mov rdx, rcx
L0007: mov rcx, 0x7ff953a2e558
L0011: call System.Runtime.CompilerServices.CastHelpers.IsInstanceOfClass(Void*, System.Object)
L0016: test rax, rax
L0019: setne al
L001c: movzx eax, al
L001f: add rsp, 0x28
L0023: ret

;Test+Test+DecisionType.get_IsInteger()
L0000: sub rsp, 0x28
L0004: mov rdx, rcx
L0007: mov rcx, 0x7ff953a2e700
L0011: call System.Runtime.CompilerServices.CastHelpers.IsInstanceOfClass(Void*, System.Object)
L0016: test rax, rax
L0019: setne al
L001c: movzx eax, al
L001f: add rsp, 0x28
L0023: ret

;Test+Test+DecisionType.get_IsContinuous()
L0000: sub rsp, 0x28
L0004: mov rdx, rcx
L0007: mov rcx, 0x7ff953a2e8a8
L0011: call System.Runtime.CompilerServices.CastHelpers.IsInstanceOfClass(Void*, System.Object)
L0016: test rax, rax
L0019: setne al
L001c: movzx eax, al
L001f: add rsp, 0x28
L0023: ret

;Test+Test+DecisionType.get_Tag()
L0000: push rsi
L0001: sub rsp, 0x20
L0005: mov rsi, rcx
L0008: mov rdx, rsi
L000b: mov rcx, 0x7ff953a2e8a8
L0015: call System.Runtime.CompilerServices.CastHelpers.IsInstanceOfClass(Void*, System.Object)
L001a: test rax, rax
L001d: je short L0026
L001f: mov eax, 2
L0024: jmp short L0046
L0026: mov rdx, rsi
L0029: mov rcx, 0x7ff953a2e700
L0033: call System.Runtime.CompilerServices.CastHelpers.IsInstanceOfClass(Void*, System.Object)
L0038: test rax, rax
L003b: je short L0044
L003d: mov eax, 1
L0042: jmp short L0046
L0044: xor eax, eax
L0046: add rsp, 0x20
L004a: pop rsi
L004b: ret

;Test+Test+DecisionType.ToString()
L0000: push rdi
L0001: push rsi
L0002: sub rsp, 0x28
L0006: mov rsi, rcx
L0009: mov rcx, 0x7ff953af5d58
L0013: call 0x00007ff9b182a470
L0018: mov rdi, rax
L001b: mov rdx, 0x191f2a9b3f0
L0025: mov rdx, [rdx]
L0028: lea rcx, [rdi+8]
L002c: call 0x00007ff9b182a080
L0031: xor edx, edx
L0033: mov [rdi+0x10], rdx
L0037: mov [rdi+0x18], rdx
L003b: mov rdx, rdi
L003e: mov rcx, 0x7ff953af6038
L0048: call Microsoft.FSharp.Core.PrintfModule.PrintFormatToStringThen[[System.__Canon, System.Private.CoreLib]](Microsoft.FSharp.Core.PrintfFormat`4<System.__Canon,Microsoft.FSharp.Core.Unit,System.String,System.String>)
L004d: mov rcx, rax
L0050: mov rdx, rsi
L0053: mov rax, [rax]
L0056: mov rax, [rax+0x40]
L005a: mov rax, [rax+0x20]
L005e: add rsp, 0x28
L0062: pop rsi
L0063: pop rdi
L0064: jmp rax

;Test+Test+DecisionType.CompareTo(DecisionType)
L0000: push rdi
L0001: push rsi
L0002: push rbp
L0003: push rbx
L0004: sub rsp, 0x28
L0008: mov rdi, rcx
L000b: mov rsi, rdx
L000e: test rdi, rdi
L0011: je L00ca
L0017: test rsi, rsi
L001a: je L00bc
L0020: mov rdx, rdi
L0023: mov rcx, 0x7ff953a2e8a8
L002d: call System.Runtime.CompilerServices.CastHelpers.IsInstanceOfClass(Void*, System.Object)
L0032: test rax, rax
L0035: je short L003e
L0037: mov ebx, 2
L003c: jmp short L005e
L003e: mov rdx, rdi
L0041: mov rcx, 0x7ff953a2e700
L004b: call System.Runtime.CompilerServices.CastHelpers.IsInstanceOfClass(Void*, System.Object)
L0050: test rax, rax
L0053: je short L005c
L0055: mov ebx, 1
L005a: jmp short L005e
L005c: xor ebx, ebx
L005e: mov rdx, rsi
L0061: mov rcx, 0x7ff953a2e8a8
L006b: call System.Runtime.CompilerServices.CastHelpers.IsInstanceOfClass(Void*, System.Object)
L0070: test rax, rax
L0073: je short L007c
L0075: mov ebp, 2
L007a: jmp short L009c
L007c: mov rdx, rsi
L007f: mov rcx, 0x7ff953a2e700
L0089: call System.Runtime.CompilerServices.CastHelpers.IsInstanceOfClass(Void*, System.Object)
L008e: test rax, rax
L0091: je short L009a
L0093: mov ebp, 1
L0098: jmp short L009c
L009a: xor ebp, ebp
L009c: cmp ebx, ebp
L009e: jne short L00b6
L00a0: mov rcx, rdi
L00a3: mov rdx, rsi
L00a6: xor r8d, r8d
L00a9: add rsp, 0x28
L00ad: pop rbx
L00ae: pop rbp
L00af: pop rsi
L00b0: pop rdi
L00b1: jmp Test+Test.CompareTo$cont@6(DecisionType, DecisionType, Microsoft.FSharp.Core.Unit)
L00b6: mov eax, ebx
L00b8: sub eax, ebp
L00ba: jmp short L00e8
L00bc: mov eax, 1
L00c1: add rsp, 0x28
L00c5: pop rbx
L00c6: pop rbp
L00c7: pop rsi
L00c8: pop rdi
L00c9: ret
L00ca: test rsi, rsi
L00cd: je short L00dd
L00cf: mov eax, 0xffffffff
L00d4: add rsp, 0x28
L00d8: pop rbx
L00d9: pop rbp
L00da: pop rsi
L00db: pop rdi
L00dc: ret
L00dd: xor eax, eax
L00df: add rsp, 0x28
L00e3: pop rbx
L00e4: pop rbp
L00e5: pop rsi
L00e6: pop rdi
L00e7: ret
L00e8: add rsp, 0x28
L00ec: pop rbx
L00ed: pop rbp
L00ee: pop rsi
L00ef: pop rdi
L00f0: ret

;Test+Test+DecisionType.CompareTo(System.Object)
L0000: push rsi
L0001: sub rsp, 0x20
L0005: mov rsi, rcx
L0008: mov rax, rdx
L000b: test rax, rax
L000e: je short L0024
L0010: mov rcx, 0x7ff953a2e0b8
L001a: cmp [rax], rcx
L001d: je short L0024
L001f: call System.Runtime.CompilerServices.CastHelpers.ChkCastClassSpecial(Void*, System.Object)
L0024: mov rcx, rsi
L0027: mov rdx, rax
L002a: cmp [rcx], ecx
L002c: add rsp, 0x20
L0030: pop rsi
L0031: jmp Test+Test+DecisionType.CompareTo(DecisionType)

;Test+Test+DecisionType.CompareTo(System.Object, System.Collections.IComparer)
L0000: push r14
L0002: push rdi
L0003: push rsi
L0004: push rbp
L0005: push rbx
L0006: sub rsp, 0x20
L000a: mov rdi, rcx
L000d: mov rsi, rdx
L0010: mov rbx, r8
L0013: mov rcx, rsi
L0016: test rcx, rcx
L0019: je short L0035
L001b: mov rdx, 0x7ff953a2e0b8
L0025: cmp [rcx], rdx
L0028: je short L0035
L002a: mov rcx, rdx
L002d: mov rdx, rsi
L0030: call System.Runtime.CompilerServices.CastHelpers.ChkCastClassSpecial(Void*, System.Object)
L0035: mov rbp, rsi
L0038: test rdi, rdi
L003b: je L011d
L0041: test rbp, rbp
L0044: je short L005e
L0046: mov rcx, 0x7ff953a2e0b8
L0050: cmp [rbp], rcx
L0054: je short L005e
L0056: mov rdx, rbp
L0059: call System.Runtime.CompilerServices.CastHelpers.ChkCastClassSpecial(Void*, System.Object)
L005e: test rbp, rbp
L0061: je L010d
L0067: mov rdx, rdi
L006a: mov rcx, 0x7ff953a2e8a8
L0074: call System.Runtime.CompilerServices.CastHelpers.IsInstanceOfClass(Void*, System.Object)
L0079: test rax, rax
L007c: je short L0085
L007e: mov esi, 2
L0083: jmp short L00a5
L0085: mov rdx, rdi
L0088: mov rcx, 0x7ff953a2e700
L0092: call System.Runtime.CompilerServices.CastHelpers.IsInstanceOfClass(Void*, System.Object)
L0097: test rax, rax
L009a: je short L00a3
L009c: mov esi, 1
L00a1: jmp short L00a5
L00a3: xor esi, esi
L00a5: mov rdx, rbp
L00a8: mov rcx, 0x7ff953a2e8a8
L00b2: call System.Runtime.CompilerServices.CastHelpers.IsInstanceOfClass(Void*, System.Object)
L00b7: test rax, rax
L00ba: je short L00c4
L00bc: mov r14d, 2
L00c2: jmp short L00e6
L00c4: mov rdx, rbp
L00c7: mov rcx, 0x7ff953a2e700
L00d1: call System.Runtime.CompilerServices.CastHelpers.IsInstanceOfClass(Void*, System.Object)
L00d6: test rax, rax
L00d9: je short L00e3
L00db: mov r14d, 1
L00e1: jmp short L00e6
L00e3: xor r14d, r14d
L00e6: cmp esi, r14d
L00e9: jne short L0106
L00eb: mov rcx, rbx
L00ee: mov rdx, rdi
L00f1: mov r8, rbp
L00f4: xor r9d, r9d
L00f7: add rsp, 0x20
L00fb: pop rbx
L00fc: pop rbp
L00fd: pop rsi
L00fe: pop rdi
L00ff: pop r14
L0101: jmp Test+Test.CompareTo$cont@6-1(System.Collections.IComparer, DecisionType, DecisionType, Microsoft.FSharp.Core.Unit)
L0106: mov eax, esi
L0108: sub eax, r14d
L010b: jmp short L015e
L010d: mov eax, 1
L0112: add rsp, 0x20
L0116: pop rbx
L0117: pop rbp
L0118: pop rsi
L0119: pop rdi
L011a: pop r14
L011c: ret
L011d: mov rax, rsi
L0120: test rax, rax
L0123: je short L013c
L0125: mov rcx, 0x7ff953a2e0b8
L012f: cmp [rax], rcx
L0132: je short L013c
L0134: mov rdx, rsi
L0137: call System.Runtime.CompilerServices.CastHelpers.ChkCastClassSpecial(Void*, System.Object)
L013c: test rax, rax
L013f: je short L0151
L0141: mov eax, 0xffffffff
L0146: add rsp, 0x20
L014a: pop rbx
L014b: pop rbp
L014c: pop rsi
L014d: pop rdi
L014e: pop r14
L0150: ret
L0151: xor eax, eax
L0153: add rsp, 0x20
L0157: pop rbx
L0158: pop rbp
L0159: pop rsi
L015a: pop rdi
L015b: pop r14
L015d: ret
L015e: add rsp, 0x20
L0162: pop rbx
L0163: pop rbp
L0164: pop rsi
L0165: pop rdi
L0166: pop r14
L0168: ret

;Test+Test+DecisionType.GetHashCode(System.Collections.IEqualityComparer)
L0000: push rdi
L0001: push rsi
L0002: push rbx
L0003: sub rsp, 0x20
L0007: vzeroupper
L000a: mov rsi, rcx
L000d: mov rdi, rdx
L0010: test rsi, rsi
L0013: je L0122
L0019: mov rdx, rsi
L001c: mov rcx, 0x7ff953a2e700
L0026: call System.Runtime.CompilerServices.CastHelpers.IsInstanceOfClass(Void*, System.Object)
L002b: mov rbx, rax
L002e: test rbx, rbx
L0031: jne short L0070
L0033: mov rdx, rsi
L0036: mov rcx, 0x7ff953a2e8a8
L0040: call System.Runtime.CompilerServices.CastHelpers.IsInstanceOfClass(Void*, System.Object)
L0045: test rax, rax
L0048: jne L00c8
L004e: test rax, rax
L0051: je short L005a
L0053: mov eax, 2
L0058: jmp short L0068
L005a: test rbx, rbx
L005d: je short L0066
L005f: mov eax, 1
L0064: jmp short L0068
L0066: xor eax, eax
L0068: add rsp, 0x20
L006c: pop rbx
L006d: pop rsi
L006e: pop rdi
L006f: ret
L0070: mov rcx, rsi
L0073: mov rdx, 0x7ff953a2e700
L007d: cmp [rcx], rdx
L0080: je short L008d
L0082: mov rcx, rdx
L0085: mov rdx, rsi
L0088: call System.Runtime.CompilerServices.CastHelpers.ChkCastClassSpecial(Void*, System.Object)
L008d: vmovsd xmm1, [rsi+0x10]
L0092: mov rcx, rdi
L0095: call Microsoft.FSharp.Core.LanguagePrimitives+HashCompare.GenericHashWithComparerIntrinsic[[System.Double, System.Private.CoreLib]](System.Collections.IEqualityComparer, Double)
L009a: lea ebx, [rax-0x61c88607]
L00a0: vmovsd xmm1, [rsi+8]
L00a5: mov rcx, rdi
L00a8: call Microsoft.FSharp.Core.LanguagePrimitives+HashCompare.GenericHashWithComparerIntrinsic[[System.Double, System.Private.CoreLib]](System.Collections.IEqualityComparer, Double)
L00ad: mov ecx, ebx
L00af: shl ecx, 6
L00b2: add eax, ecx
L00b4: sar ebx, 2
L00b7: lea ebx, [rax+rbx-0x61c88647]
L00be: mov eax, ebx
L00c0: add rsp, 0x20
L00c4: pop rbx
L00c5: pop rsi
L00c6: pop rdi
L00c7: ret
L00c8: mov rcx, rsi
L00cb: mov rdx, 0x7ff953a2e8a8
L00d5: cmp [rcx], rdx
L00d8: je short L00e5
L00da: mov rcx, rdx
L00dd: mov rdx, rsi
L00e0: call System.Runtime.CompilerServices.CastHelpers.ChkCastClassSpecial(Void*, System.Object)
L00e5: vmovsd xmm1, [rsi+0x10]
L00ea: mov rcx, rdi
L00ed: call Microsoft.FSharp.Core.LanguagePrimitives+HashCompare.GenericHashWithComparerIntrinsic[[System.Double, System.Private.CoreLib]](System.Collections.IEqualityComparer, Double)
L00f2: lea ebx, [rax-0x61c885c7]
L00f8: vmovsd xmm1, [rsi+8]
L00fd: mov rcx, rdi
L0100: call Microsoft.FSharp.Core.LanguagePrimitives+HashCompare.GenericHashWithComparerIntrinsic[[System.Double, System.Private.CoreLib]](System.Collections.IEqualityComparer, Double)
L0105: mov edx, ebx
L0107: shl edx, 6
L010a: add eax, edx
L010c: mov edx, ebx
L010e: sar edx, 2
L0111: lea ebx, [rax+rdx-0x61c88647]
L0118: mov eax, ebx
L011a: add rsp, 0x20
L011e: pop rbx
L011f: pop rsi
L0120: pop rdi
L0121: ret
L0122: xor eax, eax
L0124: add rsp, 0x20
L0128: pop rbx
L0129: pop rsi
L012a: pop rdi
L012b: ret

;Test+Test+DecisionType.GetHashCode()
L0000: mov rdx, 0x191f2a86fa8
L000a: mov rdx, [rdx]
L000d: cmp [rcx], ecx
L000f: jmp Test+Test+DecisionType.GetHashCode(System.Collections.IEqualityComparer)

;Test+Test+DecisionType.Equals(System.Object, System.Collections.IEqualityComparer)
L0000: push r14
L0002: push rdi
L0003: push rsi
L0004: push rbp
L0005: push rbx
L0006: sub rsp, 0x20
L000a: vzeroupper
L000d: mov rsi, rcx
L0010: test rsi, rsi
L0013: je L01a1
L0019: mov rcx, 0x7ff953a2e0b8
L0023: call System.Runtime.CompilerServices.CastHelpers.IsInstanceOfClass(Void*, System.Object)
L0028: mov rdi, rax
L002b: test rdi, rdi
L002e: je L0194
L0034: mov rdx, rsi
L0037: mov rcx, 0x7ff953a2e8a8
L0041: call System.Runtime.CompilerServices.CastHelpers.IsInstanceOfClass(Void*, System.Object)
L0046: mov rbx, rax
L0049: test rbx, rbx
L004c: je short L0055
L004e: mov ebp, 2
L0053: jmp short L0075
L0055: mov rdx, rsi
L0058: mov rcx, 0x7ff953a2e700
L0062: call System.Runtime.CompilerServices.CastHelpers.IsInstanceOfClass(Void*, System.Object)
L0067: test rax, rax
L006a: je short L0073
L006c: mov ebp, 1
L0071: jmp short L0075
L0073: xor ebp, ebp
L0075: mov rdx, rdi
L0078: mov rcx, 0x7ff953a2e8a8
L0082: call System.Runtime.CompilerServices.CastHelpers.IsInstanceOfClass(Void*, System.Object)
L0087: test rax, rax
L008a: je short L0094
L008c: mov r14d, 2
L0092: jmp short L00b6
L0094: mov rdx, rdi
L0097: mov rcx, 0x7ff953a2e700
L00a1: call System.Runtime.CompilerServices.CastHelpers.IsInstanceOfClass(Void*, System.Object)
L00a6: test rax, rax
L00a9: je short L00b3
L00ab: mov r14d, 1
L00b1: jmp short L00b6
L00b3: xor r14d, r14d
L00b6: cmp ebp, r14d
L00b9: jne L0194
L00bf: mov rdx, rsi
L00c2: mov rcx, 0x7ff953a2e700
L00cc: call System.Runtime.CompilerServices.CastHelpers.IsInstanceOfClass(Void*, System.Object)
L00d1: test rax, rax
L00d4: jne short L00eb
L00d6: test rbx, rbx
L00d9: jne short L0135
L00db: mov eax, 1
L00e0: add rsp, 0x20
L00e4: pop rbx
L00e5: pop rbp
L00e6: pop rsi
L00e7: pop rdi
L00e8: pop r14
L00ea: ret
L00eb: mov rbx, rdi
L00ee: mov rcx, 0x7ff953a2e700
L00f8: cmp [rbx], rcx
L00fb: je short L0108
L00fd: mov rdx, rdi
L0100: call System.Runtime.CompilerServices.CastHelpers.ChkCastClassSpecial(Void*, System.Object)
L0105: mov rbx, rax
L0108: vmovsd xmm0, [rsi+8]
L010d: vucomisd xmm0, [rbx+8]
L0112: jp L0194
L0118: jne L0194
L011e: vmovsd xmm0, [rsi+0x10]
L0123: vucomisd xmm0, [rbx+0x10]
L0128: setnp bl
L012b: jp short L0130
L012d: sete bl
L0130: movzx ebx, bl
L0133: jmp short L01aa
L0135: mov rcx, rsi
L0138: mov rdx, 0x7ff953a2e8a8
L0142: cmp [rcx], rdx
L0145: je short L0152
L0147: mov rcx, rdx
L014a: mov rdx, rsi
L014d: call System.Runtime.CompilerServices.CastHelpers.ChkCastClassSpecial(Void*, System.Object)
L0152: mov rbx, rdi
L0155: mov rcx, 0x7ff953a2e8a8
L015f: cmp [rbx], rcx
L0162: je short L016f
L0164: mov rdx, rdi
L0167: call System.Runtime.CompilerServices.CastHelpers.ChkCastClassSpecial(Void*, System.Object)
L016c: mov rbx, rax
L016f: vmovsd xmm0, [rsi+8]
L0174: vucomisd xmm0, [rbx+8]
L0179: jp short L0194
L017b: jne short L0194
L017d: vmovsd xmm0, [rsi+0x10]
L0182: vucomisd xmm0, [rbx+0x10]
L0187: setnp bl
L018a: jp short L018f
L018c: sete bl
L018f: movzx ebx, bl
L0192: jmp short L01aa
L0194: xor eax, eax
L0196: add rsp, 0x20
L019a: pop rbx
L019b: pop rbp
L019c: pop rsi
L019d: pop rdi
L019e: pop r14
L01a0: ret
L01a1: test rdx, rdx
L01a4: sete bl
L01a7: movzx ebx, bl
L01aa: movzx eax, bl
L01ad: add rsp, 0x20
L01b1: pop rbx
L01b2: pop rbp
L01b3: pop rsi
L01b4: pop rdi
L01b5: pop r14
L01b7: ret

;Test+Test+DecisionType.Equals(DecisionType)
L0000: push rdi
L0001: push rsi
L0002: push rbp
L0003: push rbx
L0004: sub rsp, 0x28
L0008: mov rdi, rcx
L000b: mov rsi, rdx
L000e: test rdi, rdi
L0011: je L00cc
L0017: test rsi, rsi
L001a: je L00c1
L0020: mov rdx, rdi
L0023: mov rcx, 0x7ff953a2e8a8
L002d: call System.Runtime.CompilerServices.CastHelpers.IsInstanceOfClass(Void*, System.Object)
L0032: test rax, rax
L0035: je short L003e
L0037: mov ebx, 2
L003c: jmp short L005e
L003e: mov rdx, rdi
L0041: mov rcx, 0x7ff953a2e700
L004b: call System.Runtime.CompilerServices.CastHelpers.IsInstanceOfClass(Void*, System.Object)
L0050: test rax, rax
L0053: je short L005c
L0055: mov ebx, 1
L005a: jmp short L005e
L005c: xor ebx, ebx
L005e: mov rdx, rsi
L0061: mov rcx, 0x7ff953a2e8a8
L006b: call System.Runtime.CompilerServices.CastHelpers.IsInstanceOfClass(Void*, System.Object)
L0070: test rax, rax
L0073: je short L007c
L0075: mov ebp, 2
L007a: jmp short L009c
L007c: mov rdx, rsi
L007f: mov rcx, 0x7ff953a2e700
L0089: call System.Runtime.CompilerServices.CastHelpers.IsInstanceOfClass(Void*, System.Object)
L008e: test rax, rax
L0091: je short L009a
L0093: mov ebp, 1
L0098: jmp short L009c
L009a: xor ebp, ebp
L009c: cmp ebx, ebp
L009e: jne short L00b6
L00a0: mov rcx, rdi
L00a3: mov rdx, rsi
L00a6: xor r8d, r8d
L00a9: add rsp, 0x28
L00ad: pop rbx
L00ae: pop rbp
L00af: pop rsi
L00b0: pop rdi
L00b1: jmp Test+Test.Equals$cont@6(DecisionType, DecisionType, Microsoft.FSharp.Core.Unit)
L00b6: xor eax, eax
L00b8: add rsp, 0x28
L00bc: pop rbx
L00bd: pop rbp
L00be: pop rsi
L00bf: pop rdi
L00c0: ret
L00c1: xor eax, eax
L00c3: add rsp, 0x28
L00c7: pop rbx
L00c8: pop rbp
L00c9: pop rsi
L00ca: pop rdi
L00cb: ret
L00cc: test rsi, rsi
L00cf: sete al
L00d2: movzx eax, al
L00d5: add rsp, 0x28
L00d9: pop rbx
L00da: pop rbp
L00db: pop rsi
L00dc: pop rdi
L00dd: ret

;Test+Test+DecisionType.Equals(System.Object)
L0000: push rsi
L0001: sub rsp, 0x20
L0005: mov rsi, rcx
L0008: mov rcx, 0x7ff953a2e0b8
L0012: call System.Runtime.CompilerServices.CastHelpers.IsInstanceOfClass(Void*, System.Object)
L0017: test rax, rax
L001a: je short L002e
L001c: mov rcx, rsi
L001f: mov rdx, rax
L0022: cmp [rcx], ecx
L0024: add rsp, 0x20
L0028: pop rsi
L0029: jmp Test+Test+DecisionType.Equals(DecisionType)
L002e: xor eax, eax
L0030: add rsp, 0x20
L0034: pop rsi
L0035: ret

;Test+Test+DecisionType.get_IsBoolean()
L0000: sub rsp, 0x28
L0004: mov rdx, rcx
L0007: mov rcx, 0x7ff953a2e558
L0011: call System.Runtime.CompilerServices.CastHelpers.IsInstanceOfClass(Void*, System.Object)
L0016: test rax, rax
L0019: setne al
L001c: movzx eax, al
L001f: add rsp, 0x28
L0023: ret

;Test+Test+DecisionType.get_IsInteger()
L0000: sub rsp, 0x28
L0004: mov rdx, rcx
L0007: mov rcx, 0x7ff953a2e700
L0011: call System.Runtime.CompilerServices.CastHelpers.IsInstanceOfClass(Void*, System.Object)
L0016: test rax, rax
L0019: setne al
L001c: movzx eax, al
L001f: add rsp, 0x28
L0023: ret

;Test+Test+DecisionType.get_IsContinuous()
L0000: sub rsp, 0x28
L0004: mov rdx, rcx
L0007: mov rcx, 0x7ff953a2e8a8
L0011: call System.Runtime.CompilerServices.CastHelpers.IsInstanceOfClass(Void*, System.Object)
L0016: test rax, rax
L0019: setne al
L001c: movzx eax, al
L001f: add rsp, 0x28
L0023: ret

;Test+Test+DecisionType.get_Tag()
L0000: push rsi
L0001: sub rsp, 0x20
L0005: mov rsi, rcx
L0008: mov rdx, rsi
L000b: mov rcx, 0x7ff953a2e8a8
L0015: call System.Runtime.CompilerServices.CastHelpers.IsInstanceOfClass(Void*, System.Object)
L001a: test rax, rax
L001d: je short L0026
L001f: mov eax, 2
L0024: jmp short L0046
L0026: mov rdx, rsi
L0029: mov rcx, 0x7ff953a2e700
L0033: call System.Runtime.CompilerServices.CastHelpers.IsInstanceOfClass(Void*, System.Object)
L0038: test rax, rax
L003b: je short L0044
L003d: mov eax, 1
L0042: jmp short L0046
L0044: xor eax, eax
L0046: add rsp, 0x20
L004a: pop rsi
L004b: ret

;Test+Test+DecisionType.ToString()
L0000: push rdi
L0001: push rsi
L0002: sub rsp, 0x28
L0006: mov rsi, rcx
L0009: mov rcx, 0x7ff953af5d58
L0013: call 0x00007ff9b182a470
L0018: mov rdi, rax
L001b: mov rdx, 0x191f2a9b3f0
L0025: mov rdx, [rdx]
L0028: lea rcx, [rdi+8]
L002c: call 0x00007ff9b182a080
L0031: xor edx, edx
L0033: mov [rdi+0x10], rdx
L0037: mov [rdi+0x18], rdx
L003b: mov rdx, rdi
L003e: mov rcx, 0x7ff953af6038
L0048: call Microsoft.FSharp.Core.PrintfModule.PrintFormatToStringThen[[System.__Canon, System.Private.CoreLib]](Microsoft.FSharp.Core.PrintfFormat`4<System.__Canon,Microsoft.FSharp.Core.Unit,System.String,System.String>)
L004d: mov rcx, rax
L0050: mov rdx, rsi
L0053: mov rax, [rax]
L0056: mov rax, [rax+0x40]
L005a: mov rax, [rax+0x20]
L005e: add rsp, 0x28
L0062: pop rsi
L0063: pop rdi
L0064: jmp rax

;Test+Test+DecisionType.CompareTo(DecisionType)
L0000: push rdi
L0001: push rsi
L0002: push rbp
L0003: push rbx
L0004: sub rsp, 0x28
L0008: mov rdi, rcx
L000b: mov rsi, rdx
L000e: test rdi, rdi
L0011: je L00ca
L0017: test rsi, rsi
L001a: je L00bc
L0020: mov rdx, rdi
L0023: mov rcx, 0x7ff953a2e8a8
L002d: call System.Runtime.CompilerServices.CastHelpers.IsInstanceOfClass(Void*, System.Object)
L0032: test rax, rax
L0035: je short L003e
L0037: mov ebx, 2
L003c: jmp short L005e
L003e: mov rdx, rdi
L0041: mov rcx, 0x7ff953a2e700
L004b: call System.Runtime.CompilerServices.CastHelpers.IsInstanceOfClass(Void*, System.Object)
L0050: test rax, rax
L0053: je short L005c
L0055: mov ebx, 1
L005a: jmp short L005e
L005c: xor ebx, ebx
L005e: mov rdx, rsi
L0061: mov rcx, 0x7ff953a2e8a8
L006b: call System.Runtime.CompilerServices.CastHelpers.IsInstanceOfClass(Void*, System.Object)
L0070: test rax, rax
L0073: je short L007c
L0075: mov ebp, 2
L007a: jmp short L009c
L007c: mov rdx, rsi
L007f: mov rcx, 0x7ff953a2e700
L0089: call System.Runtime.CompilerServices.CastHelpers.IsInstanceOfClass(Void*, System.Object)
L008e: test rax, rax
L0091: je short L009a
L0093: mov ebp, 1
L0098: jmp short L009c
L009a: xor ebp, ebp
L009c: cmp ebx, ebp
L009e: jne short L00b6
L00a0: mov rcx, rdi
L00a3: mov rdx, rsi
L00a6: xor r8d, r8d
L00a9: add rsp, 0x28
L00ad: pop rbx
L00ae: pop rbp
L00af: pop rsi
L00b0: pop rdi
L00b1: jmp Test+Test.CompareTo$cont@6(DecisionType, DecisionType, Microsoft.FSharp.Core.Unit)
L00b6: mov eax, ebx
L00b8: sub eax, ebp
L00ba: jmp short L00e8
L00bc: mov eax, 1
L00c1: add rsp, 0x28
L00c5: pop rbx
L00c6: pop rbp
L00c7: pop rsi
L00c8: pop rdi
L00c9: ret
L00ca: test rsi, rsi
L00cd: je short L00dd
L00cf: mov eax, 0xffffffff
L00d4: add rsp, 0x28
L00d8: pop rbx
L00d9: pop rbp
L00da: pop rsi
L00db: pop rdi
L00dc: ret
L00dd: xor eax, eax
L00df: add rsp, 0x28
L00e3: pop rbx
L00e4: pop rbp
L00e5: pop rsi
L00e6: pop rdi
L00e7: ret
L00e8: add rsp, 0x28
L00ec: pop rbx
L00ed: pop rbp
L00ee: pop rsi
L00ef: pop rdi
L00f0: ret

;Test+Test+DecisionType.CompareTo(System.Object)
L0000: push rsi
L0001: sub rsp, 0x20
L0005: mov rsi, rcx
L0008: mov rax, rdx
L000b: test rax, rax
L000e: je short L0024
L0010: mov rcx, 0x7ff953a2e0b8
L001a: cmp [rax], rcx
L001d: je short L0024
L001f: call System.Runtime.CompilerServices.CastHelpers.ChkCastClassSpecial(Void*, System.Object)
L0024: mov rcx, rsi
L0027: mov rdx, rax
L002a: cmp [rcx], ecx
L002c: add rsp, 0x20
L0030: pop rsi
L0031: jmp Test+Test+DecisionType.CompareTo(DecisionType)

;Test+Test+DecisionType.CompareTo(System.Object, System.Collections.IComparer)
L0000: push r14
L0002: push rdi
L0003: push rsi
L0004: push rbp
L0005: push rbx
L0006: sub rsp, 0x20
L000a: mov rdi, rcx
L000d: mov rsi, rdx
L0010: mov rbx, r8
L0013: mov rcx, rsi
L0016: test rcx, rcx
L0019: je short L0035
L001b: mov rdx, 0x7ff953a2e0b8
L0025: cmp [rcx], rdx
L0028: je short L0035
L002a: mov rcx, rdx
L002d: mov rdx, rsi
L0030: call System.Runtime.CompilerServices.CastHelpers.ChkCastClassSpecial(Void*, System.Object)
L0035: mov rbp, rsi
L0038: test rdi, rdi
L003b: je L011d
L0041: test rbp, rbp
L0044: je short L005e
L0046: mov rcx, 0x7ff953a2e0b8
L0050: cmp [rbp], rcx
L0054: je short L005e
L0056: mov rdx, rbp
L0059: call System.Runtime.CompilerServices.CastHelpers.ChkCastClassSpecial(Void*, System.Object)
L005e: test rbp, rbp
L0061: je L010d
L0067: mov rdx, rdi
L006a: mov rcx, 0x7ff953a2e8a8
L0074: call System.Runtime.CompilerServices.CastHelpers.IsInstanceOfClass(Void*, System.Object)
L0079: test rax, rax
L007c: je short L0085
L007e: mov esi, 2
L0083: jmp short L00a5
L0085: mov rdx, rdi
L0088: mov rcx, 0x7ff953a2e700
L0092: call System.Runtime.CompilerServices.CastHelpers.IsInstanceOfClass(Void*, System.Object)
L0097: test rax, rax
L009a: je short L00a3
L009c: mov esi, 1
L00a1: jmp short L00a5
L00a3: xor esi, esi
L00a5: mov rdx, rbp
L00a8: mov rcx, 0x7ff953a2e8a8
L00b2: call System.Runtime.CompilerServices.CastHelpers.IsInstanceOfClass(Void*, System.Object)
L00b7: test rax, rax
L00ba: je short L00c4
L00bc: mov r14d, 2
L00c2: jmp short L00e6
L00c4: mov rdx, rbp
L00c7: mov rcx, 0x7ff953a2e700
L00d1: call System.Runtime.CompilerServices.CastHelpers.IsInstanceOfClass(Void*, System.Object)
L00d6: test rax, rax
L00d9: je short L00e3
L00db: mov r14d, 1
L00e1: jmp short L00e6
L00e3: xor r14d, r14d
L00e6: cmp esi, r14d
L00e9: jne short L0106
L00eb: mov rcx, rbx
L00ee: mov rdx, rdi
L00f1: mov r8, rbp
L00f4: xor r9d, r9d
L00f7: add rsp, 0x20
L00fb: pop rbx
L00fc: pop rbp
L00fd: pop rsi
L00fe: pop rdi
L00ff: pop r14
L0101: jmp Test+Test.CompareTo$cont@6-1(System.Collections.IComparer, DecisionType, DecisionType, Microsoft.FSharp.Core.Unit)
L0106: mov eax, esi
L0108: sub eax, r14d
L010b: jmp short L015e
L010d: mov eax, 1
L0112: add rsp, 0x20
L0116: pop rbx
L0117: pop rbp
L0118: pop rsi
L0119: pop rdi
L011a: pop r14
L011c: ret
L011d: mov rax, rsi
L0120: test rax, rax
L0123: je short L013c
L0125: mov rcx, 0x7ff953a2e0b8
L012f: cmp [rax], rcx
L0132: je short L013c
L0134: mov rdx, rsi
L0137: call System.Runtime.CompilerServices.CastHelpers.ChkCastClassSpecial(Void*, System.Object)
L013c: test rax, rax
L013f: je short L0151
L0141: mov eax, 0xffffffff
L0146: add rsp, 0x20
L014a: pop rbx
L014b: pop rbp
L014c: pop rsi
L014d: pop rdi
L014e: pop r14
L0150: ret
L0151: xor eax, eax
L0153: add rsp, 0x20
L0157: pop rbx
L0158: pop rbp
L0159: pop rsi
L015a: pop rdi
L015b: pop r14
L015d: ret
L015e: add rsp, 0x20
L0162: pop rbx
L0163: pop rbp
L0164: pop rsi
L0165: pop rdi
L0166: pop r14
L0168: ret

;Test+Test+DecisionType.GetHashCode(System.Collections.IEqualityComparer)
L0000: push rdi
L0001: push rsi
L0002: push rbx
L0003: sub rsp, 0x20
L0007: vzeroupper
L000a: mov rsi, rcx
L000d: mov rdi, rdx
L0010: test rsi, rsi
L0013: je L0122
L0019: mov rdx, rsi
L001c: mov rcx, 0x7ff953a2e700
L0026: call System.Runtime.CompilerServices.CastHelpers.IsInstanceOfClass(Void*, System.Object)
L002b: mov rbx, rax
L002e: test rbx, rbx
L0031: jne short L0070
L0033: mov rdx, rsi
L0036: mov rcx, 0x7ff953a2e8a8
L0040: call System.Runtime.CompilerServices.CastHelpers.IsInstanceOfClass(Void*, System.Object)
L0045: test rax, rax
L0048: jne L00c8
L004e: test rax, rax
L0051: je short L005a
L0053: mov eax, 2
L0058: jmp short L0068
L005a: test rbx, rbx
L005d: je short L0066
L005f: mov eax, 1
L0064: jmp short L0068
L0066: xor eax, eax
L0068: add rsp, 0x20
L006c: pop rbx
L006d: pop rsi
L006e: pop rdi
L006f: ret
L0070: mov rcx, rsi
L0073: mov rdx, 0x7ff953a2e700
L007d: cmp [rcx], rdx
L0080: je short L008d
L0082: mov rcx, rdx
L0085: mov rdx, rsi
L0088: call System.Runtime.CompilerServices.CastHelpers.ChkCastClassSpecial(Void*, System.Object)
L008d: vmovsd xmm1, [rsi+0x10]
L0092: mov rcx, rdi
L0095: call Microsoft.FSharp.Core.LanguagePrimitives+HashCompare.GenericHashWithComparerIntrinsic[[System.Double, System.Private.CoreLib]](System.Collections.IEqualityComparer, Double)
L009a: lea ebx, [rax-0x61c88607]
L00a0: vmovsd xmm1, [rsi+8]
L00a5: mov rcx, rdi
L00a8: call Microsoft.FSharp.Core.LanguagePrimitives+HashCompare.GenericHashWithComparerIntrinsic[[System.Double, System.Private.CoreLib]](System.Collections.IEqualityComparer, Double)
L00ad: mov ecx, ebx
L00af: shl ecx, 6
L00b2: add eax, ecx
L00b4: sar ebx, 2
L00b7: lea ebx, [rax+rbx-0x61c88647]
L00be: mov eax, ebx
L00c0: add rsp, 0x20
L00c4: pop rbx
L00c5: pop rsi
L00c6: pop rdi
L00c7: ret
L00c8: mov rcx, rsi
L00cb: mov rdx, 0x7ff953a2e8a8
L00d5: cmp [rcx], rdx
L00d8: je short L00e5
L00da: mov rcx, rdx
L00dd: mov rdx, rsi
L00e0: call System.Runtime.CompilerServices.CastHelpers.ChkCastClassSpecial(Void*, System.Object)
L00e5: vmovsd xmm1, [rsi+0x10]
L00ea: mov rcx, rdi
L00ed: call Microsoft.FSharp.Core.LanguagePrimitives+HashCompare.GenericHashWithComparerIntrinsic[[System.Double, System.Private.CoreLib]](System.Collections.IEqualityComparer, Double)
L00f2: lea ebx, [rax-0x61c885c7]
L00f8: vmovsd xmm1, [rsi+8]
L00fd: mov rcx, rdi
L0100: call Microsoft.FSharp.Core.LanguagePrimitives+HashCompare.GenericHashWithComparerIntrinsic[[System.Double, System.Private.CoreLib]](System.Collections.IEqualityComparer, Double)
L0105: mov edx, ebx
L0107: shl edx, 6
L010a: add eax, edx
L010c: mov edx, ebx
L010e: sar edx, 2
L0111: lea ebx, [rax+rdx-0x61c88647]
L0118: mov eax, ebx
L011a: add rsp, 0x20
L011e: pop rbx
L011f: pop rsi
L0120: pop rdi
L0121: ret
L0122: xor eax, eax
L0124: add rsp, 0x20
L0128: pop rbx
L0129: pop rsi
L012a: pop rdi
L012b: ret

;Test+Test+DecisionType.GetHashCode()
L0000: mov rdx, 0x191f2a86fa8
L000a: mov rdx, [rdx]
L000d: cmp [rcx], ecx
L000f: jmp Test+Test+DecisionType.GetHashCode(System.Collections.IEqualityComparer)

;Test+Test+DecisionType.Equals(System.Object, System.Collections.IEqualityComparer)
L0000: push r14
L0002: push rdi
L0003: push rsi
L0004: push rbp
L0005: push rbx
L0006: sub rsp, 0x20
L000a: vzeroupper
L000d: mov rsi, rcx
L0010: test rsi, rsi
L0013: je L01a1
L0019: mov rcx, 0x7ff953a2e0b8
L0023: call System.Runtime.CompilerServices.CastHelpers.IsInstanceOfClass(Void*, System.Object)
L0028: mov rdi, rax
L002b: test rdi, rdi
L002e: je L0194
L0034: mov rdx, rsi
L0037: mov rcx, 0x7ff953a2e8a8
L0041: call System.Runtime.CompilerServices.CastHelpers.IsInstanceOfClass(Void*, System.Object)
L0046: mov rbx, rax
L0049: test rbx, rbx
L004c: je short L0055
L004e: mov ebp, 2
L0053: jmp short L0075
L0055: mov rdx, rsi
L0058: mov rcx, 0x7ff953a2e700
L0062: call System.Runtime.CompilerServices.CastHelpers.IsInstanceOfClass(Void*, System.Object)
L0067: test rax, rax
L006a: je short L0073
L006c: mov ebp, 1
L0071: jmp short L0075
L0073: xor ebp, ebp
L0075: mov rdx, rdi
L0078: mov rcx, 0x7ff953a2e8a8
L0082: call System.Runtime.CompilerServices.CastHelpers.IsInstanceOfClass(Void*, System.Object)
L0087: test rax, rax
L008a: je short L0094
L008c: mov r14d, 2
L0092: jmp short L00b6
L0094: mov rdx, rdi
L0097: mov rcx, 0x7ff953a2e700
L00a1: call System.Runtime.CompilerServices.CastHelpers.IsInstanceOfClass(Void*, System.Object)
L00a6: test rax, rax
L00a9: je short L00b3
L00ab: mov r14d, 1
L00b1: jmp short L00b6
L00b3: xor r14d, r14d
L00b6: cmp ebp, r14d
L00b9: jne L0194
L00bf: mov rdx, rsi
L00c2: mov rcx, 0x7ff953a2e700
L00cc: call System.Runtime.CompilerServices.CastHelpers.IsInstanceOfClass(Void*, System.Object)
L00d1: test rax, rax
L00d4: jne short L00eb
L00d6: test rbx, rbx
L00d9: jne short L0135
L00db: mov eax, 1
L00e0: add rsp, 0x20
L00e4: pop rbx
L00e5: pop rbp
L00e6: pop rsi
L00e7: pop rdi
L00e8: pop r14
L00ea: ret
L00eb: mov rbx, rdi
L00ee: mov rcx, 0x7ff953a2e700
L00f8: cmp [rbx], rcx
L00fb: je short L0108
L00fd: mov rdx, rdi
L0100: call System.Runtime.CompilerServices.CastHelpers.ChkCastClassSpecial(Void*, System.Object)
L0105: mov rbx, rax
L0108: vmovsd xmm0, [rsi+8]
L010d: vucomisd xmm0, [rbx+8]
L0112: jp L0194
L0118: jne L0194
L011e: vmovsd xmm0, [rsi+0x10]
L0123: vucomisd xmm0, [rbx+0x10]
L0128: setnp bl
L012b: jp short L0130
L012d: sete bl
L0130: movzx ebx, bl
L0133: jmp short L01aa
L0135: mov rcx, rsi
L0138: mov rdx, 0x7ff953a2e8a8
L0142: cmp [rcx], rdx
L0145: je short L0152
L0147: mov rcx, rdx
L014a: mov rdx, rsi
L014d: call System.Runtime.CompilerServices.CastHelpers.ChkCastClassSpecial(Void*, System.Object)
L0152: mov rbx, rdi
L0155: mov rcx, 0x7ff953a2e8a8
L015f: cmp [rbx], rcx
L0162: je short L016f
L0164: mov rdx, rdi
L0167: call System.Runtime.CompilerServices.CastHelpers.ChkCastClassSpecial(Void*, System.Object)
L016c: mov rbx, rax
L016f: vmovsd xmm0, [rsi+8]
L0174: vucomisd xmm0, [rbx+8]
L0179: jp short L0194
L017b: jne short L0194
L017d: vmovsd xmm0, [rsi+0x10]
L0182: vucomisd xmm0, [rbx+0x10]
L0187: setnp bl
L018a: jp short L018f
L018c: sete bl
L018f: movzx ebx, bl
L0192: jmp short L01aa
L0194: xor eax, eax
L0196: add rsp, 0x20
L019a: pop rbx
L019b: pop rbp
L019c: pop rsi
L019d: pop rdi
L019e: pop r14
L01a0: ret
L01a1: test rdx, rdx
L01a4: sete bl
L01a7: movzx ebx, bl
L01aa: movzx eax, bl
L01ad: add rsp, 0x20
L01b1: pop rbx
L01b2: pop rbp
L01b3: pop rsi
L01b4: pop rdi
L01b5: pop r14
L01b7: ret

;Test+Test+DecisionType.Equals(DecisionType)
L0000: push rdi
L0001: push rsi
L0002: push rbp
L0003: push rbx
L0004: sub rsp, 0x28
L0008: mov rdi, rcx
L000b: mov rsi, rdx
L000e: test rdi, rdi
L0011: je L00cc
L0017: test rsi, rsi
L001a: je L00c1
L0020: mov rdx, rdi
L0023: mov rcx, 0x7ff953a2e8a8
L002d: call System.Runtime.CompilerServices.CastHelpers.IsInstanceOfClass(Void*, System.Object)
L0032: test rax, rax
L0035: je short L003e
L0037: mov ebx, 2
L003c: jmp short L005e
L003e: mov rdx, rdi
L0041: mov rcx, 0x7ff953a2e700
L004b: call System.Runtime.CompilerServices.CastHelpers.IsInstanceOfClass(Void*, System.Object)
L0050: test rax, rax
L0053: je short L005c
L0055: mov ebx, 1
L005a: jmp short L005e
L005c: xor ebx, ebx
L005e: mov rdx, rsi
L0061: mov rcx, 0x7ff953a2e8a8
L006b: call System.Runtime.CompilerServices.CastHelpers.IsInstanceOfClass(Void*, System.Object)
L0070: test rax, rax
L0073: je short L007c
L0075: mov ebp, 2
L007a: jmp short L009c
L007c: mov rdx, rsi
L007f: mov rcx, 0x7ff953a2e700
L0089: call System.Runtime.CompilerServices.CastHelpers.IsInstanceOfClass(Void*, System.Object)
L008e: test rax, rax
L0091: je short L009a
L0093: mov ebp, 1
L0098: jmp short L009c
L009a: xor ebp, ebp
L009c: cmp ebx, ebp
L009e: jne short L00b6
L00a0: mov rcx, rdi
L00a3: mov rdx, rsi
L00a6: xor r8d, r8d
L00a9: add rsp, 0x28
L00ad: pop rbx
L00ae: pop rbp
L00af: pop rsi
L00b0: pop rdi
L00b1: jmp Test+Test.Equals$cont@6(DecisionType, DecisionType, Microsoft.FSharp.Core.Unit)
L00b6: xor eax, eax
L00b8: add rsp, 0x28
L00bc: pop rbx
L00bd: pop rbp
L00be: pop rsi
L00bf: pop rdi
L00c0: ret
L00c1: xor eax, eax
L00c3: add rsp, 0x28
L00c7: pop rbx
L00c8: pop rbp
L00c9: pop rsi
L00ca: pop rdi
L00cb: ret
L00cc: test rsi, rsi
L00cf: sete al
L00d2: movzx eax, al
L00d5: add rsp, 0x28
L00d9: pop rbx
L00da: pop rbp
L00db: pop rsi
L00dc: pop rdi
L00dd: ret

;Test+Test+DecisionType.Equals(System.Object)
L0000: push rsi
L0001: sub rsp, 0x20
L0005: mov rsi, rcx
L0008: mov rcx, 0x7ff953a2e0b8
L0012: call System.Runtime.CompilerServices.CastHelpers.IsInstanceOfClass(Void*, System.Object)
L0017: test rax, rax
L001a: je short L002e
L001c: mov rcx, rsi
L001f: mov rdx, rax
L0022: cmp [rcx], ecx
L0024: add rsp, 0x20
L0028: pop rsi
L0029: jmp Test+Test+DecisionType.Equals(DecisionType)
L002e: xor eax, eax
L0030: add rsp, 0x20
L0034: pop rsi
L0035: ret

;Test+Test+DecisionType+Integer.get_LowerBound()
L0000: vzeroupper
L0003: vmovsd xmm0, [rcx+8]
L0008: ret

;Test+Test+DecisionType+Integer.get_UpperBound()
L0000: vzeroupper
L0003: vmovsd xmm0, [rcx+0x10]
L0008: ret

;Test+Test+DecisionType.get_IsBoolean()
L0000: sub rsp, 0x28
L0004: mov rdx, rcx
L0007: mov rcx, 0x7ff953a2e558
L0011: call System.Runtime.CompilerServices.CastHelpers.IsInstanceOfClass(Void*, System.Object)
L0016: test rax, rax
L0019: setne al
L001c: movzx eax, al
L001f: add rsp, 0x28
L0023: ret

;Test+Test+DecisionType.get_IsInteger()
L0000: sub rsp, 0x28
L0004: mov rdx, rcx
L0007: mov rcx, 0x7ff953a2e700
L0011: call System.Runtime.CompilerServices.CastHelpers.IsInstanceOfClass(Void*, System.Object)
L0016: test rax, rax
L0019: setne al
L001c: movzx eax, al
L001f: add rsp, 0x28
L0023: ret

;Test+Test+DecisionType.get_IsContinuous()
L0000: sub rsp, 0x28
L0004: mov rdx, rcx
L0007: mov rcx, 0x7ff953a2e8a8
L0011: call System.Runtime.CompilerServices.CastHelpers.IsInstanceOfClass(Void*, System.Object)
L0016: test rax, rax
L0019: setne al
L001c: movzx eax, al
L001f: add rsp, 0x28
L0023: ret

;Test+Test+DecisionType.get_Tag()
L0000: push rsi
L0001: sub rsp, 0x20
L0005: mov rsi, rcx
L0008: mov rdx, rsi
L000b: mov rcx, 0x7ff953a2e8a8
L0015: call System.Runtime.CompilerServices.CastHelpers.IsInstanceOfClass(Void*, System.Object)
L001a: test rax, rax
L001d: je short L0026
L001f: mov eax, 2
L0024: jmp short L0046
L0026: mov rdx, rsi
L0029: mov rcx, 0x7ff953a2e700
L0033: call System.Runtime.CompilerServices.CastHelpers.IsInstanceOfClass(Void*, System.Object)
L0038: test rax, rax
L003b: je short L0044
L003d: mov eax, 1
L0042: jmp short L0046
L0044: xor eax, eax
L0046: add rsp, 0x20
L004a: pop rsi
L004b: ret

;Test+Test+DecisionType.ToString()
L0000: push rdi
L0001: push rsi
L0002: sub rsp, 0x28
L0006: mov rsi, rcx
L0009: mov rcx, 0x7ff953af5d58
L0013: call 0x00007ff9b182a470
L0018: mov rdi, rax
L001b: mov rdx, 0x191f2a9b3f0
L0025: mov rdx, [rdx]
L0028: lea rcx, [rdi+8]
L002c: call 0x00007ff9b182a080
L0031: xor edx, edx
L0033: mov [rdi+0x10], rdx
L0037: mov [rdi+0x18], rdx
L003b: mov rdx, rdi
L003e: mov rcx, 0x7ff953af6038
L0048: call Microsoft.FSharp.Core.PrintfModule.PrintFormatToStringThen[[System.__Canon, System.Private.CoreLib]](Microsoft.FSharp.Core.PrintfFormat`4<System.__Canon,Microsoft.FSharp.Core.Unit,System.String,System.String>)
L004d: mov rcx, rax
L0050: mov rdx, rsi
L0053: mov rax, [rax]
L0056: mov rax, [rax+0x40]
L005a: mov rax, [rax+0x20]
L005e: add rsp, 0x28
L0062: pop rsi
L0063: pop rdi
L0064: jmp rax

;Test+Test+DecisionType.CompareTo(DecisionType)
L0000: push rdi
L0001: push rsi
L0002: push rbp
L0003: push rbx
L0004: sub rsp, 0x28
L0008: mov rdi, rcx
L000b: mov rsi, rdx
L000e: test rdi, rdi
L0011: je L00ca
L0017: test rsi, rsi
L001a: je L00bc
L0020: mov rdx, rdi
L0023: mov rcx, 0x7ff953a2e8a8
L002d: call System.Runtime.CompilerServices.CastHelpers.IsInstanceOfClass(Void*, System.Object)
L0032: test rax, rax
L0035: je short L003e
L0037: mov ebx, 2
L003c: jmp short L005e
L003e: mov rdx, rdi
L0041: mov rcx, 0x7ff953a2e700
L004b: call System.Runtime.CompilerServices.CastHelpers.IsInstanceOfClass(Void*, System.Object)
L0050: test rax, rax
L0053: je short L005c
L0055: mov ebx, 1
L005a: jmp short L005e
L005c: xor ebx, ebx
L005e: mov rdx, rsi
L0061: mov rcx, 0x7ff953a2e8a8
L006b: call System.Runtime.CompilerServices.CastHelpers.IsInstanceOfClass(Void*, System.Object)
L0070: test rax, rax
L0073: je short L007c
L0075: mov ebp, 2
L007a: jmp short L009c
L007c: mov rdx, rsi
L007f: mov rcx, 0x7ff953a2e700
L0089: call System.Runtime.CompilerServices.CastHelpers.IsInstanceOfClass(Void*, System.Object)
L008e: test rax, rax
L0091: je short L009a
L0093: mov ebp, 1
L0098: jmp short L009c
L009a: xor ebp, ebp
L009c: cmp ebx, ebp
L009e: jne short L00b6
L00a0: mov rcx, rdi
L00a3: mov rdx, rsi
L00a6: xor r8d, r8d
L00a9: add rsp, 0x28
L00ad: pop rbx
L00ae: pop rbp
L00af: pop rsi
L00b0: pop rdi
L00b1: jmp Test+Test.CompareTo$cont@6(DecisionType, DecisionType, Microsoft.FSharp.Core.Unit)
L00b6: mov eax, ebx
L00b8: sub eax, ebp
L00ba: jmp short L00e8
L00bc: mov eax, 1
L00c1: add rsp, 0x28
L00c5: pop rbx
L00c6: pop rbp
L00c7: pop rsi
L00c8: pop rdi
L00c9: ret
L00ca: test rsi, rsi
L00cd: je short L00dd
L00cf: mov eax, 0xffffffff
L00d4: add rsp, 0x28
L00d8: pop rbx
L00d9: pop rbp
L00da: pop rsi
L00db: pop rdi
L00dc: ret
L00dd: xor eax, eax
L00df: add rsp, 0x28
L00e3: pop rbx
L00e4: pop rbp
L00e5: pop rsi
L00e6: pop rdi
L00e7: ret
L00e8: add rsp, 0x28
L00ec: pop rbx
L00ed: pop rbp
L00ee: pop rsi
L00ef: pop rdi
L00f0: ret

;Test+Test+DecisionType.CompareTo(System.Object)
L0000: push rsi
L0001: sub rsp, 0x20
L0005: mov rsi, rcx
L0008: mov rax, rdx
L000b: test rax, rax
L000e: je short L0024
L0010: mov rcx, 0x7ff953a2e0b8
L001a: cmp [rax], rcx
L001d: je short L0024
L001f: call System.Runtime.CompilerServices.CastHelpers.ChkCastClassSpecial(Void*, System.Object)
L0024: mov rcx, rsi
L0027: mov rdx, rax
L002a: cmp [rcx], ecx
L002c: add rsp, 0x20
L0030: pop rsi
L0031: jmp Test+Test+DecisionType.CompareTo(DecisionType)

;Test+Test+DecisionType.CompareTo(System.Object, System.Collections.IComparer)
L0000: push r14
L0002: push rdi
L0003: push rsi
L0004: push rbp
L0005: push rbx
L0006: sub rsp, 0x20
L000a: mov rdi, rcx
L000d: mov rsi, rdx
L0010: mov rbx, r8
L0013: mov rcx, rsi
L0016: test rcx, rcx
L0019: je short L0035
L001b: mov rdx, 0x7ff953a2e0b8
L0025: cmp [rcx], rdx
L0028: je short L0035
L002a: mov rcx, rdx
L002d: mov rdx, rsi
L0030: call System.Runtime.CompilerServices.CastHelpers.ChkCastClassSpecial(Void*, System.Object)
L0035: mov rbp, rsi
L0038: test rdi, rdi
L003b: je L011d
L0041: test rbp, rbp
L0044: je short L005e
L0046: mov rcx, 0x7ff953a2e0b8
L0050: cmp [rbp], rcx
L0054: je short L005e
L0056: mov rdx, rbp
L0059: call System.Runtime.CompilerServices.CastHelpers.ChkCastClassSpecial(Void*, System.Object)
L005e: test rbp, rbp
L0061: je L010d
L0067: mov rdx, rdi
L006a: mov rcx, 0x7ff953a2e8a8
L0074: call System.Runtime.CompilerServices.CastHelpers.IsInstanceOfClass(Void*, System.Object)
L0079: test rax, rax
L007c: je short L0085
L007e: mov esi, 2
L0083: jmp short L00a5
L0085: mov rdx, rdi
L0088: mov rcx, 0x7ff953a2e700
L0092: call System.Runtime.CompilerServices.CastHelpers.IsInstanceOfClass(Void*, System.Object)
L0097: test rax, rax
L009a: je short L00a3
L009c: mov esi, 1
L00a1: jmp short L00a5
L00a3: xor esi, esi
L00a5: mov rdx, rbp
L00a8: mov rcx, 0x7ff953a2e8a8
L00b2: call System.Runtime.CompilerServices.CastHelpers.IsInstanceOfClass(Void*, System.Object)
L00b7: test rax, rax
L00ba: je short L00c4
L00bc: mov r14d, 2
L00c2: jmp short L00e6
L00c4: mov rdx, rbp
L00c7: mov rcx, 0x7ff953a2e700
L00d1: call System.Runtime.CompilerServices.CastHelpers.IsInstanceOfClass(Void*, System.Object)
L00d6: test rax, rax
L00d9: je short L00e3
L00db: mov r14d, 1
L00e1: jmp short L00e6
L00e3: xor r14d, r14d
L00e6: cmp esi, r14d
L00e9: jne short L0106
L00eb: mov rcx, rbx
L00ee: mov rdx, rdi
L00f1: mov r8, rbp
L00f4: xor r9d, r9d
L00f7: add rsp, 0x20
L00fb: pop rbx
L00fc: pop rbp
L00fd: pop rsi
L00fe: pop rdi
L00ff: pop r14
L0101: jmp Test+Test.CompareTo$cont@6-1(System.Collections.IComparer, DecisionType, DecisionType, Microsoft.FSharp.Core.Unit)
L0106: mov eax, esi
L0108: sub eax, r14d
L010b: jmp short L015e
L010d: mov eax, 1
L0112: add rsp, 0x20
L0116: pop rbx
L0117: pop rbp
L0118: pop rsi
L0119: pop rdi
L011a: pop r14
L011c: ret
L011d: mov rax, rsi
L0120: test rax, rax
L0123: je short L013c
L0125: mov rcx, 0x7ff953a2e0b8
L012f: cmp [rax], rcx
L0132: je short L013c
L0134: mov rdx, rsi
L0137: call System.Runtime.CompilerServices.CastHelpers.ChkCastClassSpecial(Void*, System.Object)
L013c: test rax, rax
L013f: je short L0151
L0141: mov eax, 0xffffffff
L0146: add rsp, 0x20
L014a: pop rbx
L014b: pop rbp
L014c: pop rsi
L014d: pop rdi
L014e: pop r14
L0150: ret
L0151: xor eax, eax
L0153: add rsp, 0x20
L0157: pop rbx
L0158: pop rbp
L0159: pop rsi
L015a: pop rdi
L015b: pop r14
L015d: ret
L015e: add rsp, 0x20
L0162: pop rbx
L0163: pop rbp
L0164: pop rsi
L0165: pop rdi
L0166: pop r14
L0168: ret

;Test+Test+DecisionType.GetHashCode(System.Collections.IEqualityComparer)
L0000: push rdi
L0001: push rsi
L0002: push rbx
L0003: sub rsp, 0x20
L0007: vzeroupper
L000a: mov rsi, rcx
L000d: mov rdi, rdx
L0010: test rsi, rsi
L0013: je L0122
L0019: mov rdx, rsi
L001c: mov rcx, 0x7ff953a2e700
L0026: call System.Runtime.CompilerServices.CastHelpers.IsInstanceOfClass(Void*, System.Object)
L002b: mov rbx, rax
L002e: test rbx, rbx
L0031: jne short L0070
L0033: mov rdx, rsi
L0036: mov rcx, 0x7ff953a2e8a8
L0040: call System.Runtime.CompilerServices.CastHelpers.IsInstanceOfClass(Void*, System.Object)
L0045: test rax, rax
L0048: jne L00c8
L004e: test rax, rax
L0051: je short L005a
L0053: mov eax, 2
L0058: jmp short L0068
L005a: test rbx, rbx
L005d: je short L0066
L005f: mov eax, 1
L0064: jmp short L0068
L0066: xor eax, eax
L0068: add rsp, 0x20
L006c: pop rbx
L006d: pop rsi
L006e: pop rdi
L006f: ret
L0070: mov rcx, rsi
L0073: mov rdx, 0x7ff953a2e700
L007d: cmp [rcx], rdx
L0080: je short L008d
L0082: mov rcx, rdx
L0085: mov rdx, rsi
L0088: call System.Runtime.CompilerServices.CastHelpers.ChkCastClassSpecial(Void*, System.Object)
L008d: vmovsd xmm1, [rsi+0x10]
L0092: mov rcx, rdi
L0095: call Microsoft.FSharp.Core.LanguagePrimitives+HashCompare.GenericHashWithComparerIntrinsic[[System.Double, System.Private.CoreLib]](System.Collections.IEqualityComparer, Double)
L009a: lea ebx, [rax-0x61c88607]
L00a0: vmovsd xmm1, [rsi+8]
L00a5: mov rcx, rdi
L00a8: call Microsoft.FSharp.Core.LanguagePrimitives+HashCompare.GenericHashWithComparerIntrinsic[[System.Double, System.Private.CoreLib]](System.Collections.IEqualityComparer, Double)
L00ad: mov ecx, ebx
L00af: shl ecx, 6
L00b2: add eax, ecx
L00b4: sar ebx, 2
L00b7: lea ebx, [rax+rbx-0x61c88647]
L00be: mov eax, ebx
L00c0: add rsp, 0x20
L00c4: pop rbx
L00c5: pop rsi
L00c6: pop rdi
L00c7: ret
L00c8: mov rcx, rsi
L00cb: mov rdx, 0x7ff953a2e8a8
L00d5: cmp [rcx], rdx
L00d8: je short L00e5
L00da: mov rcx, rdx
L00dd: mov rdx, rsi
L00e0: call System.Runtime.CompilerServices.CastHelpers.ChkCastClassSpecial(Void*, System.Object)
L00e5: vmovsd xmm1, [rsi+0x10]
L00ea: mov rcx, rdi
L00ed: call Microsoft.FSharp.Core.LanguagePrimitives+HashCompare.GenericHashWithComparerIntrinsic[[System.Double, System.Private.CoreLib]](System.Collections.IEqualityComparer, Double)
L00f2: lea ebx, [rax-0x61c885c7]
L00f8: vmovsd xmm1, [rsi+8]
L00fd: mov rcx, rdi
L0100: call Microsoft.FSharp.Core.LanguagePrimitives+HashCompare.GenericHashWithComparerIntrinsic[[System.Double, System.Private.CoreLib]](System.Collections.IEqualityComparer, Double)
L0105: mov edx, ebx
L0107: shl edx, 6
L010a: add eax, edx
L010c: mov edx, ebx
L010e: sar edx, 2
L0111: lea ebx, [rax+rdx-0x61c88647]
L0118: mov eax, ebx
L011a: add rsp, 0x20
L011e: pop rbx
L011f: pop rsi
L0120: pop rdi
L0121: ret
L0122: xor eax, eax
L0124: add rsp, 0x20
L0128: pop rbx
L0129: pop rsi
L012a: pop rdi
L012b: ret

;Test+Test+DecisionType.GetHashCode()
L0000: mov rdx, 0x191f2a86fa8
L000a: mov rdx, [rdx]
L000d: cmp [rcx], ecx
L000f: jmp Test+Test+DecisionType.GetHashCode(System.Collections.IEqualityComparer)

;Test+Test+DecisionType.Equals(System.Object, System.Collections.IEqualityComparer)
L0000: push r14
L0002: push rdi
L0003: push rsi
L0004: push rbp
L0005: push rbx
L0006: sub rsp, 0x20
L000a: vzeroupper
L000d: mov rsi, rcx
L0010: test rsi, rsi
L0013: je L01a1
L0019: mov rcx, 0x7ff953a2e0b8
L0023: call System.Runtime.CompilerServices.CastHelpers.IsInstanceOfClass(Void*, System.Object)
L0028: mov rdi, rax
L002b: test rdi, rdi
L002e: je L0194
L0034: mov rdx, rsi
L0037: mov rcx, 0x7ff953a2e8a8
L0041: call System.Runtime.CompilerServices.CastHelpers.IsInstanceOfClass(Void*, System.Object)
L0046: mov rbx, rax
L0049: test rbx, rbx
L004c: je short L0055
L004e: mov ebp, 2
L0053: jmp short L0075
L0055: mov rdx, rsi
L0058: mov rcx, 0x7ff953a2e700
L0062: call System.Runtime.CompilerServices.CastHelpers.IsInstanceOfClass(Void*, System.Object)
L0067: test rax, rax
L006a: je short L0073
L006c: mov ebp, 1
L0071: jmp short L0075
L0073: xor ebp, ebp
L0075: mov rdx, rdi
L0078: mov rcx, 0x7ff953a2e8a8
L0082: call System.Runtime.CompilerServices.CastHelpers.IsInstanceOfClass(Void*, System.Object)
L0087: test rax, rax
L008a: je short L0094
L008c: mov r14d, 2
L0092: jmp short L00b6
L0094: mov rdx, rdi
L0097: mov rcx, 0x7ff953a2e700
L00a1: call System.Runtime.CompilerServices.CastHelpers.IsInstanceOfClass(Void*, System.Object)
L00a6: test rax, rax
L00a9: je short L00b3
L00ab: mov r14d, 1
L00b1: jmp short L00b6
L00b3: xor r14d, r14d
L00b6: cmp ebp, r14d
L00b9: jne L0194
L00bf: mov rdx, rsi
L00c2: mov rcx, 0x7ff953a2e700
L00cc: call System.Runtime.CompilerServices.CastHelpers.IsInstanceOfClass(Void*, System.Object)
L00d1: test rax, rax
L00d4: jne short L00eb
L00d6: test rbx, rbx
L00d9: jne short L0135
L00db: mov eax, 1
L00e0: add rsp, 0x20
L00e4: pop rbx
L00e5: pop rbp
L00e6: pop rsi
L00e7: pop rdi
L00e8: pop r14
L00ea: ret
L00eb: mov rbx, rdi
L00ee: mov rcx, 0x7ff953a2e700
L00f8: cmp [rbx], rcx
L00fb: je short L0108
L00fd: mov rdx, rdi
L0100: call System.Runtime.CompilerServices.CastHelpers.ChkCastClassSpecial(Void*, System.Object)
L0105: mov rbx, rax
L0108: vmovsd xmm0, [rsi+8]
L010d: vucomisd xmm0, [rbx+8]
L0112: jp L0194
L0118: jne L0194
L011e: vmovsd xmm0, [rsi+0x10]
L0123: vucomisd xmm0, [rbx+0x10]
L0128: setnp bl
L012b: jp short L0130
L012d: sete bl
L0130: movzx ebx, bl
L0133: jmp short L01aa
L0135: mov rcx, rsi
L0138: mov rdx, 0x7ff953a2e8a8
L0142: cmp [rcx], rdx
L0145: je short L0152
L0147: mov rcx, rdx
L014a: mov rdx, rsi
L014d: call System.Runtime.CompilerServices.CastHelpers.ChkCastClassSpecial(Void*, System.Object)
L0152: mov rbx, rdi
L0155: mov rcx, 0x7ff953a2e8a8
L015f: cmp [rbx], rcx
L0162: je short L016f
L0164: mov rdx, rdi
L0167: call System.Runtime.CompilerServices.CastHelpers.ChkCastClassSpecial(Void*, System.Object)
L016c: mov rbx, rax
L016f: vmovsd xmm0, [rsi+8]
L0174: vucomisd xmm0, [rbx+8]
L0179: jp short L0194
L017b: jne short L0194
L017d: vmovsd xmm0, [rsi+0x10]
L0182: vucomisd xmm0, [rbx+0x10]
L0187: setnp bl
L018a: jp short L018f
L018c: sete bl
L018f: movzx ebx, bl
L0192: jmp short L01aa
L0194: xor eax, eax
L0196: add rsp, 0x20
L019a: pop rbx
L019b: pop rbp
L019c: pop rsi
L019d: pop rdi
L019e: pop r14
L01a0: ret
L01a1: test rdx, rdx
L01a4: sete bl
L01a7: movzx ebx, bl
L01aa: movzx eax, bl
L01ad: add rsp, 0x20
L01b1: pop rbx
L01b2: pop rbp
L01b3: pop rsi
L01b4: pop rdi
L01b5: pop r14
L01b7: ret

;Test+Test+DecisionType.Equals(DecisionType)
L0000: push rdi
L0001: push rsi
L0002: push rbp
L0003: push rbx
L0004: sub rsp, 0x28
L0008: mov rdi, rcx
L000b: mov rsi, rdx
L000e: test rdi, rdi
L0011: je L00cc
L0017: test rsi, rsi
L001a: je L00c1
L0020: mov rdx, rdi
L0023: mov rcx, 0x7ff953a2e8a8
L002d: call System.Runtime.CompilerServices.CastHelpers.IsInstanceOfClass(Void*, System.Object)
L0032: test rax, rax
L0035: je short L003e
L0037: mov ebx, 2
L003c: jmp short L005e
L003e: mov rdx, rdi
L0041: mov rcx, 0x7ff953a2e700
L004b: call System.Runtime.CompilerServices.CastHelpers.IsInstanceOfClass(Void*, System.Object)
L0050: test rax, rax
L0053: je short L005c
L0055: mov ebx, 1
L005a: jmp short L005e
L005c: xor ebx, ebx
L005e: mov rdx, rsi
L0061: mov rcx, 0x7ff953a2e8a8
L006b: call System.Runtime.CompilerServices.CastHelpers.IsInstanceOfClass(Void*, System.Object)
L0070: test rax, rax
L0073: je short L007c
L0075: mov ebp, 2
L007a: jmp short L009c
L007c: mov rdx, rsi
L007f: mov rcx, 0x7ff953a2e700
L0089: call System.Runtime.CompilerServices.CastHelpers.IsInstanceOfClass(Void*, System.Object)
L008e: test rax, rax
L0091: je short L009a
L0093: mov ebp, 1
L0098: jmp short L009c
L009a: xor ebp, ebp
L009c: cmp ebx, ebp
L009e: jne short L00b6
L00a0: mov rcx, rdi
L00a3: mov rdx, rsi
L00a6: xor r8d, r8d
L00a9: add rsp, 0x28
L00ad: pop rbx
L00ae: pop rbp
L00af: pop rsi
L00b0: pop rdi
L00b1: jmp Test+Test.Equals$cont@6(DecisionType, DecisionType, Microsoft.FSharp.Core.Unit)
L00b6: xor eax, eax
L00b8: add rsp, 0x28
L00bc: pop rbx
L00bd: pop rbp
L00be: pop rsi
L00bf: pop rdi
L00c0: ret
L00c1: xor eax, eax
L00c3: add rsp, 0x28
L00c7: pop rbx
L00c8: pop rbp
L00c9: pop rsi
L00ca: pop rdi
L00cb: ret
L00cc: test rsi, rsi
L00cf: sete al
L00d2: movzx eax, al
L00d5: add rsp, 0x28
L00d9: pop rbx
L00da: pop rbp
L00db: pop rsi
L00dc: pop rdi
L00dd: ret

;Test+Test+DecisionType.Equals(System.Object)
L0000: push rsi
L0001: sub rsp, 0x20
L0005: mov rsi, rcx
L0008: mov rcx, 0x7ff953a2e0b8
L0012: call System.Runtime.CompilerServices.CastHelpers.IsInstanceOfClass(Void*, System.Object)
L0017: test rax, rax
L001a: je short L002e
L001c: mov rcx, rsi
L001f: mov rdx, rax
L0022: cmp [rcx], ecx
L0024: add rsp, 0x20
L0028: pop rsi
L0029: jmp Test+Test+DecisionType.Equals(DecisionType)
L002e: xor eax, eax
L0030: add rsp, 0x20
L0034: pop rsi
L0035: ret

;Test+Test+DecisionType+Continuous.get_LowerBound()
L0000: vzeroupper
L0003: vmovsd xmm0, [rcx+8]
L0008: ret

;Test+Test+DecisionType+Continuous.get_UpperBound()
L0000: vzeroupper
L0003: vmovsd xmm0, [rcx+0x10]
L0008: ret

;Test+Test+DecisionType.get_IsBoolean()
L0000: sub rsp, 0x28
L0004: mov rdx, rcx
L0007: mov rcx, 0x7ff953a2e558
L0011: call System.Runtime.CompilerServices.CastHelpers.IsInstanceOfClass(Void*, System.Object)
L0016: test rax, rax
L0019: setne al
L001c: movzx eax, al
L001f: add rsp, 0x28
L0023: ret

;Test+Test+DecisionType.get_IsInteger()
L0000: sub rsp, 0x28
L0004: mov rdx, rcx
L0007: mov rcx, 0x7ff953a2e700
L0011: call System.Runtime.CompilerServices.CastHelpers.IsInstanceOfClass(Void*, System.Object)
L0016: test rax, rax
L0019: setne al
L001c: movzx eax, al
L001f: add rsp, 0x28
L0023: ret

;Test+Test+DecisionType.get_IsContinuous()
L0000: sub rsp, 0x28
L0004: mov rdx, rcx
L0007: mov rcx, 0x7ff953a2e8a8
L0011: call System.Runtime.CompilerServices.CastHelpers.IsInstanceOfClass(Void*, System.Object)
L0016: test rax, rax
L0019: setne al
L001c: movzx eax, al
L001f: add rsp, 0x28
L0023: ret

;Test+Test+DecisionType.get_Tag()
L0000: push rsi
L0001: sub rsp, 0x20
L0005: mov rsi, rcx
L0008: mov rdx, rsi
L000b: mov rcx, 0x7ff953a2e8a8
L0015: call System.Runtime.CompilerServices.CastHelpers.IsInstanceOfClass(Void*, System.Object)
L001a: test rax, rax
L001d: je short L0026
L001f: mov eax, 2
L0024: jmp short L0046
L0026: mov rdx, rsi
L0029: mov rcx, 0x7ff953a2e700
L0033: call System.Runtime.CompilerServices.CastHelpers.IsInstanceOfClass(Void*, System.Object)
L0038: test rax, rax
L003b: je short L0044
L003d: mov eax, 1
L0042: jmp short L0046
L0044: xor eax, eax
L0046: add rsp, 0x20
L004a: pop rsi
L004b: ret

;Test+Test+DecisionType.ToString()
L0000: push rdi
L0001: push rsi
L0002: sub rsp, 0x28
L0006: mov rsi, rcx
L0009: mov rcx, 0x7ff953af5d58
L0013: call 0x00007ff9b182a470
L0018: mov rdi, rax
L001b: mov rdx, 0x191f2a9b3f0
L0025: mov rdx, [rdx]
L0028: lea rcx, [rdi+8]
L002c: call 0x00007ff9b182a080
L0031: xor edx, edx
L0033: mov [rdi+0x10], rdx
L0037: mov [rdi+0x18], rdx
L003b: mov rdx, rdi
L003e: mov rcx, 0x7ff953af6038
L0048: call Microsoft.FSharp.Core.PrintfModule.PrintFormatToStringThen[[System.__Canon, System.Private.CoreLib]](Microsoft.FSharp.Core.PrintfFormat`4<System.__Canon,Microsoft.FSharp.Core.Unit,System.String,System.String>)
L004d: mov rcx, rax
L0050: mov rdx, rsi
L0053: mov rax, [rax]
L0056: mov rax, [rax+0x40]
L005a: mov rax, [rax+0x20]
L005e: add rsp, 0x28
L0062: pop rsi
L0063: pop rdi
L0064: jmp rax

;Test+Test+DecisionType.CompareTo(DecisionType)
L0000: push rdi
L0001: push rsi
L0002: push rbp
L0003: push rbx
L0004: sub rsp, 0x28
L0008: mov rdi, rcx
L000b: mov rsi, rdx
L000e: test rdi, rdi
L0011: je L00ca
L0017: test rsi, rsi
L001a: je L00bc
L0020: mov rdx, rdi
L0023: mov rcx, 0x7ff953a2e8a8
L002d: call System.Runtime.CompilerServices.CastHelpers.IsInstanceOfClass(Void*, System.Object)
L0032: test rax, rax
L0035: je short L003e
L0037: mov ebx, 2
L003c: jmp short L005e
L003e: mov rdx, rdi
L0041: mov rcx, 0x7ff953a2e700
L004b: call System.Runtime.CompilerServices.CastHelpers.IsInstanceOfClass(Void*, System.Object)
L0050: test rax, rax
L0053: je short L005c
L0055: mov ebx, 1
L005a: jmp short L005e
L005c: xor ebx, ebx
L005e: mov rdx, rsi
L0061: mov rcx, 0x7ff953a2e8a8
L006b: call System.Runtime.CompilerServices.CastHelpers.IsInstanceOfClass(Void*, System.Object)
L0070: test rax, rax
L0073: je short L007c
L0075: mov ebp, 2
L007a: jmp short L009c
L007c: mov rdx, rsi
L007f: mov rcx, 0x7ff953a2e700
L0089: call System.Runtime.CompilerServices.CastHelpers.IsInstanceOfClass(Void*, System.Object)
L008e: test rax, rax
L0091: je short L009a
L0093: mov ebp, 1
L0098: jmp short L009c
L009a: xor ebp, ebp
L009c: cmp ebx, ebp
L009e: jne short L00b6
L00a0: mov rcx, rdi
L00a3: mov rdx, rsi
L00a6: xor r8d, r8d
L00a9: add rsp, 0x28
L00ad: pop rbx
L00ae: pop rbp
L00af: pop rsi
L00b0: pop rdi
L00b1: jmp Test+Test.CompareTo$cont@6(DecisionType, DecisionType, Microsoft.FSharp.Core.Unit)
L00b6: mov eax, ebx
L00b8: sub eax, ebp
L00ba: jmp short L00e8
L00bc: mov eax, 1
L00c1: add rsp, 0x28
L00c5: pop rbx
L00c6: pop rbp
L00c7: pop rsi
L00c8: pop rdi
L00c9: ret
L00ca: test rsi, rsi
L00cd: je short L00dd
L00cf: mov eax, 0xffffffff
L00d4: add rsp, 0x28
L00d8: pop rbx
L00d9: pop rbp
L00da: pop rsi
L00db: pop rdi
L00dc: ret
L00dd: xor eax, eax
L00df: add rsp, 0x28
L00e3: pop rbx
L00e4: pop rbp
L00e5: pop rsi
L00e6: pop rdi
L00e7: ret
L00e8: add rsp, 0x28
L00ec: pop rbx
L00ed: pop rbp
L00ee: pop rsi
L00ef: pop rdi
L00f0: ret

;Test+Test+DecisionType.CompareTo(System.Object)
L0000: push rsi
L0001: sub rsp, 0x20
L0005: mov rsi, rcx
L0008: mov rax, rdx
L000b: test rax, rax
L000e: je short L0024
L0010: mov rcx, 0x7ff953a2e0b8
L001a: cmp [rax], rcx
L001d: je short L0024
L001f: call System.Runtime.CompilerServices.CastHelpers.ChkCastClassSpecial(Void*, System.Object)
L0024: mov rcx, rsi
L0027: mov rdx, rax
L002a: cmp [rcx], ecx
L002c: add rsp, 0x20
L0030: pop rsi
L0031: jmp Test+Test+DecisionType.CompareTo(DecisionType)

;Test+Test+DecisionType.CompareTo(System.Object, System.Collections.IComparer)
L0000: push r14
L0002: push rdi
L0003: push rsi
L0004: push rbp
L0005: push rbx
L0006: sub rsp, 0x20
L000a: mov rdi, rcx
L000d: mov rsi, rdx
L0010: mov rbx, r8
L0013: mov rcx, rsi
L0016: test rcx, rcx
L0019: je short L0035
L001b: mov rdx, 0x7ff953a2e0b8
L0025: cmp [rcx], rdx
L0028: je short L0035
L002a: mov rcx, rdx
L002d: mov rdx, rsi
L0030: call System.Runtime.CompilerServices.CastHelpers.ChkCastClassSpecial(Void*, System.Object)
L0035: mov rbp, rsi
L0038: test rdi, rdi
L003b: je L011d
L0041: test rbp, rbp
L0044: je short L005e
L0046: mov rcx, 0x7ff953a2e0b8
L0050: cmp [rbp], rcx
L0054: je short L005e
L0056: mov rdx, rbp
L0059: call System.Runtime.CompilerServices.CastHelpers.ChkCastClassSpecial(Void*, System.Object)
L005e: test rbp, rbp
L0061: je L010d
L0067: mov rdx, rdi
L006a: mov rcx, 0x7ff953a2e8a8
L0074: call System.Runtime.CompilerServices.CastHelpers.IsInstanceOfClass(Void*, System.Object)
L0079: test rax, rax
L007c: je short L0085
L007e: mov esi, 2
L0083: jmp short L00a5
L0085: mov rdx, rdi
L0088: mov rcx, 0x7ff953a2e700
L0092: call System.Runtime.CompilerServices.CastHelpers.IsInstanceOfClass(Void*, System.Object)
L0097: test rax, rax
L009a: je short L00a3
L009c: mov esi, 1
L00a1: jmp short L00a5
L00a3: xor esi, esi
L00a5: mov rdx, rbp
L00a8: mov rcx, 0x7ff953a2e8a8
L00b2: call System.Runtime.CompilerServices.CastHelpers.IsInstanceOfClass(Void*, System.Object)
L00b7: test rax, rax
L00ba: je short L00c4
L00bc: mov r14d, 2
L00c2: jmp short L00e6
L00c4: mov rdx, rbp
L00c7: mov rcx, 0x7ff953a2e700
L00d1: call System.Runtime.CompilerServices.CastHelpers.IsInstanceOfClass(Void*, System.Object)
L00d6: test rax, rax
L00d9: je short L00e3
L00db: mov r14d, 1
L00e1: jmp short L00e6
L00e3: xor r14d, r14d
L00e6: cmp esi, r14d
L00e9: jne short L0106
L00eb: mov rcx, rbx
L00ee: mov rdx, rdi
L00f1: mov r8, rbp
L00f4: xor r9d, r9d
L00f7: add rsp, 0x20
L00fb: pop rbx
L00fc: pop rbp
L00fd: pop rsi
L00fe: pop rdi
L00ff: pop r14
L0101: jmp Test+Test.CompareTo$cont@6-1(System.Collections.IComparer, DecisionType, DecisionType, Microsoft.FSharp.Core.Unit)
L0106: mov eax, esi
L0108: sub eax, r14d
L010b: jmp short L015e
L010d: mov eax, 1
L0112: add rsp, 0x20
L0116: pop rbx
L0117: pop rbp
L0118: pop rsi
L0119: pop rdi
L011a: pop r14
L011c: ret
L011d: mov rax, rsi
L0120: test rax, rax
L0123: je short L013c
L0125: mov rcx, 0x7ff953a2e0b8
L012f: cmp [rax], rcx
L0132: je short L013c
L0134: mov rdx, rsi
L0137: call System.Runtime.CompilerServices.CastHelpers.ChkCastClassSpecial(Void*, System.Object)
L013c: test rax, rax
L013f: je short L0151
L0141: mov eax, 0xffffffff
L0146: add rsp, 0x20
L014a: pop rbx
L014b: pop rbp
L014c: pop rsi
L014d: pop rdi
L014e: pop r14
L0150: ret
L0151: xor eax, eax
L0153: add rsp, 0x20
L0157: pop rbx
L0158: pop rbp
L0159: pop rsi
L015a: pop rdi
L015b: pop r14
L015d: ret
L015e: add rsp, 0x20
L0162: pop rbx
L0163: pop rbp
L0164: pop rsi
L0165: pop rdi
L0166: pop r14
L0168: ret

;Test+Test+DecisionType.GetHashCode(System.Collections.IEqualityComparer)
L0000: push rdi
L0001: push rsi
L0002: push rbx
L0003: sub rsp, 0x20
L0007: vzeroupper
L000a: mov rsi, rcx
L000d: mov rdi, rdx
L0010: test rsi, rsi
L0013: je L0122
L0019: mov rdx, rsi
L001c: mov rcx, 0x7ff953a2e700
L0026: call System.Runtime.CompilerServices.CastHelpers.IsInstanceOfClass(Void*, System.Object)
L002b: mov rbx, rax
L002e: test rbx, rbx
L0031: jne short L0070
L0033: mov rdx, rsi
L0036: mov rcx, 0x7ff953a2e8a8
L0040: call System.Runtime.CompilerServices.CastHelpers.IsInstanceOfClass(Void*, System.Object)
L0045: test rax, rax
L0048: jne L00c8
L004e: test rax, rax
L0051: je short L005a
L0053: mov eax, 2
L0058: jmp short L0068
L005a: test rbx, rbx
L005d: je short L0066
L005f: mov eax, 1
L0064: jmp short L0068
L0066: xor eax, eax
L0068: add rsp, 0x20
L006c: pop rbx
L006d: pop rsi
L006e: pop rdi
L006f: ret
L0070: mov rcx, rsi
L0073: mov rdx, 0x7ff953a2e700
L007d: cmp [rcx], rdx
L0080: je short L008d
L0082: mov rcx, rdx
L0085: mov rdx, rsi
L0088: call System.Runtime.CompilerServices.CastHelpers.ChkCastClassSpecial(Void*, System.Object)
L008d: vmovsd xmm1, [rsi+0x10]
L0092: mov rcx, rdi
L0095: call Microsoft.FSharp.Core.LanguagePrimitives+HashCompare.GenericHashWithComparerIntrinsic[[System.Double, System.Private.CoreLib]](System.Collections.IEqualityComparer, Double)
L009a: lea ebx, [rax-0x61c88607]
L00a0: vmovsd xmm1, [rsi+8]
L00a5: mov rcx, rdi
L00a8: call Microsoft.FSharp.Core.LanguagePrimitives+HashCompare.GenericHashWithComparerIntrinsic[[System.Double, System.Private.CoreLib]](System.Collections.IEqualityComparer, Double)
L00ad: mov ecx, ebx
L00af: shl ecx, 6
L00b2: add eax, ecx
L00b4: sar ebx, 2
L00b7: lea ebx, [rax+rbx-0x61c88647]
L00be: mov eax, ebx
L00c0: add rsp, 0x20
L00c4: pop rbx
L00c5: pop rsi
L00c6: pop rdi
L00c7: ret
L00c8: mov rcx, rsi
L00cb: mov rdx, 0x7ff953a2e8a8
L00d5: cmp [rcx], rdx
L00d8: je short L00e5
L00da: mov rcx, rdx
L00dd: mov rdx, rsi
L00e0: call System.Runtime.CompilerServices.CastHelpers.ChkCastClassSpecial(Void*, System.Object)
L00e5: vmovsd xmm1, [rsi+0x10]
L00ea: mov rcx, rdi
L00ed: call Microsoft.FSharp.Core.LanguagePrimitives+HashCompare.GenericHashWithComparerIntrinsic[[System.Double, System.Private.CoreLib]](System.Collections.IEqualityComparer, Double)
L00f2: lea ebx, [rax-0x61c885c7]
L00f8: vmovsd xmm1, [rsi+8]
L00fd: mov rcx, rdi
L0100: call Microsoft.FSharp.Core.LanguagePrimitives+HashCompare.GenericHashWithComparerIntrinsic[[System.Double, System.Private.CoreLib]](System.Collections.IEqualityComparer, Double)
L0105: mov edx, ebx
L0107: shl edx, 6
L010a: add eax, edx
L010c: mov edx, ebx
L010e: sar edx, 2
L0111: lea ebx, [rax+rdx-0x61c88647]
L0118: mov eax, ebx
L011a: add rsp, 0x20
L011e: pop rbx
L011f: pop rsi
L0120: pop rdi
L0121: ret
L0122: xor eax, eax
L0124: add rsp, 0x20
L0128: pop rbx
L0129: pop rsi
L012a: pop rdi
L012b: ret

;Test+Test+DecisionType.GetHashCode()
L0000: mov rdx, 0x191f2a86fa8
L000a: mov rdx, [rdx]
L000d: cmp [rcx], ecx
L000f: jmp Test+Test+DecisionType.GetHashCode(System.Collections.IEqualityComparer)

;Test+Test+DecisionType.Equals(System.Object, System.Collections.IEqualityComparer)
L0000: push r14
L0002: push rdi
L0003: push rsi
L0004: push rbp
L0005: push rbx
L0006: sub rsp, 0x20
L000a: vzeroupper
L000d: mov rsi, rcx
L0010: test rsi, rsi
L0013: je L01a1
L0019: mov rcx, 0x7ff953a2e0b8
L0023: call System.Runtime.CompilerServices.CastHelpers.IsInstanceOfClass(Void*, System.Object)
L0028: mov rdi, rax
L002b: test rdi, rdi
L002e: je L0194
L0034: mov rdx, rsi
L0037: mov rcx, 0x7ff953a2e8a8
L0041: call System.Runtime.CompilerServices.CastHelpers.IsInstanceOfClass(Void*, System.Object)
L0046: mov rbx, rax
L0049: test rbx, rbx
L004c: je short L0055
L004e: mov ebp, 2
L0053: jmp short L0075
L0055: mov rdx, rsi
L0058: mov rcx, 0x7ff953a2e700
L0062: call System.Runtime.CompilerServices.CastHelpers.IsInstanceOfClass(Void*, System.Object)
L0067: test rax, rax
L006a: je short L0073
L006c: mov ebp, 1
L0071: jmp short L0075
L0073: xor ebp, ebp
L0075: mov rdx, rdi
L0078: mov rcx, 0x7ff953a2e8a8
L0082: call System.Runtime.CompilerServices.CastHelpers.IsInstanceOfClass(Void*, System.Object)
L0087: test rax, rax
L008a: je short L0094
L008c: mov r14d, 2
L0092: jmp short L00b6
L0094: mov rdx, rdi
L0097: mov rcx, 0x7ff953a2e700
L00a1: call System.Runtime.CompilerServices.CastHelpers.IsInstanceOfClass(Void*, System.Object)
L00a6: test rax, rax
L00a9: je short L00b3
L00ab: mov r14d, 1
L00b1: jmp short L00b6
L00b3: xor r14d, r14d
L00b6: cmp ebp, r14d
L00b9: jne L0194
L00bf: mov rdx, rsi
L00c2: mov rcx, 0x7ff953a2e700
L00cc: call System.Runtime.CompilerServices.CastHelpers.IsInstanceOfClass(Void*, System.Object)
L00d1: test rax, rax
L00d4: jne short L00eb
L00d6: test rbx, rbx
L00d9: jne short L0135
L00db: mov eax, 1
L00e0: add rsp, 0x20
L00e4: pop rbx
L00e5: pop rbp
L00e6: pop rsi
L00e7: pop rdi
L00e8: pop r14
L00ea: ret
L00eb: mov rbx, rdi
L00ee: mov rcx, 0x7ff953a2e700
L00f8: cmp [rbx], rcx
L00fb: je short L0108
L00fd: mov rdx, rdi
L0100: call System.Runtime.CompilerServices.CastHelpers.ChkCastClassSpecial(Void*, System.Object)
L0105: mov rbx, rax
L0108: vmovsd xmm0, [rsi+8]
L010d: vucomisd xmm0, [rbx+8]
L0112: jp L0194
L0118: jne L0194
L011e: vmovsd xmm0, [rsi+0x10]
L0123: vucomisd xmm0, [rbx+0x10]
L0128: setnp bl
L012b: jp short L0130
L012d: sete bl
L0130: movzx ebx, bl
L0133: jmp short L01aa
L0135: mov rcx, rsi
L0138: mov rdx, 0x7ff953a2e8a8
L0142: cmp [rcx], rdx
L0145: je short L0152
L0147: mov rcx, rdx
L014a: mov rdx, rsi
L014d: call System.Runtime.CompilerServices.CastHelpers.ChkCastClassSpecial(Void*, System.Object)
L0152: mov rbx, rdi
L0155: mov rcx, 0x7ff953a2e8a8
L015f: cmp [rbx], rcx
L0162: je short L016f
L0164: mov rdx, rdi
L0167: call System.Runtime.CompilerServices.CastHelpers.ChkCastClassSpecial(Void*, System.Object)
L016c: mov rbx, rax
L016f: vmovsd xmm0, [rsi+8]
L0174: vucomisd xmm0, [rbx+8]
L0179: jp short L0194
L017b: jne short L0194
L017d: vmovsd xmm0, [rsi+0x10]
L0182: vucomisd xmm0, [rbx+0x10]
L0187: setnp bl
L018a: jp short L018f
L018c: sete bl
L018f: movzx ebx, bl
L0192: jmp short L01aa
L0194: xor eax, eax
L0196: add rsp, 0x20
L019a: pop rbx
L019b: pop rbp
L019c: pop rsi
L019d: pop rdi
L019e: pop r14
L01a0: ret
L01a1: test rdx, rdx
L01a4: sete bl
L01a7: movzx ebx, bl
L01aa: movzx eax, bl
L01ad: add rsp, 0x20
L01b1: pop rbx
L01b2: pop rbp
L01b3: pop rsi
L01b4: pop rdi
L01b5: pop r14
L01b7: ret

;Test+Test+DecisionType.Equals(DecisionType)
L0000: push rdi
L0001: push rsi
L0002: push rbp
L0003: push rbx
L0004: sub rsp, 0x28
L0008: mov rdi, rcx
L000b: mov rsi, rdx
L000e: test rdi, rdi
L0011: je L00cc
L0017: test rsi, rsi
L001a: je L00c1
L0020: mov rdx, rdi
L0023: mov rcx, 0x7ff953a2e8a8
L002d: call System.Runtime.CompilerServices.CastHelpers.IsInstanceOfClass(Void*, System.Object)
L0032: test rax, rax
L0035: je short L003e
L0037: mov ebx, 2
L003c: jmp short L005e
L003e: mov rdx, rdi
L0041: mov rcx, 0x7ff953a2e700
L004b: call System.Runtime.CompilerServices.CastHelpers.IsInstanceOfClass(Void*, System.Object)
L0050: test rax, rax
L0053: je short L005c
L0055: mov ebx, 1
L005a: jmp short L005e
L005c: xor ebx, ebx
L005e: mov rdx, rsi
L0061: mov rcx, 0x7ff953a2e8a8
L006b: call System.Runtime.CompilerServices.CastHelpers.IsInstanceOfClass(Void*, System.Object)
L0070: test rax, rax
L0073: je short L007c
L0075: mov ebp, 2
L007a: jmp short L009c
L007c: mov rdx, rsi
L007f: mov rcx, 0x7ff953a2e700
L0089: call System.Runtime.CompilerServices.CastHelpers.IsInstanceOfClass(Void*, System.Object)
L008e: test rax, rax
L0091: je short L009a
L0093: mov ebp, 1
L0098: jmp short L009c
L009a: xor ebp, ebp
L009c: cmp ebx, ebp
L009e: jne short L00b6
L00a0: mov rcx, rdi
L00a3: mov rdx, rsi
L00a6: xor r8d, r8d
L00a9: add rsp, 0x28
L00ad: pop rbx
L00ae: pop rbp
L00af: pop rsi
L00b0: pop rdi
L00b1: jmp Test+Test.Equals$cont@6(DecisionType, DecisionType, Microsoft.FSharp.Core.Unit)
L00b6: xor eax, eax
L00b8: add rsp, 0x28
L00bc: pop rbx
L00bd: pop rbp
L00be: pop rsi
L00bf: pop rdi
L00c0: ret
L00c1: xor eax, eax
L00c3: add rsp, 0x28
L00c7: pop rbx
L00c8: pop rbp
L00c9: pop rsi
L00ca: pop rdi
L00cb: ret
L00cc: test rsi, rsi
L00cf: sete al
L00d2: movzx eax, al
L00d5: add rsp, 0x28
L00d9: pop rbx
L00da: pop rbp
L00db: pop rsi
L00dc: pop rdi
L00dd: ret

;Test+Test+DecisionType.Equals(System.Object)
L0000: push rsi
L0001: sub rsp, 0x20
L0005: mov rsi, rcx
L0008: mov rcx, 0x7ff953a2e0b8
L0012: call System.Runtime.CompilerServices.CastHelpers.IsInstanceOfClass(Void*, System.Object)
L0017: test rax, rax
L001a: je short L002e
L001c: mov rcx, rsi
L001f: mov rdx, rax
L0022: cmp [rcx], ecx
L0024: add rsp, 0x20
L0028: pop rsi
L0029: jmp Test+Test+DecisionType.Equals(DecisionType)
L002e: xor eax, eax
L0030: add rsp, 0x20
L0034: pop rsi
L0035: ret

;Test+Test+DecisionType+_Boolean@DebugTypeProxy..ctor(_Boolean)
L0000: lea rcx, [rcx+8]
L0004: call 0x00007ff9b182a080
L0009: nop
L000a: ret

;Test+Test+DecisionType+Integer@DebugTypeProxy..ctor(Integer)
L0000: lea rcx, [rcx+8]
L0004: call 0x00007ff9b182a080
L0009: nop
L000a: ret

;Test+Test+DecisionType+Integer@DebugTypeProxy.get_LowerBound()
L0000: vzeroupper
L0003: mov rax, [rcx+8]
L0007: vmovsd xmm0, [rax+8]
L000c: ret

;Test+Test+DecisionType+Integer@DebugTypeProxy.get_UpperBound()
L0000: vzeroupper
L0003: mov rax, [rcx+8]
L0007: vmovsd xmm0, [rax+0x10]
L000c: ret

;Test+Test+DecisionType+Continuous@DebugTypeProxy..ctor(Continuous)
L0000: lea rcx, [rcx+8]
L0004: call 0x00007ff9b182a080
L0009: nop
L000a: ret

;Test+Test+DecisionType+Continuous@DebugTypeProxy.get_LowerBound()
L0000: vzeroupper
L0003: mov rax, [rcx+8]
L0007: vmovsd xmm0, [rax+8]
L000c: ret

;Test+Test+DecisionType+Continuous@DebugTypeProxy.get_UpperBound()
L0000: vzeroupper
L0003: mov rax, [rcx+8]
L0007: vmovsd xmm0, [rax+0x10]
L000c: ret

;Test+Test+DecisionName.NewDecisionName(System.String)
L0000: push rdi
L0001: push rsi
L0002: sub rsp, 0x28
L0006: mov rsi, rcx
L0009: mov rcx, 0x7ff953af0038
L0013: call 0x00007ff9b182a470
L0018: mov rdi, rax
L001b: lea rcx, [rdi+8]
L001f: mov rdx, rsi
L0022: call 0x00007ff9b182a080
L0027: mov rax, rdi
L002a: add rsp, 0x28
L002e: pop rsi
L002f: pop rdi
L0030: ret

;Test+Test+DecisionName.get_Item()
L0000: mov rax, [rcx+8]
L0004: ret

;Test+Test+DecisionName.get_Tag()
L0000: xor eax, eax
L0002: ret

;Test+Test+DecisionName.ToString()
L0000: push rdi
L0001: push rsi
L0002: sub rsp, 0x28
L0006: mov rsi, rcx
L0009: mov rcx, 0x7ff953af67e0
L0013: call 0x00007ff9b182a470
L0018: mov rdi, rax
L001b: mov rdx, 0x191f2a9b3f0
L0025: mov rdx, [rdx]
L0028: lea rcx, [rdi+8]
L002c: call 0x00007ff9b182a080
L0031: xor edx, edx
L0033: mov [rdi+0x10], rdx
L0037: mov [rdi+0x18], rdx
L003b: mov rdx, rdi
L003e: mov rcx, 0x7ff953af6ac0
L0048: call Microsoft.FSharp.Core.PrintfModule.PrintFormatToStringThen[[System.__Canon, System.Private.CoreLib]](Microsoft.FSharp.Core.PrintfFormat`4<System.__Canon,Microsoft.FSharp.Core.Unit,System.String,System.String>)
L004d: mov rcx, rax
L0050: mov rdx, rsi
L0053: mov rax, [rax]
L0056: mov rax, [rax+0x40]
L005a: mov rax, [rax+0x20]
L005e: add rsp, 0x28
L0062: pop rsi
L0063: pop rdi
L0064: jmp rax

;Test+Test+DecisionName.CompareTo(DecisionName)
L0000: test rcx, rcx
L0003: je short L001d
L0005: test rdx, rdx
L0008: je short L0017
L000a: mov rcx, [rcx+8]
L000e: mov rdx, [rdx+8]
L0012: jmp System.String.CompareOrdinal(System.String, System.String)
L0017: mov eax, 1
L001c: ret
L001d: test rdx, rdx
L0020: je short L0028
L0022: mov eax, 0xffffffff
L0027: ret
L0028: xor eax, eax
L002a: ret

;Test+Test+DecisionName.CompareTo(System.Object)
L0000: push rsi
L0001: sub rsp, 0x20
L0005: mov rsi, rcx
L0008: mov rax, rdx
L000b: test rax, rax
L000e: je short L0024
L0010: mov rcx, 0x7ff953af0038
L001a: cmp [rax], rcx
L001d: je short L0024
L001f: call System.Runtime.CompilerServices.CastHelpers.ChkCastClassSpecial(Void*, System.Object)
L0024: mov rcx, rsi
L0027: mov rdx, rax
L002a: cmp [rcx], ecx
L002c: add rsp, 0x20
L0030: pop rsi
L0031: jmp Test+Test+DecisionName.CompareTo(DecisionName)

;Test+Test+DecisionName.CompareTo(System.Object, System.Collections.IComparer)
L0000: push rdi
L0001: push rsi
L0002: push rbx
L0003: sub rsp, 0x20
L0007: mov rdi, rcx
L000a: mov rsi, rdx
L000d: mov rcx, rsi
L0010: test rcx, rcx
L0013: je short L002f
L0015: mov rdx, 0x7ff953af0038
L001f: cmp [rcx], rdx
L0022: je short L002f
L0024: mov rcx, rdx
L0027: mov rdx, rsi
L002a: call System.Runtime.CompilerServices.CastHelpers.ChkCastClassSpecial(Void*, System.Object)
L002f: mov rbx, rsi
L0032: test rdi, rdi
L0035: je short L0079
L0037: test rbx, rbx
L003a: je short L0053
L003c: mov rcx, 0x7ff953af0038
L0046: cmp [rbx], rcx
L0049: je short L0053
L004b: mov rdx, rbx
L004e: call System.Runtime.CompilerServices.CastHelpers.ChkCastClassSpecial(Void*, System.Object)
L0053: test rbx, rbx
L0056: je short L006c
L0058: mov rcx, [rdi+8]
L005c: mov rdx, [rbx+8]
L0060: add rsp, 0x20
L0064: pop rbx
L0065: pop rsi
L0066: pop rdi
L0067: jmp System.String.CompareOrdinal(System.String, System.String)
L006c: mov eax, 1
L0071: add rsp, 0x20
L0075: pop rbx
L0076: pop rsi
L0077: pop rdi
L0078: ret
L0079: mov rax, rsi
L007c: test rax, rax
L007f: je short L0098
L0081: mov rcx, 0x7ff953af0038
L008b: cmp [rax], rcx
L008e: je short L0098
L0090: mov rdx, rsi
L0093: call System.Runtime.CompilerServices.CastHelpers.ChkCastClassSpecial(Void*, System.Object)
L0098: test rax, rax
L009b: je short L00aa
L009d: mov eax, 0xffffffff
L00a2: add rsp, 0x20
L00a6: pop rbx
L00a7: pop rsi
L00a8: pop rdi
L00a9: ret
L00aa: xor eax, eax
L00ac: add rsp, 0x20
L00b0: pop rbx
L00b1: pop rsi
L00b2: pop rdi
L00b3: ret

;Test+Test+DecisionName.GetHashCode(System.Collections.IEqualityComparer)
L0000: sub rsp, 0x28
L0004: test rcx, rcx
L0007: je short L003a
L0009: mov rdx, [rcx+8]
L000d: test rdx, rdx
L0010: je short L002e
L0012: lea rcx, [rdx+0xc]
L0016: mov edx, [rdx+8]
L0019: add edx, edx
L001b: mov r8d, 0x315ddb11
L0021: mov r9d, 0xa84d9600
L0027: call System.Marvin.ComputeHash32(Byte ByRef, UInt32, UInt32, UInt32)
L002c: jmp short L0030
L002e: xor eax, eax
L0030: add eax, 0x9e3779b9
L0035: add rsp, 0x28
L0039: ret
L003a: xor eax, eax
L003c: add rsp, 0x28
L0040: ret

;Test+Test+DecisionName.GetHashCode()
L0000: mov rdx, 0x191f2a86fa8
L000a: mov rdx, [rdx]
L000d: cmp [rcx], ecx
L000f: jmp Test+Test+DecisionName.GetHashCode(System.Collections.IEqualityComparer)

;Test+Test+DecisionName.Equals(System.Object, System.Collections.IEqualityComparer)
L0000: test rcx, rcx
L0003: je short L0030
L0005: test rdx, rdx
L0008: je short L001b
L000a: mov rax, 0x7ff953af0038
L0014: cmp [rdx], rax
L0017: je short L001b
L0019: xor edx, edx
L001b: test rdx, rdx
L001e: je short L002d
L0020: mov rcx, [rcx+8]
L0024: mov rdx, [rdx+8]
L0028: jmp System.String.Equals(System.String, System.String)
L002d: xor eax, eax
L002f: ret
L0030: test rdx, rdx
L0033: sete al
L0036: movzx eax, al
L0039: ret

;Test+Test+DecisionName.Equals(DecisionName)
L0000: test rcx, rcx
L0003: je short L001a
L0005: test rdx, rdx
L0008: je short L0017
L000a: mov rcx, [rcx+8]
L000e: mov rdx, [rdx+8]
L0012: jmp System.String.Equals(System.String, System.String)
L0017: xor eax, eax
L0019: ret
L001a: test rdx, rdx
L001d: sete al
L0020: movzx eax, al
L0023: ret

;Test+Test+DecisionName.Equals(System.Object)
L0000: test rdx, rdx
L0003: je short L0016
L0005: mov rax, 0x7ff953af0038
L000f: cmp [rdx], rax
L0012: je short L0016
L0014: xor edx, edx
L0016: test rdx, rdx
L0019: je short L0022
L001b: cmp [rcx], ecx
L001d: jmp Test+Test+DecisionName.Equals(DecisionName)
L0022: xor eax, eax
L0024: ret

;Test+Test+Decision..ctor(DecisionName, DecisionType)
L0000: push rdi
L0001: push rsi
L0002: mov rsi, rcx
L0005: mov rdi, r8
L0008: mov rcx, rsi
L000b: call 0x00007ff9b182a050
L0010: lea rcx, [rsi+8]
L0014: mov rdx, rdi
L0017: call 0x00007ff9b182a050
L001c: nop
L001d: pop rsi
L001e: pop rdi
L001f: ret

;Test+Test+Decision.get_Name()
L0000: mov rax, [rcx]
L0003: ret

;Test+Test+Decision.get_Type()
L0000: mov rax, [rcx+8]
L0004: ret

;Test+Test+Decision.op_Addition(Double, Decision)
L0000: push rdi
L0001: push rsi
L0002: push rbp
L0003: push rbx
L0004: sub rsp, 0x28
L0008: vzeroupper
L000b: mov rsi, rdx
L000e: vmovsd [rsp+0x50], xmm0
L0014: mov rcx, 0x7ff953af0ff8
L001e: call 0x00007ff9b182a470
L0023: mov rdi, rax
L0026: xor ecx, ecx
L0028: mov [rdi+8], ecx
L002b: vmovsd xmm0, [rsp+0x50]
L0031: vmovsd [rdi+0x10], xmm0
L0036: mov rbx, [rsi]
L0039: mov rsi, [rsi+8]
L003d: mov rcx, 0x7ff953af1178
L0047: call 0x00007ff9b182a470
L004c: mov rbp, rax
L004f: mov rdx, rbx
L0052: mov dword ptr [rbp+8], 1
L0059: lea rbx, [rbp+0x10]
L005d: mov rcx, rbx
L0060: call 0x00007ff9b182a050
L0065: lea rcx, [rbx+8]
L0069: mov rdx, rsi
L006c: call 0x00007ff9b182a050
L0071: mov rcx, 0x7ff953af14c8
L007b: call 0x00007ff9b182a470
L0080: mov rsi, rax
L0083: mov dword ptr [rsi+8], 3
L008a: lea rcx, [rsi+0x10]
L008e: mov rdx, rdi
L0091: call 0x00007ff9b182a080
L0096: lea rcx, [rsi+0x18]
L009a: mov rdx, rbp
L009d: call 0x00007ff9b182a080
L00a2: mov rax, rsi
L00a5: add rsp, 0x28
L00a9: pop rbx
L00aa: pop rbp
L00ab: pop rsi
L00ac: pop rdi
L00ad: ret

;Test+Test+Decision.op_Addition(Decision, Decision)
L0000: push rdi
L0001: push rsi
L0002: push rbp
L0003: push rbx
L0004: sub rsp, 0x28
L0008: mov rsi, rdx
L000b: mov rdi, [rcx]
L000e: mov rbx, [rcx+8]
L0012: mov rcx, 0x7ff953af1178
L001c: call 0x00007ff9b182a470
L0021: mov rbp, rax
L0024: mov rdx, rdi
L0027: mov dword ptr [rbp+8], 1
L002e: lea rdi, [rbp+0x10]
L0032: mov rcx, rdi
L0035: call 0x00007ff9b182a050
L003a: lea rcx, [rdi+8]
L003e: mov rdx, rbx
L0041: call 0x00007ff9b182a050
L0046: mov rdi, [rsi]
L0049: mov rsi, [rsi+8]
L004d: mov rcx, 0x7ff953af1178
L0057: call 0x00007ff9b182a470
L005c: mov rbx, rax
L005f: mov rdx, rdi
L0062: mov dword ptr [rbx+8], 1
L0069: lea rdi, [rbx+0x10]
L006d: mov rcx, rdi
L0070: call 0x00007ff9b182a050
L0075: lea rcx, [rdi+8]
L0079: mov rdx, rsi
L007c: call 0x00007ff9b182a050
L0081: mov rcx, 0x7ff953af14c8
L008b: call 0x00007ff9b182a470
L0090: mov rsi, rax
L0093: mov dword ptr [rsi+8], 3
L009a: lea rcx, [rsi+0x10]
L009e: mov rdx, rbp
L00a1: call 0x00007ff9b182a080
L00a6: lea rcx, [rsi+0x18]
L00aa: mov rdx, rbx
L00ad: call 0x00007ff9b182a080
L00b2: mov rax, rsi
L00b5: add rsp, 0x28
L00b9: pop rbx
L00ba: pop rbp
L00bb: pop rsi
L00bc: pop rdi
L00bd: ret

;Test+Test+Decision.op_Multiply(Double, Decision)
L0000: push rdi
L0001: push rsi
L0002: push rbx
L0003: sub rsp, 0x20
L0007: vzeroupper
L000a: vmovsd [rsp+0x40], xmm0
L0010: mov rsi, [rdx]
L0013: mov rdi, [rdx+8]
L0017: mov rcx, 0x7ff953af1178
L0021: call 0x00007ff9b182a470
L0026: mov rbx, rax
L0029: mov rdx, rsi
L002c: mov dword ptr [rbx+8], 1
L0033: lea rsi, [rbx+0x10]
L0037: mov rcx, rsi
L003a: call 0x00007ff9b182a050
L003f: lea rcx, [rsi+8]
L0043: mov rdx, rdi
L0046: call 0x00007ff9b182a050
L004b: mov rcx, 0x7ff953af1320
L0055: call 0x00007ff9b182a470
L005a: mov rsi, rax
L005d: mov dword ptr [rsi+8], 2
L0064: vmovsd xmm0, [rsp+0x40]
L006a: vmovsd [rsi+0x18], xmm0
L006f: lea rcx, [rsi+0x10]
L0073: mov rdx, rbx
L0076: call 0x00007ff9b182a080
L007b: mov rax, rsi
L007e: add rsp, 0x20
L0082: pop rbx
L0083: pop rsi
L0084: pop rdi
L0085: ret

;Test+Test+Decision.op_Multiply(Decision, Double)
L0000: push rdi
L0001: push rsi
L0002: push rbx
L0003: sub rsp, 0x20
L0007: vzeroupper
L000a: vmovsd [rsp+0x48], xmm1
L0010: mov rsi, [rcx]
L0013: mov rdi, [rcx+8]
L0017: mov rcx, 0x7ff953af1178
L0021: call 0x00007ff9b182a470
L0026: mov rbx, rax
L0029: mov rdx, rsi
L002c: mov dword ptr [rbx+8], 1
L0033: lea rsi, [rbx+0x10]
L0037: mov rcx, rsi
L003a: call 0x00007ff9b182a050
L003f: lea rcx, [rsi+8]
L0043: mov rdx, rdi
L0046: call 0x00007ff9b182a050
L004b: mov rcx, 0x7ff953af1320
L0055: call 0x00007ff9b182a470
L005a: mov rsi, rax
L005d: mov dword ptr [rsi+8], 2
L0064: vmovsd xmm1, [rsp+0x48]
L006a: vmovsd [rsi+0x18], xmm1
L006f: lea rcx, [rsi+0x10]
L0073: mov rdx, rbx
L0076: call 0x00007ff9b182a080
L007b: mov rax, rsi
L007e: add rsp, 0x20
L0082: pop rbx
L0083: pop rsi
L0084: pop rdi
L0085: ret

;Test+Test+LinearExpr.NewFloat(Double)
L0000: sub rsp, 0x28
L0004: vzeroupper
L0007: vmovsd [rsp+0x30], xmm0
L000d: mov rcx, 0x7ff953af0ff8
L0017: call 0x00007ff9b182a470
L001c: xor edx, edx
L001e: mov [rax+8], edx
L0021: vmovsd xmm0, [rsp+0x30]
L0027: vmovsd [rax+0x10], xmm0
L002c: add rsp, 0x28
L0030: ret

;Test+Test+LinearExpr.get_IsFloat()
L0000: cmp dword ptr [rcx+8], 0
L0004: sete al
L0007: movzx eax, al
L000a: ret

;Test+Test+LinearExpr.NewDecision(Decision)
L0000: push rdi
L0001: push rsi
L0002: push rbx
L0003: sub rsp, 0x20
L0007: mov rsi, rcx
L000a: mov rcx, 0x7ff953af1178
L0014: call 0x00007ff9b182a470
L0019: mov rdi, rax
L001c: mov rdx, [rsi]
L001f: mov rsi, [rsi+8]
L0023: mov dword ptr [rdi+8], 1
L002a: lea rbx, [rdi+0x10]
L002e: mov rcx, rbx
L0031: call 0x00007ff9b182a050
L0036: lea rcx, [rbx+8]
L003a: mov rdx, rsi
L003d: call 0x00007ff9b182a050
L0042: mov rax, rdi
L0045: add rsp, 0x20
L0049: pop rbx
L004a: pop rsi
L004b: pop rdi
L004c: ret

;Test+Test+LinearExpr.get_IsDecision()
L0000: cmp dword ptr [rcx+8], 1
L0004: sete al
L0007: movzx eax, al
L000a: ret

;Test+Test+LinearExpr.NewScale(Double, LinearExpr)
L0000: push rdi
L0001: push rsi
L0002: sub rsp, 0x28
L0006: vzeroupper
L0009: mov rsi, rdx
L000c: vmovsd [rsp+0x40], xmm0
L0012: mov rcx, 0x7ff953af1320
L001c: call 0x00007ff9b182a470
L0021: mov rdi, rax
L0024: mov dword ptr [rdi+8], 2
L002b: vmovsd xmm0, [rsp+0x40]
L0031: vmovsd [rdi+0x18], xmm0
L0036: lea rcx, [rdi+0x10]
L003a: mov rdx, rsi
L003d: call 0x00007ff9b182a080
L0042: mov rax, rdi
L0045: add rsp, 0x28
L0049: pop rsi
L004a: pop rdi
L004b: ret

;Test+Test+LinearExpr.get_IsScale()
L0000: cmp dword ptr [rcx+8], 2
L0004: sete al
L0007: movzx eax, al
L000a: ret

;Test+Test+LinearExpr.NewAdd(LinearExpr, LinearExpr)
L0000: push rdi
L0001: push rsi
L0002: push rbx
L0003: sub rsp, 0x20
L0007: mov rsi, rcx
L000a: mov rdi, rdx
L000d: mov rcx, 0x7ff953af14c8
L0017: call 0x00007ff9b182a470
L001c: mov rbx, rax
L001f: mov dword ptr [rbx+8], 3
L0026: lea rcx, [rbx+0x10]
L002a: mov rdx, rsi
L002d: call 0x00007ff9b182a080
L0032: lea rcx, [rbx+0x18]
L0036: mov rdx, rdi
L0039: call 0x00007ff9b182a080
L003e: mov rax, rbx
L0041: add rsp, 0x20
L0045: pop rbx
L0046: pop rsi
L0047: pop rdi
L0048: ret

;Test+Test+LinearExpr.get_IsAdd()
L0000: cmp dword ptr [rcx+8], 3
L0004: sete al
L0007: movzx eax, al
L000a: ret

;Test+Test+LinearExpr.get_Tag()
L0000: mov eax, [rcx+8]
L0003: ret

;Test+Test+LinearExpr.ToString()
L0000: push rdi
L0001: push rsi
L0002: sub rsp, 0x28
L0006: mov rsi, rcx
L0009: mov rcx, 0x7ff953af84a0
L0013: call 0x00007ff9b182a470
L0018: mov rdi, rax
L001b: mov rdx, 0x191f2a9b3f0
L0025: mov rdx, [rdx]
L0028: lea rcx, [rdi+8]
L002c: call 0x00007ff9b182a080
L0031: xor edx, edx
L0033: mov [rdi+0x10], rdx
L0037: mov [rdi+0x18], rdx
L003b: mov rdx, rdi
L003e: mov rcx, 0x7ff953af8780
L0048: call Microsoft.FSharp.Core.PrintfModule.PrintFormatToStringThen[[System.__Canon, System.Private.CoreLib]](Microsoft.FSharp.Core.PrintfFormat`4<System.__Canon,Microsoft.FSharp.Core.Unit,System.String,System.String>)
L004d: mov rcx, rax
L0050: mov rdx, rsi
L0053: mov rax, [rax]
L0056: mov rax, [rax+0x40]
L005a: mov rax, [rax+0x20]
L005e: add rsp, 0x28
L0062: pop rsi
L0063: pop rdi
L0064: jmp rax

;Test+Test+LinearExpr.CompareTo(LinearExpr)
L0000: push r14
L0002: push rdi
L0003: push rsi
L0004: push rbp
L0005: push rbx
L0006: sub rsp, 0x30
L000a: vzeroupper
L000d: xor eax, eax
L000f: mov [rsp+0x20], rax
L0014: mov [rsp+0x28], rax
L0019: mov rsi, rcx
L001c: mov rdi, rdx
L001f: test rsi, rsi
L0022: je L02e3
L0028: test rdi, rdi
L002b: je L02d3
L0031: mov ebx, [rsi+8]
L0034: mov eax, ebx
L0036: mov ebp, [rdi+8]
L0039: cmp eax, ebp
L003b: jne L02cf
L0041: cmp ebx, 3
L0044: ja short L005e
L0046: mov ecx, ebx
L0048: lea rdx, [Test+Test+LinearExpr.CompareTo(LinearExpr)]
L004f: mov edx, [rdx+rcx*4]
L0052: lea rax, [L001f]
L0059: add rdx, rax
L005c: jmp rdx
L005e: mov rcx, rsi
L0061: mov rdx, 0x7ff953af0ff8
L006b: cmp [rcx], rdx
L006e: je short L007b
L0070: mov rcx, rdx
L0073: mov rdx, rsi
L0076: call System.Runtime.CompilerServices.CastHelpers.ChkCastClassSpecial(Void*, System.Object)
L007b: mov rax, rdi
L007e: mov rcx, 0x7ff953af0ff8
L0088: cmp [rax], rcx
L008b: je short L0095
L008d: mov rdx, rdi
L0090: call System.Runtime.CompilerServices.CastHelpers.ChkCastClassSpecial(Void*, System.Object)
L0095: mov rcx, 0x191f2a86fa0
L009f: mov r14, [rcx]
L00a2: vmovsd xmm1, [rsi+0x10]
L00a7: vmovsd xmm2, [rax+0x10]
L00ac: vucomisd xmm2, xmm1
L00b0: ja L02e8
L00b6: vucomisd xmm1, xmm2
L00ba: ja L02d3
L00c0: vucomisd xmm1, xmm2
L00c4: jp short L00cc
L00c6: je L02f8
L00cc: mov rcx, r14
L00cf: add rsp, 0x30
L00d3: pop rbx
L00d4: pop rbp
L00d5: pop rsi
L00d6: pop rdi
L00d7: pop r14
L00d9: jmp Microsoft.FSharp.Core.LanguagePrimitives+HashCompare.GenericComparisonWithComparerIntrinsic[[System.Double, System.Private.CoreLib]](System.Collections.IComparer, Double, Double)
L00de: mov rcx, rsi
L00e1: mov rdx, 0x7ff953af1178
L00eb: cmp [rcx], rdx
L00ee: je short L00fb
L00f0: mov rcx, rdx
L00f3: mov rdx, rsi
L00f6: call System.Runtime.CompilerServices.CastHelpers.ChkCastClassSpecial(Void*, System.Object)
L00fb: mov rbx, rdi
L00fe: mov rcx, 0x7ff953af1178
L0108: cmp [rbx], rcx
L010b: je short L0118
L010d: mov rdx, rdi
L0110: call System.Runtime.CompilerServices.CastHelpers.ChkCastClassSpecial(Void*, System.Object)
L0115: mov rbx, rax
L0118: mov rcx, 0x191f2a86fa0
L0122: mov r14, [rcx]
L0125: add rsi, 0x10
L0129: mov rcx, [rsi]
L012c: mov [rsp+0x20], rcx
L0131: mov rcx, [rsi+8]
L0135: mov [rsp+0x28], rcx
L013a: add rbx, 0x10
L013e: mov rdi, [rbx]
L0141: mov rsi, [rbx+8]
L0145: mov rcx, 0x7ff953af0538
L014f: call 0x00007ff9b182a470
L0154: mov rbx, rax
L0157: lea rbp, [rbx+8]
L015b: mov rcx, rbp
L015e: mov rdx, rdi
L0161: call 0x00007ff9b182a050
L0166: lea rcx, [rbp+8]
L016a: mov rdx, rsi
L016d: call 0x00007ff9b182a050
L0172: mov rdx, rbx
L0175: lea rcx, [rsp+0x20]
L017a: mov r8, r14
L017d: call Test+Test+Decision.CompareTo(System.Object, System.Collections.IComparer)
L0182: jmp L0305
L0187: mov rcx, rsi
L018a: mov rdx, 0x7ff953af1320
L0194: cmp [rcx], rdx
L0197: je short L01a4
L0199: mov rcx, rdx
L019c: mov rdx, rsi
L019f: call System.Runtime.CompilerServices.CastHelpers.ChkCastClassSpecial(Void*, System.Object)
L01a4: mov rbx, rdi
L01a7: mov rcx, 0x7ff953af1320
L01b1: cmp [rbx], rcx
L01b4: je short L01c1
L01b6: mov rdx, rdi
L01b9: call System.Runtime.CompilerServices.CastHelpers.ChkCastClassSpecial(Void*, System.Object)
L01be: mov rbx, rax
L01c1: mov rcx, 0x191f2a86fa0
L01cb: mov r14, [rcx]
L01ce: vmovsd xmm1, [rsi+0x18]
L01d3: vmovsd xmm2, [rbx+0x18]
L01d8: vucomisd xmm2, xmm1
L01dc: jbe short L01e5
L01de: mov eax, 0xffffffff
L01e3: jmp short L0206
L01e5: vucomisd xmm1, xmm2
L01e9: jbe short L01f2
L01eb: mov eax, 1
L01f0: jmp short L0206
L01f2: vucomisd xmm1, xmm2
L01f6: jp short L01fe
L01f8: jne short L01fe
L01fa: xor eax, eax
L01fc: jmp short L0206
L01fe: mov rcx, r14
L0201: call Microsoft.FSharp.Core.LanguagePrimitives+HashCompare.GenericComparisonWithComparerIntrinsic[[System.Double, System.Private.CoreLib]](System.Collections.IComparer, Double, Double)
L0206: test eax, eax
L0208: jge short L020f
L020a: jmp L0305
L020f: test eax, eax
L0211: jle short L0218
L0213: jmp L0305
L0218: mov rcx, 0x191f2a86fa0
L0222: mov r14, [rcx]
L0225: mov rcx, [rsi+0x10]
L0229: mov rdx, [rbx+0x10]
L022d: mov r8, r14
L0230: cmp [rcx], ecx
L0232: add rsp, 0x30
L0236: pop rbx
L0237: pop rbp
L0238: pop rsi
L0239: pop rdi
L023a: pop r14
L023c: jmp Test+Test+LinearExpr.CompareTo(System.Object, System.Collections.IComparer)
L0241: mov rcx, rsi
L0244: mov rdx, 0x7ff953af14c8
L024e: cmp [rcx], rdx
L0251: je short L025e
L0253: mov rcx, rdx
L0256: mov rdx, rsi
L0259: call System.Runtime.CompilerServices.CastHelpers.ChkCastClassSpecial(Void*, System.Object)
L025e: mov rbx, rdi
L0261: mov rcx, 0x7ff953af14c8
L026b: cmp [rbx], rcx
L026e: je short L027b
L0270: mov rdx, rdi
L0273: call System.Runtime.CompilerServices.CastHelpers.ChkCastClassSpecial(Void*, System.Object)
L0278: mov rbx, rax
L027b: mov rcx, 0x191f2a86fa0
L0285: mov r14, [rcx]
L0288: mov rcx, [rsi+0x10]
L028c: mov rdx, [rbx+0x10]
L0290: mov r8, r14
L0293: cmp [rcx], ecx
L0295: call Test+Test+LinearExpr.CompareTo(System.Object, System.Collections.IComparer)
L029a: test eax, eax
L029c: jge short L02a0
L029e: jmp short L0305
L02a0: test eax, eax
L02a2: jle short L02a6
L02a4: jmp short L0305
L02a6: mov rcx, 0x191f2a86fa0
L02b0: mov r14, [rcx]
L02b3: mov rcx, [rsi+0x18]
L02b7: mov rdx, [rbx+0x18]
L02bb: mov r8, r14
L02be: cmp [rcx], ecx
L02c0: add rsp, 0x30
L02c4: pop rbx
L02c5: pop rbp
L02c6: pop rsi
L02c7: pop rdi
L02c8: pop r14
L02ca: jmp Test+Test+LinearExpr.CompareTo(System.Object, System.Collections.IComparer)
L02cf: sub eax, ebp
L02d1: jmp short L0305
L02d3: mov eax, 1
L02d8: add rsp, 0x30
L02dc: pop rbx
L02dd: pop rbp
L02de: pop rsi
L02df: pop rdi
L02e0: pop r14
L02e2: ret
L02e3: test rdi, rdi
L02e6: je short L02f8
L02e8: mov eax, 0xffffffff
L02ed: add rsp, 0x30
L02f1: pop rbx
L02f2: pop rbp
L02f3: pop rsi
L02f4: pop rdi
L02f5: pop r14
L02f7: ret
L02f8: xor eax, eax
L02fa: add rsp, 0x30
L02fe: pop rbx
L02ff: pop rbp
L0300: pop rsi
L0301: pop rdi
L0302: pop r14
L0304: ret
L0305: add rsp, 0x30
L0309: pop rbx
L030a: pop rbp
L030b: pop rsi
L030c: pop rdi
L030d: pop r14
L030f: ret

;Test+Test+LinearExpr.CompareTo(System.Object)
L0000: push rsi
L0001: sub rsp, 0x20
L0005: mov rsi, rcx
L0008: mov rax, rdx
L000b: test rax, rax
L000e: je short L0024
L0010: mov rcx, 0x7ff953af0bf0
L001a: cmp [rax], rcx
L001d: je short L0024
L001f: call System.Runtime.CompilerServices.CastHelpers.ChkCastClassSpecial(Void*, System.Object)
L0024: mov rcx, rsi
L0027: mov rdx, rax
L002a: cmp [rcx], ecx
L002c: add rsp, 0x20
L0030: pop rsi
L0031: jmp Test+Test+LinearExpr.CompareTo(LinearExpr)

;Test+Test+LinearExpr.CompareTo(System.Object, System.Collections.IComparer)
L0000: push r14
L0002: push rdi
L0003: push rsi
L0004: push rbp
L0005: push rbx
L0006: sub rsp, 0x30
L000a: vzeroupper
L000d: xor eax, eax
L000f: mov [rsp+0x20], rax
L0014: mov [rsp+0x28], rax
L0019: mov rsi, rdx
L001c: mov rdi, r8
L001f: mov rbx, rcx
L0022: mov rbp, rsi
L0025: test rbp, rbp
L0028: je short L0045
L002a: mov rcx, 0x7ff953af0bf0
L0034: cmp [rbp], rcx
L0038: je short L0045
L003a: mov rdx, rsi
L003d: call System.Runtime.CompilerServices.CastHelpers.ChkCastClassSpecial(Void*, System.Object)
L0042: mov rbp, rax
L0045: test rbx, rbx
L0048: je L02d5
L004e: test rbp, rbp
L0051: je short L006b
L0053: mov rcx, 0x7ff953af0bf0
L005d: cmp [rbp], rcx
L0061: je short L006b
L0063: mov rdx, rbp
L0066: call System.Runtime.CompilerServices.CastHelpers.ChkCastClassSpecial(Void*, System.Object)
L006b: test rbp, rbp
L006e: je L02c5
L0074: mov r14d, [rbx+8]
L0078: mov eax, r14d
L007b: mov esi, [rbp+8]
L007e: cmp eax, esi
L0080: jne L02c1
L0086: cmp r14d, 3
L008a: ja L01ad
L0090: mov ecx, r14d
L0093: lea rdx, [Test+Test+LinearExpr.CompareTo(System.Object, System.Collections.IComparer)]
L009a: mov edx, [rdx+rcx*4]
L009d: lea rax, [L001f]
L00a4: add rdx, rax
L00a7: jmp rdx
L00a9: mov rsi, rbx
L00ac: mov rcx, 0x7ff953af1320
L00b6: cmp [rsi], rcx
L00b9: je short L00c6
L00bb: mov rdx, rbx
L00be: call System.Runtime.CompilerServices.CastHelpers.ChkCastClassSpecial(Void*, System.Object)
L00c3: mov rsi, rax
L00c6: mov rbx, rbp
L00c9: mov rcx, 0x7ff953af1320
L00d3: cmp [rbx], rcx
L00d6: je short L00e3
L00d8: mov rdx, rbp
L00db: call System.Runtime.CompilerServices.CastHelpers.ChkCastClassSpecial(Void*, System.Object)
L00e0: mov rbx, rax
L00e3: vmovsd xmm1, [rsi+0x18]
L00e8: vmovsd xmm2, [rbx+0x18]
L00ed: vucomisd xmm2, xmm1
L00f1: jbe short L00fa
L00f3: mov eax, 0xffffffff
L00f8: jmp short L011b
L00fa: vucomisd xmm1, xmm2
L00fe: jbe short L0107
L0100: mov eax, 1
L0105: jmp short L011b
L0107: vucomisd xmm1, xmm2
L010b: jp short L0113
L010d: jne short L0113
L010f: xor eax, eax
L0111: jmp short L011b
L0113: mov rcx, rdi
L0116: call Microsoft.FSharp.Core.LanguagePrimitives+HashCompare.GenericComparisonWithComparerIntrinsic[[System.Double, System.Private.CoreLib]](System.Collections.IComparer, Double, Double)
L011b: test eax, eax
L011d: jl L02b9
L0123: test eax, eax
L0125: jg L02bb
L012b: mov rcx, [rsi+0x10]
L012f: mov rdx, [rbx+0x10]
L0133: mov rsi, rdx
L0136: mov rbx, rcx
L0139: jmp L0022
L013e: mov rsi, rbx
L0141: mov rcx, 0x7ff953af14c8
L014b: cmp [rsi], rcx
L014e: je short L015b
L0150: mov rdx, rbx
L0153: call System.Runtime.CompilerServices.CastHelpers.ChkCastClassSpecial(Void*, System.Object)
L0158: mov rsi, rax
L015b: mov rbx, rbp
L015e: mov rcx, 0x7ff953af14c8
L0168: cmp [rbx], rcx
L016b: je short L0178
L016d: mov rdx, rbp
L0170: call System.Runtime.CompilerServices.CastHelpers.ChkCastClassSpecial(Void*, System.Object)
L0175: mov rbx, rax
L0178: mov rcx, [rsi+0x10]
L017c: mov rdx, [rbx+0x10]
L0180: mov r8, rdi
L0183: cmp [rcx], ecx
L0185: call Test+Test+LinearExpr.CompareTo(System.Object, System.Collections.IComparer)
L018a: test eax, eax
L018c: jl L02bd
L0192: test eax, eax
L0194: jg L02bf
L019a: mov rcx, [rsi+0x18]
L019e: mov rdx, [rbx+0x18]
L01a2: mov rsi, rdx
L01a5: mov rbx, rcx
L01a8: jmp L0022
L01ad: mov rsi, rbx
L01b0: mov rcx, 0x7ff953af0ff8
L01ba: cmp [rsi], rcx
L01bd: je short L01ca
L01bf: mov rdx, rbx
L01c2: call System.Runtime.CompilerServices.CastHelpers.ChkCastClassSpecial(Void*, System.Object)
L01c7: mov rsi, rax
L01ca: mov rax, rbp
L01cd: mov rcx, 0x7ff953af0ff8
L01d7: cmp [rax], rcx
L01da: je short L01e4
L01dc: mov rdx, rbp
L01df: call System.Runtime.CompilerServices.CastHelpers.ChkCastClassSpecial(Void*, System.Object)
L01e4: vmovsd xmm1, [rsi+0x10]
L01e9: vmovsd xmm2, [rax+0x10]
L01ee: vucomisd xmm2, xmm1
L01f2: ja L02f9
L01f8: vucomisd xmm1, xmm2
L01fc: ja L02c5
L0202: vucomisd xmm1, xmm2
L0206: jp short L020e
L0208: je L0309
L020e: mov rcx, rdi
L0211: add rsp, 0x30
L0215: pop rbx
L0216: pop rbp
L0217: pop rsi
L0218: pop rdi
L0219: pop r14
L021b: jmp Microsoft.FSharp.Core.LanguagePrimitives+HashCompare.GenericComparisonWithComparerIntrinsic[[System.Double, System.Private.CoreLib]](System.Collections.IComparer, Double, Double)
L0220: mov rsi, rbx
L0223: mov rcx, 0x7ff953af1178
L022d: cmp [rsi], rcx
L0230: je short L023d
L0232: mov rdx, rbx
L0235: call System.Runtime.CompilerServices.CastHelpers.ChkCastClassSpecial(Void*, System.Object)
L023a: mov rsi, rax
L023d: mov rbx, rbp
L0240: mov rcx, 0x7ff953af1178
L024a: cmp [rbx], rcx
L024d: je short L025a
L024f: mov rdx, rbp
L0252: call System.Runtime.CompilerServices.CastHelpers.ChkCastClassSpecial(Void*, System.Object)
L0257: mov rbx, rax
L025a: add rsi, 0x10
L025e: mov rcx, [rsi]
L0261: mov [rsp+0x20], rcx
L0266: mov rcx, [rsi+8]
L026a: mov [rsp+0x28], rcx
L026f: add rbx, 0x10
L0273: mov rsi, [rbx]
L0276: mov rbx, [rbx+8]
L027a: mov rcx, 0x7ff953af0538
L0284: call 0x00007ff9b182a470
L0289: mov rbp, rax
L028c: lea r14, [rbp+8]
L0290: mov rcx, r14
L0293: mov rdx, rsi
L0296: call 0x00007ff9b182a050
L029b: lea rcx, [r14+8]
L029f: mov rdx, rbx
L02a2: call 0x00007ff9b182a050
L02a7: mov rdx, rbp
L02aa: lea rcx, [rsp+0x20]
L02af: mov r8, rdi
L02b2: call Test+Test+Decision.CompareTo(System.Object, System.Collections.IComparer)
L02b7: jmp short L0316
L02b9: jmp short L0316
L02bb: jmp short L0316
L02bd: jmp short L0316
L02bf: jmp short L0316
L02c1: sub eax, esi
L02c3: jmp short L0316
L02c5: mov eax, 1
L02ca: add rsp, 0x30
L02ce: pop rbx
L02cf: pop rbp
L02d0: pop rsi
L02d1: pop rdi
L02d2: pop r14
L02d4: ret
L02d5: mov rax, rsi
L02d8: test rax, rax
L02db: je short L02f4
L02dd: mov rcx, 0x7ff953af0bf0
L02e7: cmp [rax], rcx
L02ea: je short L02f4
L02ec: mov rdx, rsi
L02ef: call System.Runtime.CompilerServices.CastHelpers.ChkCastClassSpecial(Void*, System.Object)
L02f4: test rax, rax
L02f7: je short L0309
L02f9: mov eax, 0xffffffff
L02fe: add rsp, 0x30
L0302: pop rbx
L0303: pop rbp
L0304: pop rsi
L0305: pop rdi
L0306: pop r14
L0308: ret
L0309: xor eax, eax
L030b: add rsp, 0x30
L030f: pop rbx
L0310: pop rbp
L0311: pop rsi
L0312: pop rdi
L0313: pop r14
L0315: ret
L0316: add rsp, 0x30
L031a: pop rbx
L031b: pop rbp
L031c: pop rsi
L031d: pop rdi
L031e: pop r14
L0320: ret

;Test+Test+LinearExpr.GetHashCode(System.Collections.IEqualityComparer)
L0000: push rdi
L0001: push rsi
L0002: push rbp
L0003: push rbx
L0004: sub rsp, 0x38
L0008: vzeroupper
L000b: xor eax, eax
L000d: mov [rsp+0x28], rax
L0012: mov [rsp+0x30], rax
L0017: mov rsi, rcx
L001a: mov rdi, rdx
L001d: test rsi, rsi
L0020: je L017b
L0026: mov ebx, [rsi+8]
L0029: cmp ebx, 3
L002c: ja short L0046
L002e: mov ecx, ebx
L0030: lea rdx, [Test+Test+LinearExpr.GetHashCode(System.Collections.IEqualityComparer)]
L0037: mov edx, [rdx+rcx*4]
L003a: lea rax, [L001d]
L0041: add rdx, rax
L0044: jmp rdx
L0046: mov rcx, rsi
L0049: mov rdx, 0x7ff953af0ff8
L0053: cmp [rcx], rdx
L0056: je short L0063
L0058: mov rcx, rdx
L005b: mov rdx, rsi
L005e: call System.Runtime.CompilerServices.CastHelpers.ChkCastClassSpecial(Void*, System.Object)
L0063: vmovsd xmm1, [rsi+0x10]
L0068: mov rcx, rdi
L006b: call Microsoft.FSharp.Core.LanguagePrimitives+HashCompare.GenericHashWithComparerIntrinsic[[System.Double, System.Private.CoreLib]](System.Collections.IEqualityComparer, Double)
L0070: lea ebp, [rax-0x61c88647]
L0076: mov eax, ebp
L0078: jmp L0172
L007d: mov rcx, rsi
L0080: mov rdx, 0x7ff953af1178
L008a: cmp [rcx], rdx
L008d: je short L009a
L008f: mov rcx, rdx
L0092: mov rdx, rsi
L0095: call System.Runtime.CompilerServices.CastHelpers.ChkCastClassSpecial(Void*, System.Object)
L009a: add rsi, 0x10
L009e: mov rcx, [rsi]
L00a1: mov [rsp+0x28], rcx
L00a6: mov rcx, [rsi+8]
L00aa: mov [rsp+0x30], rcx
L00af: lea rcx, [rsp+0x28]
L00b4: mov rdx, rdi
L00b7: call Test+Test+Decision.GetHashCode(System.Collections.IEqualityComparer)
L00bc: lea ebp, [rax-0x61c88607]
L00c2: mov eax, ebp
L00c4: jmp L0172
L00c9: mov rcx, rsi
L00cc: mov rdx, 0x7ff953af1320
L00d6: cmp [rcx], rdx
L00d9: je short L00e6
L00db: mov rcx, rdx
L00de: mov rdx, rsi
L00e1: call System.Runtime.CompilerServices.CastHelpers.ChkCastClassSpecial(Void*, System.Object)
L00e6: mov rcx, [rsi+0x10]
L00ea: mov rdx, rdi
L00ed: cmp [rcx], ecx
L00ef: call Test+Test+LinearExpr.GetHashCode(System.Collections.IEqualityComparer)
L00f4: lea ebp, [rax-0x61c885c7]
L00fa: vmovsd xmm1, [rsi+0x18]
L00ff: mov rcx, rdi
L0102: call Microsoft.FSharp.Core.LanguagePrimitives+HashCompare.GenericHashWithComparerIntrinsic[[System.Double, System.Private.CoreLib]](System.Collections.IEqualityComparer, Double)
L0107: mov ecx, ebp
L0109: shl ecx, 6
L010c: add ecx, eax
L010e: mov edx, ebp
L0110: sar edx, 2
L0113: lea ebp, [rcx+rdx-0x61c88647]
L011a: mov eax, ebp
L011c: jmp short L0172
L011e: mov rcx, rsi
L0121: mov rdx, 0x7ff953af14c8
L012b: cmp [rcx], rdx
L012e: je short L013b
L0130: mov rcx, rdx
L0133: mov rdx, rsi
L0136: call System.Runtime.CompilerServices.CastHelpers.ChkCastClassSpecial(Void*, System.Object)
L013b: mov rcx, [rsi+0x18]
L013f: mov rdx, rdi
L0142: cmp [rcx], ecx
L0144: call Test+Test+LinearExpr.GetHashCode(System.Collections.IEqualityComparer)
L0149: lea ebp, [rax-0x61c88587]
L014f: mov rcx, [rsi+0x10]
L0153: mov rdx, rdi
L0156: cmp [rcx], ecx
L0158: call Test+Test+LinearExpr.GetHashCode(System.Collections.IEqualityComparer)
L015d: mov edx, ebp
L015f: shl edx, 6
L0162: add eax, edx
L0164: mov edx, ebp
L0166: sar edx, 2
L0169: lea ebp, [rax+rdx-0x61c88647]
L0170: mov eax, ebp
L0172: add rsp, 0x38
L0176: pop rbx
L0177: pop rbp
L0178: pop rsi
L0179: pop rdi
L017a: ret
L017b: xor eax, eax
L017d: add rsp, 0x38
L0181: pop rbx
L0182: pop rbp
L0183: pop rsi
L0184: pop rdi
L0185: ret

;Test+Test+LinearExpr.GetHashCode()
L0000: mov rdx, 0x191f2a86fa8
L000a: mov rdx, [rdx]
L000d: cmp [rcx], ecx
L000f: jmp Test+Test+LinearExpr.GetHashCode(System.Collections.IEqualityComparer)

;Test+Test+LinearExpr.Equals(System.Object, System.Collections.IEqualityComparer)
L0000: push r14
L0002: push rdi
L0003: push rsi
L0004: push rbp
L0005: push rbx
L0006: sub rsp, 0x30
L000a: vzeroupper
L000d: xor eax, eax
L000f: mov [rsp+0x20], rax
L0014: mov [rsp+0x28], rax
L0019: mov rsi, r8
L001c: mov rdi, rcx
L001f: test rdi, rdi
L0022: je L023a
L0028: mov rcx, 0x7ff953af0bf0
L0032: call System.Runtime.CompilerServices.CastHelpers.IsInstanceOfClass(Void*, System.Object)
L0037: mov rbx, rax
L003a: test rbx, rbx
L003d: je L022d
L0043: mov ebp, [rdi+8]
L0046: mov ecx, ebp
L0048: mov edx, [rbx+8]
L004b: cmp ecx, edx
L004d: jne L022d
L0053: cmp ebp, 3
L0056: ja L013a
L005c: mov ecx, ebp
L005e: lea rdx, [Test+Test+LinearExpr.Equals(System.Object, System.Collections.IEqualityComparer)]
L0065: mov edx, [rdx+rcx*4]
L0068: lea rax, [L001c]
L006f: add rdx, rax
L0072: jmp rdx
L0074: mov r14, rdi
L0077: mov rcx, 0x7ff953af1320
L0081: cmp [r14], rcx
L0084: je short L0091
L0086: mov rdx, rdi
L0089: call System.Runtime.CompilerServices.CastHelpers.ChkCastClassSpecial(Void*, System.Object)
L008e: mov r14, rax
L0091: mov rdi, rbx
L0094: mov rcx, 0x7ff953af1320
L009e: cmp [rdi], rcx
L00a1: je short L00ae
L00a3: mov rdx, rbx
L00a6: call System.Runtime.CompilerServices.CastHelpers.ChkCastClassSpecial(Void*, System.Object)
L00ab: mov rdi, rax
L00ae: vmovsd xmm0, [r14+0x18]
L00b4: vucomisd xmm0, [rdi+0x18]
L00b9: jp L022d
L00bf: jne L022d
L00c5: mov rcx, [r14+0x10]
L00c9: mov rdx, [rdi+0x10]
L00cd: mov rdi, rcx
L00d0: jmp L001f
L00d5: mov rbp, rdi
L00d8: mov rcx, 0x7ff953af14c8
L00e2: cmp [rbp], rcx
L00e6: je short L00f3
L00e8: mov rdx, rdi
L00eb: call System.Runtime.CompilerServices.CastHelpers.ChkCastClassSpecial(Void*, System.Object)
L00f0: mov rbp, rax
L00f3: mov rdi, rbx
L00f6: mov rcx, 0x7ff953af14c8
L0100: cmp [rdi], rcx
L0103: je short L0110
L0105: mov rdx, rbx
L0108: call System.Runtime.CompilerServices.CastHelpers.ChkCastClassSpecial(Void*, System.Object)
L010d: mov rdi, rax
L0110: mov rcx, [rbp+0x10]
L0114: mov rdx, [rdi+0x10]
L0118: mov r8, rsi
L011b: cmp [rcx], ecx
L011d: call Test+Test+LinearExpr.Equals(System.Object, System.Collections.IEqualityComparer)
L0122: test eax, eax
L0124: je L022d
L012a: mov rcx, [rbp+0x18]
L012e: mov rdx, [rdi+0x18]
L0132: mov rdi, rcx
L0135: jmp L001f
L013a: mov rbp, rdi
L013d: mov rcx, 0x7ff953af0ff8
L0147: cmp [rbp], rcx
L014b: je short L0158
L014d: mov rdx, rdi
L0150: call System.Runtime.CompilerServices.CastHelpers.ChkCastClassSpecial(Void*, System.Object)
L0155: mov rbp, rax
L0158: mov rax, rbx
L015b: mov rcx, 0x7ff953af0ff8
L0165: cmp [rax], rcx
L0168: je short L0172
L016a: mov rdx, rbx
L016d: call System.Runtime.CompilerServices.CastHelpers.ChkCastClassSpecial(Void*, System.Object)
L0172: vmovsd xmm0, [rbp+0x10]
L0177: vucomisd xmm0, [rax+0x10]
L017c: setnp r14b
L0180: jp short L0186
L0182: sete r14b
L0186: movzx r14d, r14b
L018a: jmp L0245
L018f: mov rbp, rdi
L0192: mov rcx, 0x7ff953af1178
L019c: cmp [rbp], rcx
L01a0: je short L01ad
L01a2: mov rdx, rdi
L01a5: call System.Runtime.CompilerServices.CastHelpers.ChkCastClassSpecial(Void*, System.Object)
L01aa: mov rbp, rax
L01ad: mov rdi, rbx
L01b0: mov rcx, 0x7ff953af1178
L01ba: cmp [rdi], rcx
L01bd: je short L01ca
L01bf: mov rdx, rbx
L01c2: call System.Runtime.CompilerServices.CastHelpers.ChkCastClassSpecial(Void*, System.Object)
L01c7: mov rdi, rax
L01ca: add rbp, 0x10
L01ce: mov rcx, [rbp]
L01d2: mov [rsp+0x20], rcx
L01d7: mov rcx, [rbp+8]
L01db: mov [rsp+0x28], rcx
L01e0: add rdi, 0x10
L01e4: mov rbx, [rdi]
L01e7: mov rdi, [rdi+8]
L01eb: mov rcx, 0x7ff953af0538
L01f5: call 0x00007ff9b182a470
L01fa: mov rbp, rax
L01fd: lea r14, [rbp+8]
L0201: mov rcx, r14
L0204: mov rdx, rbx
L0207: call 0x00007ff9b182a050
L020c: lea rcx, [r14+8]
L0210: mov rdx, rdi
L0213: call 0x00007ff9b182a050
L0218: mov rdx, rbp
L021b: lea rcx, [rsp+0x20]
L0220: mov r8, rsi
L0223: call Test+Test+Decision.Equals(System.Object, System.Collections.IEqualityComparer)
L0228: mov r14d, eax
L022b: jmp short L0245
L022d: xor eax, eax
L022f: add rsp, 0x30
L0233: pop rbx
L0234: pop rbp
L0235: pop rsi
L0236: pop rdi
L0237: pop r14
L0239: ret
L023a: test rdx, rdx
L023d: sete r14b
L0241: movzx r14d, r14b
L0245: movzx eax, r14b
L0249: add rsp, 0x30
L024d: pop rbx
L024e: pop rbp
L024f: pop rsi
L0250: pop rdi
L0251: pop r14
L0253: ret

;Test+Test+LinearExpr.op_Addition(Double, LinearExpr)
L0000: push rdi
L0001: push rsi
L0002: push rbx
L0003: sub rsp, 0x20
L0007: vzeroupper
L000a: mov rsi, rdx
L000d: vmovsd [rsp+0x40], xmm0
L0013: mov rcx, 0x7ff953af0ff8
L001d: call 0x00007ff9b182a470
L0022: mov rdi, rax
L0025: xor ecx, ecx
L0027: mov [rdi+8], ecx
L002a: vmovsd xmm0, [rsp+0x40]
L0030: vmovsd [rdi+0x10], xmm0
L0035: mov rcx, 0x7ff953af14c8
L003f: call 0x00007ff9b182a470
L0044: mov rbx, rax
L0047: mov dword ptr [rbx+8], 3
L004e: lea rcx, [rbx+0x10]
L0052: mov rdx, rdi
L0055: call 0x00007ff9b182a080
L005a: lea rcx, [rbx+0x18]
L005e: mov rdx, rsi
L0061: call 0x00007ff9b182a080
L0066: mov rax, rbx
L0069: add rsp, 0x20
L006d: pop rbx
L006e: pop rsi
L006f: pop rdi
L0070: ret

;Test+Test+LinearExpr.op_Addition(Decision, LinearExpr)
L0000: push rdi
L0001: push rsi
L0002: push rbp
L0003: push rbx
L0004: sub rsp, 0x28
L0008: mov rsi, rdx
L000b: mov rdi, [rcx]
L000e: mov rbx, [rcx+8]
L0012: mov rcx, 0x7ff953af1178
L001c: call 0x00007ff9b182a470
L0021: mov rbp, rax
L0024: mov rdx, rdi
L0027: mov dword ptr [rbp+8], 1
L002e: lea rdi, [rbp+0x10]
L0032: mov rcx, rdi
L0035: call 0x00007ff9b182a050
L003a: lea rcx, [rdi+8]
L003e: mov rdx, rbx
L0041: call 0x00007ff9b182a050
L0046: mov rcx, 0x7ff953af14c8
L0050: call 0x00007ff9b182a470
L0055: mov rdi, rax
L0058: mov dword ptr [rdi+8], 3
L005f: lea rcx, [rdi+0x10]
L0063: mov rdx, rbp
L0066: call 0x00007ff9b182a080
L006b: lea rcx, [rdi+0x18]
L006f: mov rdx, rsi
L0072: call 0x00007ff9b182a080
L0077: mov rax, rdi
L007a: add rsp, 0x28
L007e: pop rbx
L007f: pop rbp
L0080: pop rsi
L0081: pop rdi
L0082: ret

;Test+Test+LinearExpr.op_Addition(LinearExpr, LinearExpr)
L0000: push rdi
L0001: push rsi
L0002: push rbx
L0003: sub rsp, 0x20
L0007: mov rsi, rcx
L000a: mov rdi, rdx
L000d: mov rcx, 0x7ff953af14c8
L0017: call 0x00007ff9b182a470
L001c: mov rbx, rax
L001f: mov dword ptr [rbx+8], 3
L0026: lea rcx, [rbx+0x10]
L002a: mov rdx, rsi
L002d: call 0x00007ff9b182a080
L0032: lea rcx, [rbx+0x18]
L0036: mov rdx, rdi
L0039: call 0x00007ff9b182a080
L003e: mov rax, rbx
L0041: add rsp, 0x20
L0045: pop rbx
L0046: pop rsi
L0047: pop rdi
L0048: ret

;Test+Test+LinearExpr.op_Multiply(Double, LinearExpr)
L0000: push rdi
L0001: push rsi
L0002: sub rsp, 0x28
L0006: vzeroupper
L0009: mov rsi, rdx
L000c: vmovsd [rsp+0x40], xmm0
L0012: mov rcx, 0x7ff953af1320
L001c: call 0x00007ff9b182a470
L0021: mov rdi, rax
L0024: mov dword ptr [rdi+8], 2
L002b: vmovsd xmm0, [rsp+0x40]
L0031: vmovsd [rdi+0x18], xmm0
L0036: lea rcx, [rdi+0x10]
L003a: mov rdx, rsi
L003d: call 0x00007ff9b182a080
L0042: mov rax, rdi
L0045: add rsp, 0x28
L0049: pop rsi
L004a: pop rdi
L004b: ret

;Test+Test+LinearExpr.op_Multiply(LinearExpr, Double)
L0000: push rdi
L0001: push rsi
L0002: sub rsp, 0x28
L0006: vzeroupper
L0009: mov rsi, rcx
L000c: vmovsd [rsp+0x48], xmm1
L0012: mov rcx, 0x7ff953af1320
L001c: call 0x00007ff9b182a470
L0021: mov rdi, rax
L0024: mov dword ptr [rdi+8], 2
L002b: vmovsd xmm1, [rsp+0x48]
L0031: vmovsd [rdi+0x18], xmm1
L0036: lea rcx, [rdi+0x10]
L003a: mov rdx, rsi
L003d: call 0x00007ff9b182a080
L0042: mov rax, rdi
L0045: add rsp, 0x28
L0049: pop rsi
L004a: pop rdi
L004b: ret

;Test+Test+LinearExpr.get_Zero()
L0000: sub rsp, 0x28
L0004: vzeroupper
L0007: mov rcx, 0x7ff953af0ff8
L0011: call 0x00007ff9b182a470
L0016: xor edx, edx
L0018: mov [rax+8], edx
L001b: vxorps xmm0, xmm0, xmm0
L001f: vmovsd [rax+0x10], xmm0
L0024: add rsp, 0x28
L0028: ret

;Test+Test+LinearExpr.Equals(LinearExpr)
L0000: push rdi
L0001: push rsi
L0002: push rbx
L0003: sub rsp, 0x40
L0007: vzeroupper
L000a: vmovaps [rsp+0x30], xmm6
L0010: vmovaps [rsp+0x20], xmm7
L0016: mov rsi, rdx
L0019: mov rdx, rcx
L001c: test rdx, rdx
L001f: je L022c
L0025: test rsi, rsi
L0028: je L0216
L002e: mov edi, [rdx+8]
L0031: mov ecx, edi
L0033: mov eax, [rsi+8]
L0036: cmp ecx, eax
L0038: jne L0216
L003e: cmp edi, 3
L0041: ja L012e
L0047: mov ecx, edi
L0049: lea rax, [Test+Test+LinearExpr.Equals(LinearExpr)]
L0050: mov eax, [rax+rcx*4]
L0053: lea r8, [L0019]
L005a: add rax, r8
L005d: jmp rax
L005f: mov rbx, rdx
L0062: mov rcx, 0x7ff953af1320
L006c: cmp [rbx], rcx
L006f: je short L0079
L0071: call System.Runtime.CompilerServices.CastHelpers.ChkCastClassSpecial(Void*, System.Object)
L0076: mov rbx, rax
L0079: mov rdi, rsi
L007c: mov rcx, 0x7ff953af1320
L0086: cmp [rdi], rcx
L0089: je short L0096
L008b: mov rdx, rsi
L008e: call System.Runtime.CompilerServices.CastHelpers.ChkCastClassSpecial(Void*, System.Object)
L0093: mov rdi, rax
L0096: vmovsd xmm6, [rbx+0x18]
L009b: vmovsd xmm7, [rdi+0x18]
L00a0: vucomisd xmm6, xmm7
L00a4: jp short L00a8
L00a6: je short L00c6
L00a8: vucomisd xmm6, xmm6
L00ac: jp short L00b4
L00ae: je L0216
L00b4: vucomisd xmm7, xmm7
L00b8: setp cl
L00bb: movzx ecx, cl
L00be: test ecx, ecx
L00c0: je L0216
L00c6: mov rdx, [rbx+0x10]
L00ca: mov rsi, [rdi+0x10]
L00ce: jmp L001c
L00d3: mov rdi, rdx
L00d6: mov rcx, 0x7ff953af14c8
L00e0: cmp [rdi], rcx
L00e3: je short L00ed
L00e5: call System.Runtime.CompilerServices.CastHelpers.ChkCastClassSpecial(Void*, System.Object)
L00ea: mov rdi, rax
L00ed: mov rbx, rsi
L00f0: mov rcx, 0x7ff953af14c8
L00fa: cmp [rbx], rcx
L00fd: je short L010a
L00ff: mov rdx, rsi
L0102: call System.Runtime.CompilerServices.CastHelpers.ChkCastClassSpecial(Void*, System.Object)
L0107: mov rbx, rax
L010a: mov rcx, [rdi+0x10]
L010e: mov rdx, [rbx+0x10]
L0112: cmp [rcx], ecx
L0114: call Test+Test+LinearExpr.Equals(LinearExpr)
L0119: test eax, eax
L011b: je L0216
L0121: mov rdx, [rdi+0x18]
L0125: mov rsi, [rbx+0x18]
L0129: jmp L001c
L012e: mov rdi, rdx
L0131: mov rcx, 0x7ff953af0ff8
L013b: cmp [rdi], rcx
L013e: je short L0148
L0140: call System.Runtime.CompilerServices.CastHelpers.ChkCastClassSpecial(Void*, System.Object)
L0145: mov rdi, rax
L0148: mov rax, rsi
L014b: mov rcx, 0x7ff953af0ff8
L0155: cmp [rax], rcx
L0158: je short L0162
L015a: mov rdx, rsi
L015d: call System.Runtime.CompilerServices.CastHelpers.ChkCastClassSpecial(Void*, System.Object)
L0162: vmovsd xmm6, [rdi+0x10]
L0167: vmovsd xmm7, [rax+0x10]
L016c: vucomisd xmm6, xmm7
L0170: jp short L018d
L0172: jne short L018d
L0174: mov eax, 1
L0179: vmovaps xmm6, [rsp+0x30]
L017f: vmovaps xmm7, [rsp+0x20]
L0185: add rsp, 0x40
L0189: pop rbx
L018a: pop rsi
L018b: pop rdi
L018c: ret
L018d: vucomisd xmm6, xmm6
L0191: jp short L0199
L0193: je L0216
L0199: vucomisd xmm7, xmm7
L019d: setp bl
L01a0: movzx ebx, bl
L01a3: jmp L0235
L01a8: mov rdi, rdx
L01ab: mov rcx, 0x7ff953af1178
L01b5: cmp [rdi], rcx
L01b8: je short L01c2
L01ba: call System.Runtime.CompilerServices.CastHelpers.ChkCastClassSpecial(Void*, System.Object)
L01bf: mov rdi, rax
L01c2: mov rbx, rsi
L01c5: mov rcx, 0x7ff953af1178
L01cf: cmp [rbx], rcx
L01d2: je short L01df
L01d4: mov rdx, rsi
L01d7: call System.Runtime.CompilerServices.CastHelpers.ChkCastClassSpecial(Void*, System.Object)
L01dc: mov rbx, rax
L01df: add rdi, 0x10
L01e3: mov rcx, [rdi]
L01e6: mov rsi, [rdi+8]
L01ea: add rbx, 0x10
L01ee: mov rdx, [rbx]
L01f1: mov rdi, [rbx+8]
L01f5: cmp [rcx], ecx
L01f7: call Test+Test+DecisionName.Equals(DecisionName)
L01fc: test eax, eax
L01fe: je short L0212
L0200: mov rcx, rsi
L0203: mov rdx, rdi
L0206: cmp [rcx], ecx
L0208: call Test+Test+DecisionType.Equals(DecisionType)
L020d: movzx ebx, al
L0210: jmp short L0214
L0212: xor ebx, ebx
L0214: jmp short L0235
L0216: xor eax, eax
L0218: vmovaps xmm6, [rsp+0x30]
L021e: vmovaps xmm7, [rsp+0x20]
L0224: add rsp, 0x40
L0228: pop rbx
L0229: pop rsi
L022a: pop rdi
L022b: ret
L022c: test rsi, rsi
L022f: sete bl
L0232: movzx ebx, bl
L0235: movzx eax, bl
L0238: vmovaps xmm6, [rsp+0x30]
L023e: vmovaps xmm7, [rsp+0x20]
L0244: add rsp, 0x40
L0248: pop rbx
L0249: pop rsi
L024a: pop rdi
L024b: ret

;Test+Test+LinearExpr.Equals(System.Object)
L0000: push rsi
L0001: sub rsp, 0x20
L0005: mov rsi, rcx
L0008: mov rcx, 0x7ff953af0bf0
L0012: call System.Runtime.CompilerServices.CastHelpers.IsInstanceOfClass(Void*, System.Object)
L0017: test rax, rax
L001a: je short L002e
L001c: mov rcx, rsi
L001f: mov rdx, rax
L0022: cmp [rcx], ecx
L0024: add rsp, 0x20
L0028: pop rsi
L0029: jmp Test+Test+LinearExpr.Equals(LinearExpr)
L002e: xor eax, eax
L0030: add rsp, 0x20
L0034: pop rsi
L0035: ret

;Test+Test+LinearExpr+Float.get_Item()
L0000: vzeroupper
L0003: vmovsd xmm0, [rcx+0x10]
L0008: ret

;Test+Test+LinearExpr.get_IsFloat()
L0000: cmp dword ptr [rcx+8], 0
L0004: sete al
L0007: movzx eax, al
L000a: ret

;Test+Test+LinearExpr.get_IsDecision()
L0000: cmp dword ptr [rcx+8], 1
L0004: sete al
L0007: movzx eax, al
L000a: ret

;Test+Test+LinearExpr.get_IsScale()
L0000: cmp dword ptr [rcx+8], 2
L0004: sete al
L0007: movzx eax, al
L000a: ret

;Test+Test+LinearExpr.get_IsAdd()
L0000: cmp dword ptr [rcx+8], 3
L0004: sete al
L0007: movzx eax, al
L000a: ret

;Test+Test+LinearExpr.get_Tag()
L0000: mov eax, [rcx+8]
L0003: ret

;Test+Test+LinearExpr.ToString()
L0000: push rdi
L0001: push rsi
L0002: sub rsp, 0x28
L0006: mov rsi, rcx
L0009: mov rcx, 0x7ff953af84a0
L0013: call 0x00007ff9b182a470
L0018: mov rdi, rax
L001b: mov rdx, 0x191f2a9b3f0
L0025: mov rdx, [rdx]
L0028: lea rcx, [rdi+8]
L002c: call 0x00007ff9b182a080
L0031: xor edx, edx
L0033: mov [rdi+0x10], rdx
L0037: mov [rdi+0x18], rdx
L003b: mov rdx, rdi
L003e: mov rcx, 0x7ff953af8780
L0048: call Microsoft.FSharp.Core.PrintfModule.PrintFormatToStringThen[[System.__Canon, System.Private.CoreLib]](Microsoft.FSharp.Core.PrintfFormat`4<System.__Canon,Microsoft.FSharp.Core.Unit,System.String,System.String>)
L004d: mov rcx, rax
L0050: mov rdx, rsi
L0053: mov rax, [rax]
L0056: mov rax, [rax+0x40]
L005a: mov rax, [rax+0x20]
L005e: add rsp, 0x28
L0062: pop rsi
L0063: pop rdi
L0064: jmp rax

;Test+Test+LinearExpr.CompareTo(LinearExpr)
L0000: push r14
L0002: push rdi
L0003: push rsi
L0004: push rbp
L0005: push rbx
L0006: sub rsp, 0x30
L000a: vzeroupper
L000d: xor eax, eax
L000f: mov [rsp+0x20], rax
L0014: mov [rsp+0x28], rax
L0019: mov rsi, rcx
L001c: mov rdi, rdx
L001f: test rsi, rsi
L0022: je L02e3
L0028: test rdi, rdi
L002b: je L02d3
L0031: mov ebx, [rsi+8]
L0034: mov eax, ebx
L0036: mov ebp, [rdi+8]
L0039: cmp eax, ebp
L003b: jne L02cf
L0041: cmp ebx, 3
L0044: ja short L005e
L0046: mov ecx, ebx
L0048: lea rdx, [Test+Test+LinearExpr.CompareTo(LinearExpr)]
L004f: mov edx, [rdx+rcx*4]
L0052: lea rax, [L001f]
L0059: add rdx, rax
L005c: jmp rdx
L005e: mov rcx, rsi
L0061: mov rdx, 0x7ff953af0ff8
L006b: cmp [rcx], rdx
L006e: je short L007b
L0070: mov rcx, rdx
L0073: mov rdx, rsi
L0076: call System.Runtime.CompilerServices.CastHelpers.ChkCastClassSpecial(Void*, System.Object)
L007b: mov rax, rdi
L007e: mov rcx, 0x7ff953af0ff8
L0088: cmp [rax], rcx
L008b: je short L0095
L008d: mov rdx, rdi
L0090: call System.Runtime.CompilerServices.CastHelpers.ChkCastClassSpecial(Void*, System.Object)
L0095: mov rcx, 0x191f2a86fa0
L009f: mov r14, [rcx]
L00a2: vmovsd xmm1, [rsi+0x10]
L00a7: vmovsd xmm2, [rax+0x10]
L00ac: vucomisd xmm2, xmm1
L00b0: ja L02e8
L00b6: vucomisd xmm1, xmm2
L00ba: ja L02d3
L00c0: vucomisd xmm1, xmm2
L00c4: jp short L00cc
L00c6: je L02f8
L00cc: mov rcx, r14
L00cf: add rsp, 0x30
L00d3: pop rbx
L00d4: pop rbp
L00d5: pop rsi
L00d6: pop rdi
L00d7: pop r14
L00d9: jmp Microsoft.FSharp.Core.LanguagePrimitives+HashCompare.GenericComparisonWithComparerIntrinsic[[System.Double, System.Private.CoreLib]](System.Collections.IComparer, Double, Double)
L00de: mov rcx, rsi
L00e1: mov rdx, 0x7ff953af1178
L00eb: cmp [rcx], rdx
L00ee: je short L00fb
L00f0: mov rcx, rdx
L00f3: mov rdx, rsi
L00f6: call System.Runtime.CompilerServices.CastHelpers.ChkCastClassSpecial(Void*, System.Object)
L00fb: mov rbx, rdi
L00fe: mov rcx, 0x7ff953af1178
L0108: cmp [rbx], rcx
L010b: je short L0118
L010d: mov rdx, rdi
L0110: call System.Runtime.CompilerServices.CastHelpers.ChkCastClassSpecial(Void*, System.Object)
L0115: mov rbx, rax
L0118: mov rcx, 0x191f2a86fa0
L0122: mov r14, [rcx]
L0125: add rsi, 0x10
L0129: mov rcx, [rsi]
L012c: mov [rsp+0x20], rcx
L0131: mov rcx, [rsi+8]
L0135: mov [rsp+0x28], rcx
L013a: add rbx, 0x10
L013e: mov rdi, [rbx]
L0141: mov rsi, [rbx+8]
L0145: mov rcx, 0x7ff953af0538
L014f: call 0x00007ff9b182a470
L0154: mov rbx, rax
L0157: lea rbp, [rbx+8]
L015b: mov rcx, rbp
L015e: mov rdx, rdi
L0161: call 0x00007ff9b182a050
L0166: lea rcx, [rbp+8]
L016a: mov rdx, rsi
L016d: call 0x00007ff9b182a050
L0172: mov rdx, rbx
L0175: lea rcx, [rsp+0x20]
L017a: mov r8, r14
L017d: call Test+Test+Decision.CompareTo(System.Object, System.Collections.IComparer)
L0182: jmp L0305
L0187: mov rcx, rsi
L018a: mov rdx, 0x7ff953af1320
L0194: cmp [rcx], rdx
L0197: je short L01a4
L0199: mov rcx, rdx
L019c: mov rdx, rsi
L019f: call System.Runtime.CompilerServices.CastHelpers.ChkCastClassSpecial(Void*, System.Object)
L01a4: mov rbx, rdi
L01a7: mov rcx, 0x7ff953af1320
L01b1: cmp [rbx], rcx
L01b4: je short L01c1
L01b6: mov rdx, rdi
L01b9: call System.Runtime.CompilerServices.CastHelpers.ChkCastClassSpecial(Void*, System.Object)
L01be: mov rbx, rax
L01c1: mov rcx, 0x191f2a86fa0
L01cb: mov r14, [rcx]
L01ce: vmovsd xmm1, [rsi+0x18]
L01d3: vmovsd xmm2, [rbx+0x18]
L01d8: vucomisd xmm2, xmm1
L01dc: jbe short L01e5
L01de: mov eax, 0xffffffff
L01e3: jmp short L0206
L01e5: vucomisd xmm1, xmm2
L01e9: jbe short L01f2
L01eb: mov eax, 1
L01f0: jmp short L0206
L01f2: vucomisd xmm1, xmm2
L01f6: jp short L01fe
L01f8: jne short L01fe
L01fa: xor eax, eax
L01fc: jmp short L0206
L01fe: mov rcx, r14
L0201: call Microsoft.FSharp.Core.LanguagePrimitives+HashCompare.GenericComparisonWithComparerIntrinsic[[System.Double, System.Private.CoreLib]](System.Collections.IComparer, Double, Double)
L0206: test eax, eax
L0208: jge short L020f
L020a: jmp L0305
L020f: test eax, eax
L0211: jle short L0218
L0213: jmp L0305
L0218: mov rcx, 0x191f2a86fa0
L0222: mov r14, [rcx]
L0225: mov rcx, [rsi+0x10]
L0229: mov rdx, [rbx+0x10]
L022d: mov r8, r14
L0230: cmp [rcx], ecx
L0232: add rsp, 0x30
L0236: pop rbx
L0237: pop rbp
L0238: pop rsi
L0239: pop rdi
L023a: pop r14
L023c: jmp Test+Test+LinearExpr.CompareTo(System.Object, System.Collections.IComparer)
L0241: mov rcx, rsi
L0244: mov rdx, 0x7ff953af14c8
L024e: cmp [rcx], rdx
L0251: je short L025e
L0253: mov rcx, rdx
L0256: mov rdx, rsi
L0259: call System.Runtime.CompilerServices.CastHelpers.ChkCastClassSpecial(Void*, System.Object)
L025e: mov rbx, rdi
L0261: mov rcx, 0x7ff953af14c8
L026b: cmp [rbx], rcx
L026e: je short L027b
L0270: mov rdx, rdi
L0273: call System.Runtime.CompilerServices.CastHelpers.ChkCastClassSpecial(Void*, System.Object)
L0278: mov rbx, rax
L027b: mov rcx, 0x191f2a86fa0
L0285: mov r14, [rcx]
L0288: mov rcx, [rsi+0x10]
L028c: mov rdx, [rbx+0x10]
L0290: mov r8, r14
L0293: cmp [rcx], ecx
L0295: call Test+Test+LinearExpr.CompareTo(System.Object, System.Collections.IComparer)
L029a: test eax, eax
L029c: jge short L02a0
L029e: jmp short L0305
L02a0: test eax, eax
L02a2: jle short L02a6
L02a4: jmp short L0305
L02a6: mov rcx, 0x191f2a86fa0
L02b0: mov r14, [rcx]
L02b3: mov rcx, [rsi+0x18]
L02b7: mov rdx, [rbx+0x18]
L02bb: mov r8, r14
L02be: cmp [rcx], ecx
L02c0: add rsp, 0x30
L02c4: pop rbx
L02c5: pop rbp
L02c6: pop rsi
L02c7: pop rdi
L02c8: pop r14
L02ca: jmp Test+Test+LinearExpr.CompareTo(System.Object, System.Collections.IComparer)
L02cf: sub eax, ebp
L02d1: jmp short L0305
L02d3: mov eax, 1
L02d8: add rsp, 0x30
L02dc: pop rbx
L02dd: pop rbp
L02de: pop rsi
L02df: pop rdi
L02e0: pop r14
L02e2: ret
L02e3: test rdi, rdi
L02e6: je short L02f8
L02e8: mov eax, 0xffffffff
L02ed: add rsp, 0x30
L02f1: pop rbx
L02f2: pop rbp
L02f3: pop rsi
L02f4: pop rdi
L02f5: pop r14
L02f7: ret
L02f8: xor eax, eax
L02fa: add rsp, 0x30
L02fe: pop rbx
L02ff: pop rbp
L0300: pop rsi
L0301: pop rdi
L0302: pop r14
L0304: ret
L0305: add rsp, 0x30
L0309: pop rbx
L030a: pop rbp
L030b: pop rsi
L030c: pop rdi
L030d: pop r14
L030f: ret

;Test+Test+LinearExpr.CompareTo(System.Object)
L0000: push rsi
L0001: sub rsp, 0x20
L0005: mov rsi, rcx
L0008: mov rax, rdx
L000b: test rax, rax
L000e: je short L0024
L0010: mov rcx, 0x7ff953af0bf0
L001a: cmp [rax], rcx
L001d: je short L0024
L001f: call System.Runtime.CompilerServices.CastHelpers.ChkCastClassSpecial(Void*, System.Object)
L0024: mov rcx, rsi
L0027: mov rdx, rax
L002a: cmp [rcx], ecx
L002c: add rsp, 0x20
L0030: pop rsi
L0031: jmp Test+Test+LinearExpr.CompareTo(LinearExpr)

;Test+Test+LinearExpr.CompareTo(System.Object, System.Collections.IComparer)
L0000: push r14
L0002: push rdi
L0003: push rsi
L0004: push rbp
L0005: push rbx
L0006: sub rsp, 0x30
L000a: vzeroupper
L000d: xor eax, eax
L000f: mov [rsp+0x20], rax
L0014: mov [rsp+0x28], rax
L0019: mov rsi, rdx
L001c: mov rdi, r8
L001f: mov rbx, rcx
L0022: mov rbp, rsi
L0025: test rbp, rbp
L0028: je short L0045
L002a: mov rcx, 0x7ff953af0bf0
L0034: cmp [rbp], rcx
L0038: je short L0045
L003a: mov rdx, rsi
L003d: call System.Runtime.CompilerServices.CastHelpers.ChkCastClassSpecial(Void*, System.Object)
L0042: mov rbp, rax
L0045: test rbx, rbx
L0048: je L02d5
L004e: test rbp, rbp
L0051: je short L006b
L0053: mov rcx, 0x7ff953af0bf0
L005d: cmp [rbp], rcx
L0061: je short L006b
L0063: mov rdx, rbp
L0066: call System.Runtime.CompilerServices.CastHelpers.ChkCastClassSpecial(Void*, System.Object)
L006b: test rbp, rbp
L006e: je L02c5
L0074: mov r14d, [rbx+8]
L0078: mov eax, r14d
L007b: mov esi, [rbp+8]
L007e: cmp eax, esi
L0080: jne L02c1
L0086: cmp r14d, 3
L008a: ja L01ad
L0090: mov ecx, r14d
L0093: lea rdx, [Test+Test+LinearExpr.CompareTo(System.Object, System.Collections.IComparer)]
L009a: mov edx, [rdx+rcx*4]
L009d: lea rax, [L001f]
L00a4: add rdx, rax
L00a7: jmp rdx
L00a9: mov rsi, rbx
L00ac: mov rcx, 0x7ff953af1320
L00b6: cmp [rsi], rcx
L00b9: je short L00c6
L00bb: mov rdx, rbx
L00be: call System.Runtime.CompilerServices.CastHelpers.ChkCastClassSpecial(Void*, System.Object)
L00c3: mov rsi, rax
L00c6: mov rbx, rbp
L00c9: mov rcx, 0x7ff953af1320
L00d3: cmp [rbx], rcx
L00d6: je short L00e3
L00d8: mov rdx, rbp
L00db: call System.Runtime.CompilerServices.CastHelpers.ChkCastClassSpecial(Void*, System.Object)
L00e0: mov rbx, rax
L00e3: vmovsd xmm1, [rsi+0x18]
L00e8: vmovsd xmm2, [rbx+0x18]
L00ed: vucomisd xmm2, xmm1
L00f1: jbe short L00fa
L00f3: mov eax, 0xffffffff
L00f8: jmp short L011b
L00fa: vucomisd xmm1, xmm2
L00fe: jbe short L0107
L0100: mov eax, 1
L0105: jmp short L011b
L0107: vucomisd xmm1, xmm2
L010b: jp short L0113
L010d: jne short L0113
L010f: xor eax, eax
L0111: jmp short L011b
L0113: mov rcx, rdi
L0116: call Microsoft.FSharp.Core.LanguagePrimitives+HashCompare.GenericComparisonWithComparerIntrinsic[[System.Double, System.Private.CoreLib]](System.Collections.IComparer, Double, Double)
L011b: test eax, eax
L011d: jl L02b9
L0123: test eax, eax
L0125: jg L02bb
L012b: mov rcx, [rsi+0x10]
L012f: mov rdx, [rbx+0x10]
L0133: mov rsi, rdx
L0136: mov rbx, rcx
L0139: jmp L0022
L013e: mov rsi, rbx
L0141: mov rcx, 0x7ff953af14c8
L014b: cmp [rsi], rcx
L014e: je short L015b
L0150: mov rdx, rbx
L0153: call System.Runtime.CompilerServices.CastHelpers.ChkCastClassSpecial(Void*, System.Object)
L0158: mov rsi, rax
L015b: mov rbx, rbp
L015e: mov rcx, 0x7ff953af14c8
L0168: cmp [rbx], rcx
L016b: je short L0178
L016d: mov rdx, rbp
L0170: call System.Runtime.CompilerServices.CastHelpers.ChkCastClassSpecial(Void*, System.Object)
L0175: mov rbx, rax
L0178: mov rcx, [rsi+0x10]
L017c: mov rdx, [rbx+0x10]
L0180: mov r8, rdi
L0183: cmp [rcx], ecx
L0185: call Test+Test+LinearExpr.CompareTo(System.Object, System.Collections.IComparer)
L018a: test eax, eax
L018c: jl L02bd
L0192: test eax, eax
L0194: jg L02bf
L019a: mov rcx, [rsi+0x18]
L019e: mov rdx, [rbx+0x18]
L01a2: mov rsi, rdx
L01a5: mov rbx, rcx
L01a8: jmp L0022
L01ad: mov rsi, rbx
L01b0: mov rcx, 0x7ff953af0ff8
L01ba: cmp [rsi], rcx
L01bd: je short L01ca
L01bf: mov rdx, rbx
L01c2: call System.Runtime.CompilerServices.CastHelpers.ChkCastClassSpecial(Void*, System.Object)
L01c7: mov rsi, rax
L01ca: mov rax, rbp
L01cd: mov rcx, 0x7ff953af0ff8
L01d7: cmp [rax], rcx
L01da: je short L01e4
L01dc: mov rdx, rbp
L01df: call System.Runtime.CompilerServices.CastHelpers.ChkCastClassSpecial(Void*, System.Object)
L01e4: vmovsd xmm1, [rsi+0x10]
L01e9: vmovsd xmm2, [rax+0x10]
L01ee: vucomisd xmm2, xmm1
L01f2: ja L02f9
L01f8: vucomisd xmm1, xmm2
L01fc: ja L02c5
L0202: vucomisd xmm1, xmm2
L0206: jp short L020e
L0208: je L0309
L020e: mov rcx, rdi
L0211: add rsp, 0x30
L0215: pop rbx
L0216: pop rbp
L0217: pop rsi
L0218: pop rdi
L0219: pop r14
L021b: jmp Microsoft.FSharp.Core.LanguagePrimitives+HashCompare.GenericComparisonWithComparerIntrinsic[[System.Double, System.Private.CoreLib]](System.Collections.IComparer, Double, Double)
L0220: mov rsi, rbx
L0223: mov rcx, 0x7ff953af1178
L022d: cmp [rsi], rcx
L0230: je short L023d
L0232: mov rdx, rbx
L0235: call System.Runtime.CompilerServices.CastHelpers.ChkCastClassSpecial(Void*, System.Object)
L023a: mov rsi, rax
L023d: mov rbx, rbp
L0240: mov rcx, 0x7ff953af1178
L024a: cmp [rbx], rcx
L024d: je short L025a
L024f: mov rdx, rbp
L0252: call System.Runtime.CompilerServices.CastHelpers.ChkCastClassSpecial(Void*, System.Object)
L0257: mov rbx, rax
L025a: add rsi, 0x10
L025e: mov rcx, [rsi]
L0261: mov [rsp+0x20], rcx
L0266: mov rcx, [rsi+8]
L026a: mov [rsp+0x28], rcx
L026f: add rbx, 0x10
L0273: mov rsi, [rbx]
L0276: mov rbx, [rbx+8]
L027a: mov rcx, 0x7ff953af0538
L0284: call 0x00007ff9b182a470
L0289: mov rbp, rax
L028c: lea r14, [rbp+8]
L0290: mov rcx, r14
L0293: mov rdx, rsi
L0296: call 0x00007ff9b182a050
L029b: lea rcx, [r14+8]
L029f: mov rdx, rbx
L02a2: call 0x00007ff9b182a050
L02a7: mov rdx, rbp
L02aa: lea rcx, [rsp+0x20]
L02af: mov r8, rdi
L02b2: call Test+Test+Decision.CompareTo(System.Object, System.Collections.IComparer)
L02b7: jmp short L0316
L02b9: jmp short L0316
L02bb: jmp short L0316
L02bd: jmp short L0316
L02bf: jmp short L0316
L02c1: sub eax, esi
L02c3: jmp short L0316
L02c5: mov eax, 1
L02ca: add rsp, 0x30
L02ce: pop rbx
L02cf: pop rbp
L02d0: pop rsi
L02d1: pop rdi
L02d2: pop r14
L02d4: ret
L02d5: mov rax, rsi
L02d8: test rax, rax
L02db: je short L02f4
L02dd: mov rcx, 0x7ff953af0bf0
L02e7: cmp [rax], rcx
L02ea: je short L02f4
L02ec: mov rdx, rsi
L02ef: call System.Runtime.CompilerServices.CastHelpers.ChkCastClassSpecial(Void*, System.Object)
L02f4: test rax, rax
L02f7: je short L0309
L02f9: mov eax, 0xffffffff
L02fe: add rsp, 0x30
L0302: pop rbx
L0303: pop rbp
L0304: pop rsi
L0305: pop rdi
L0306: pop r14
L0308: ret
L0309: xor eax, eax
L030b: add rsp, 0x30
L030f: pop rbx
L0310: pop rbp
L0311: pop rsi
L0312: pop rdi
L0313: pop r14
L0315: ret
L0316: add rsp, 0x30
L031a: pop rbx
L031b: pop rbp
L031c: pop rsi
L031d: pop rdi
L031e: pop r14
L0320: ret

;Test+Test+LinearExpr.GetHashCode(System.Collections.IEqualityComparer)
L0000: push rdi
L0001: push rsi
L0002: push rbp
L0003: push rbx
L0004: sub rsp, 0x38
L0008: vzeroupper
L000b: xor eax, eax
L000d: mov [rsp+0x28], rax
L0012: mov [rsp+0x30], rax
L0017: mov rsi, rcx
L001a: mov rdi, rdx
L001d: test rsi, rsi
L0020: je L017b
L0026: mov ebx, [rsi+8]
L0029: cmp ebx, 3
L002c: ja short L0046
L002e: mov ecx, ebx
L0030: lea rdx, [Test+Test+LinearExpr.GetHashCode(System.Collections.IEqualityComparer)]
L0037: mov edx, [rdx+rcx*4]
L003a: lea rax, [L001d]
L0041: add rdx, rax
L0044: jmp rdx
L0046: mov rcx, rsi
L0049: mov rdx, 0x7ff953af0ff8
L0053: cmp [rcx], rdx
L0056: je short L0063
L0058: mov rcx, rdx
L005b: mov rdx, rsi
L005e: call System.Runtime.CompilerServices.CastHelpers.ChkCastClassSpecial(Void*, System.Object)
L0063: vmovsd xmm1, [rsi+0x10]
L0068: mov rcx, rdi
L006b: call Microsoft.FSharp.Core.LanguagePrimitives+HashCompare.GenericHashWithComparerIntrinsic[[System.Double, System.Private.CoreLib]](System.Collections.IEqualityComparer, Double)
L0070: lea ebp, [rax-0x61c88647]
L0076: mov eax, ebp
L0078: jmp L0172
L007d: mov rcx, rsi
L0080: mov rdx, 0x7ff953af1178
L008a: cmp [rcx], rdx
L008d: je short L009a
L008f: mov rcx, rdx
L0092: mov rdx, rsi
L0095: call System.Runtime.CompilerServices.CastHelpers.ChkCastClassSpecial(Void*, System.Object)
L009a: add rsi, 0x10
L009e: mov rcx, [rsi]
L00a1: mov [rsp+0x28], rcx
L00a6: mov rcx, [rsi+8]
L00aa: mov [rsp+0x30], rcx
L00af: lea rcx, [rsp+0x28]
L00b4: mov rdx, rdi
L00b7: call Test+Test+Decision.GetHashCode(System.Collections.IEqualityComparer)
L00bc: lea ebp, [rax-0x61c88607]
L00c2: mov eax, ebp
L00c4: jmp L0172
L00c9: mov rcx, rsi
L00cc: mov rdx, 0x7ff953af1320
L00d6: cmp [rcx], rdx
L00d9: je short L00e6
L00db: mov rcx, rdx
L00de: mov rdx, rsi
L00e1: call System.Runtime.CompilerServices.CastHelpers.ChkCastClassSpecial(Void*, System.Object)
L00e6: mov rcx, [rsi+0x10]
L00ea: mov rdx, rdi
L00ed: cmp [rcx], ecx
L00ef: call Test+Test+LinearExpr.GetHashCode(System.Collections.IEqualityComparer)
L00f4: lea ebp, [rax-0x61c885c7]
L00fa: vmovsd xmm1, [rsi+0x18]
L00ff: mov rcx, rdi
L0102: call Microsoft.FSharp.Core.LanguagePrimitives+HashCompare.GenericHashWithComparerIntrinsic[[System.Double, System.Private.CoreLib]](System.Collections.IEqualityComparer, Double)
L0107: mov ecx, ebp
L0109: shl ecx, 6
L010c: add ecx, eax
L010e: mov edx, ebp
L0110: sar edx, 2
L0113: lea ebp, [rcx+rdx-0x61c88647]
L011a: mov eax, ebp
L011c: jmp short L0172
L011e: mov rcx, rsi
L0121: mov rdx, 0x7ff953af14c8
L012b: cmp [rcx], rdx
L012e: je short L013b
L0130: mov rcx, rdx
L0133: mov rdx, rsi
L0136: call System.Runtime.CompilerServices.CastHelpers.ChkCastClassSpecial(Void*, System.Object)
L013b: mov rcx, [rsi+0x18]
L013f: mov rdx, rdi
L0142: cmp [rcx], ecx
L0144: call Test+Test+LinearExpr.GetHashCode(System.Collections.IEqualityComparer)
L0149: lea ebp, [rax-0x61c88587]
L014f: mov rcx, [rsi+0x10]
L0153: mov rdx, rdi
L0156: cmp [rcx], ecx
L0158: call Test+Test+LinearExpr.GetHashCode(System.Collections.IEqualityComparer)
L015d: mov edx, ebp
L015f: shl edx, 6
L0162: add eax, edx
L0164: mov edx, ebp
L0166: sar edx, 2
L0169: lea ebp, [rax+rdx-0x61c88647]
L0170: mov eax, ebp
L0172: add rsp, 0x38
L0176: pop rbx
L0177: pop rbp
L0178: pop rsi
L0179: pop rdi
L017a: ret
L017b: xor eax, eax
L017d: add rsp, 0x38
L0181: pop rbx
L0182: pop rbp
L0183: pop rsi
L0184: pop rdi
L0185: ret

;Test+Test+LinearExpr.GetHashCode()
L0000: mov rdx, 0x191f2a86fa8
L000a: mov rdx, [rdx]
L000d: cmp [rcx], ecx
L000f: jmp Test+Test+LinearExpr.GetHashCode(System.Collections.IEqualityComparer)

;Test+Test+LinearExpr.Equals(System.Object, System.Collections.IEqualityComparer)
L0000: push r14
L0002: push rdi
L0003: push rsi
L0004: push rbp
L0005: push rbx
L0006: sub rsp, 0x30
L000a: vzeroupper
L000d: xor eax, eax
L000f: mov [rsp+0x20], rax
L0014: mov [rsp+0x28], rax
L0019: mov rsi, r8
L001c: mov rdi, rcx
L001f: test rdi, rdi
L0022: je L023a
L0028: mov rcx, 0x7ff953af0bf0
L0032: call System.Runtime.CompilerServices.CastHelpers.IsInstanceOfClass(Void*, System.Object)
L0037: mov rbx, rax
L003a: test rbx, rbx
L003d: je L022d
L0043: mov ebp, [rdi+8]
L0046: mov ecx, ebp
L0048: mov edx, [rbx+8]
L004b: cmp ecx, edx
L004d: jne L022d
L0053: cmp ebp, 3
L0056: ja L013a
L005c: mov ecx, ebp
L005e: lea rdx, [Test+Test+LinearExpr.Equals(System.Object, System.Collections.IEqualityComparer)]
L0065: mov edx, [rdx+rcx*4]
L0068: lea rax, [L001c]
L006f: add rdx, rax
L0072: jmp rdx
L0074: mov r14, rdi
L0077: mov rcx, 0x7ff953af1320
L0081: cmp [r14], rcx
L0084: je short L0091
L0086: mov rdx, rdi
L0089: call System.Runtime.CompilerServices.CastHelpers.ChkCastClassSpecial(Void*, System.Object)
L008e: mov r14, rax
L0091: mov rdi, rbx
L0094: mov rcx, 0x7ff953af1320
L009e: cmp [rdi], rcx
L00a1: je short L00ae
L00a3: mov rdx, rbx
L00a6: call System.Runtime.CompilerServices.CastHelpers.ChkCastClassSpecial(Void*, System.Object)
L00ab: mov rdi, rax
L00ae: vmovsd xmm0, [r14+0x18]
L00b4: vucomisd xmm0, [rdi+0x18]
L00b9: jp L022d
L00bf: jne L022d
L00c5: mov rcx, [r14+0x10]
L00c9: mov rdx, [rdi+0x10]
L00cd: mov rdi, rcx
L00d0: jmp L001f
L00d5: mov rbp, rdi
L00d8: mov rcx, 0x7ff953af14c8
L00e2: cmp [rbp], rcx
L00e6: je short L00f3
L00e8: mov rdx, rdi
L00eb: call System.Runtime.CompilerServices.CastHelpers.ChkCastClassSpecial(Void*, System.Object)
L00f0: mov rbp, rax
L00f3: mov rdi, rbx
L00f6: mov rcx, 0x7ff953af14c8
L0100: cmp [rdi], rcx
L0103: je short L0110
L0105: mov rdx, rbx
L0108: call System.Runtime.CompilerServices.CastHelpers.ChkCastClassSpecial(Void*, System.Object)
L010d: mov rdi, rax
L0110: mov rcx, [rbp+0x10]
L0114: mov rdx, [rdi+0x10]
L0118: mov r8, rsi
L011b: cmp [rcx], ecx
L011d: call Test+Test+LinearExpr.Equals(System.Object, System.Collections.IEqualityComparer)
L0122: test eax, eax
L0124: je L022d
L012a: mov rcx, [rbp+0x18]
L012e: mov rdx, [rdi+0x18]
L0132: mov rdi, rcx
L0135: jmp L001f
L013a: mov rbp, rdi
L013d: mov rcx, 0x7ff953af0ff8
L0147: cmp [rbp], rcx
L014b: je short L0158
L014d: mov rdx, rdi
L0150: call System.Runtime.CompilerServices.CastHelpers.ChkCastClassSpecial(Void*, System.Object)
L0155: mov rbp, rax
L0158: mov rax, rbx
L015b: mov rcx, 0x7ff953af0ff8
L0165: cmp [rax], rcx
L0168: je short L0172
L016a: mov rdx, rbx
L016d: call System.Runtime.CompilerServices.CastHelpers.ChkCastClassSpecial(Void*, System.Object)
L0172: vmovsd xmm0, [rbp+0x10]
L0177: vucomisd xmm0, [rax+0x10]
L017c: setnp r14b
L0180: jp short L0186
L0182: sete r14b
L0186: movzx r14d, r14b
L018a: jmp L0245
L018f: mov rbp, rdi
L0192: mov rcx, 0x7ff953af1178
L019c: cmp [rbp], rcx
L01a0: je short L01ad
L01a2: mov rdx, rdi
L01a5: call System.Runtime.CompilerServices.CastHelpers.ChkCastClassSpecial(Void*, System.Object)
L01aa: mov rbp, rax
L01ad: mov rdi, rbx
L01b0: mov rcx, 0x7ff953af1178
L01ba: cmp [rdi], rcx
L01bd: je short L01ca
L01bf: mov rdx, rbx
L01c2: call System.Runtime.CompilerServices.CastHelpers.ChkCastClassSpecial(Void*, System.Object)
L01c7: mov rdi, rax
L01ca: add rbp, 0x10
L01ce: mov rcx, [rbp]
L01d2: mov [rsp+0x20], rcx
L01d7: mov rcx, [rbp+8]
L01db: mov [rsp+0x28], rcx
L01e0: add rdi, 0x10
L01e4: mov rbx, [rdi]
L01e7: mov rdi, [rdi+8]
L01eb: mov rcx, 0x7ff953af0538
L01f5: call 0x00007ff9b182a470
L01fa: mov rbp, rax
L01fd: lea r14, [rbp+8]
L0201: mov rcx, r14
L0204: mov rdx, rbx
L0207: call 0x00007ff9b182a050
L020c: lea rcx, [r14+8]
L0210: mov rdx, rdi
L0213: call 0x00007ff9b182a050
L0218: mov rdx, rbp
L021b: lea rcx, [rsp+0x20]
L0220: mov r8, rsi
L0223: call Test+Test+Decision.Equals(System.Object, System.Collections.IEqualityComparer)
L0228: mov r14d, eax
L022b: jmp short L0245
L022d: xor eax, eax
L022f: add rsp, 0x30
L0233: pop rbx
L0234: pop rbp
L0235: pop rsi
L0236: pop rdi
L0237: pop r14
L0239: ret
L023a: test rdx, rdx
L023d: sete r14b
L0241: movzx r14d, r14b
L0245: movzx eax, r14b
L0249: add rsp, 0x30
L024d: pop rbx
L024e: pop rbp
L024f: pop rsi
L0250: pop rdi
L0251: pop r14
L0253: ret

;Test+Test+LinearExpr.Equals(LinearExpr)
L0000: push rdi
L0001: push rsi
L0002: push rbx
L0003: sub rsp, 0x40
L0007: vzeroupper
L000a: vmovaps [rsp+0x30], xmm6
L0010: vmovaps [rsp+0x20], xmm7
L0016: mov rsi, rdx
L0019: mov rdx, rcx
L001c: test rdx, rdx
L001f: je L022c
L0025: test rsi, rsi
L0028: je L0216
L002e: mov edi, [rdx+8]
L0031: mov ecx, edi
L0033: mov eax, [rsi+8]
L0036: cmp ecx, eax
L0038: jne L0216
L003e: cmp edi, 3
L0041: ja L012e
L0047: mov ecx, edi
L0049: lea rax, [Test+Test+LinearExpr.Equals(LinearExpr)]
L0050: mov eax, [rax+rcx*4]
L0053: lea r8, [L0019]
L005a: add rax, r8
L005d: jmp rax
L005f: mov rbx, rdx
L0062: mov rcx, 0x7ff953af1320
L006c: cmp [rbx], rcx
L006f: je short L0079
L0071: call System.Runtime.CompilerServices.CastHelpers.ChkCastClassSpecial(Void*, System.Object)
L0076: mov rbx, rax
L0079: mov rdi, rsi
L007c: mov rcx, 0x7ff953af1320
L0086: cmp [rdi], rcx
L0089: je short L0096
L008b: mov rdx, rsi
L008e: call System.Runtime.CompilerServices.CastHelpers.ChkCastClassSpecial(Void*, System.Object)
L0093: mov rdi, rax
L0096: vmovsd xmm6, [rbx+0x18]
L009b: vmovsd xmm7, [rdi+0x18]
L00a0: vucomisd xmm6, xmm7
L00a4: jp short L00a8
L00a6: je short L00c6
L00a8: vucomisd xmm6, xmm6
L00ac: jp short L00b4
L00ae: je L0216
L00b4: vucomisd xmm7, xmm7
L00b8: setp cl
L00bb: movzx ecx, cl
L00be: test ecx, ecx
L00c0: je L0216
L00c6: mov rdx, [rbx+0x10]
L00ca: mov rsi, [rdi+0x10]
L00ce: jmp L001c
L00d3: mov rdi, rdx
L00d6: mov rcx, 0x7ff953af14c8
L00e0: cmp [rdi], rcx
L00e3: je short L00ed
L00e5: call System.Runtime.CompilerServices.CastHelpers.ChkCastClassSpecial(Void*, System.Object)
L00ea: mov rdi, rax
L00ed: mov rbx, rsi
L00f0: mov rcx, 0x7ff953af14c8
L00fa: cmp [rbx], rcx
L00fd: je short L010a
L00ff: mov rdx, rsi
L0102: call System.Runtime.CompilerServices.CastHelpers.ChkCastClassSpecial(Void*, System.Object)
L0107: mov rbx, rax
L010a: mov rcx, [rdi+0x10]
L010e: mov rdx, [rbx+0x10]
L0112: cmp [rcx], ecx
L0114: call Test+Test+LinearExpr.Equals(LinearExpr)
L0119: test eax, eax
L011b: je L0216
L0121: mov rdx, [rdi+0x18]
L0125: mov rsi, [rbx+0x18]
L0129: jmp L001c
L012e: mov rdi, rdx
L0131: mov rcx, 0x7ff953af0ff8
L013b: cmp [rdi], rcx
L013e: je short L0148
L0140: call System.Runtime.CompilerServices.CastHelpers.ChkCastClassSpecial(Void*, System.Object)
L0145: mov rdi, rax
L0148: mov rax, rsi
L014b: mov rcx, 0x7ff953af0ff8
L0155: cmp [rax], rcx
L0158: je short L0162
L015a: mov rdx, rsi
L015d: call System.Runtime.CompilerServices.CastHelpers.ChkCastClassSpecial(Void*, System.Object)
L0162: vmovsd xmm6, [rdi+0x10]
L0167: vmovsd xmm7, [rax+0x10]
L016c: vucomisd xmm6, xmm7
L0170: jp short L018d
L0172: jne short L018d
L0174: mov eax, 1
L0179: vmovaps xmm6, [rsp+0x30]
L017f: vmovaps xmm7, [rsp+0x20]
L0185: add rsp, 0x40
L0189: pop rbx
L018a: pop rsi
L018b: pop rdi
L018c: ret
L018d: vucomisd xmm6, xmm6
L0191: jp short L0199
L0193: je L0216
L0199: vucomisd xmm7, xmm7
L019d: setp bl
L01a0: movzx ebx, bl
L01a3: jmp L0235
L01a8: mov rdi, rdx
L01ab: mov rcx, 0x7ff953af1178
L01b5: cmp [rdi], rcx
L01b8: je short L01c2
L01ba: call System.Runtime.CompilerServices.CastHelpers.ChkCastClassSpecial(Void*, System.Object)
L01bf: mov rdi, rax
L01c2: mov rbx, rsi
L01c5: mov rcx, 0x7ff953af1178
L01cf: cmp [rbx], rcx
L01d2: je short L01df
L01d4: mov rdx, rsi
L01d7: call System.Runtime.CompilerServices.CastHelpers.ChkCastClassSpecial(Void*, System.Object)
L01dc: mov rbx, rax
L01df: add rdi, 0x10
L01e3: mov rcx, [rdi]
L01e6: mov rsi, [rdi+8]
L01ea: add rbx, 0x10
L01ee: mov rdx, [rbx]
L01f1: mov rdi, [rbx+8]
L01f5: cmp [rcx], ecx
L01f7: call Test+Test+DecisionName.Equals(DecisionName)
L01fc: test eax, eax
L01fe: je short L0212
L0200: mov rcx, rsi
L0203: mov rdx, rdi
L0206: cmp [rcx], ecx
L0208: call Test+Test+DecisionType.Equals(DecisionType)
L020d: movzx ebx, al
L0210: jmp short L0214
L0212: xor ebx, ebx
L0214: jmp short L0235
L0216: xor eax, eax
L0218: vmovaps xmm6, [rsp+0x30]
L021e: vmovaps xmm7, [rsp+0x20]
L0224: add rsp, 0x40
L0228: pop rbx
L0229: pop rsi
L022a: pop rdi
L022b: ret
L022c: test rsi, rsi
L022f: sete bl
L0232: movzx ebx, bl
L0235: movzx eax, bl
L0238: vmovaps xmm6, [rsp+0x30]
L023e: vmovaps xmm7, [rsp+0x20]
L0244: add rsp, 0x40
L0248: pop rbx
L0249: pop rsi
L024a: pop rdi
L024b: ret

;Test+Test+LinearExpr.Equals(System.Object)
L0000: push rsi
L0001: sub rsp, 0x20
L0005: mov rsi, rcx
L0008: mov rcx, 0x7ff953af0bf0
L0012: call System.Runtime.CompilerServices.CastHelpers.IsInstanceOfClass(Void*, System.Object)
L0017: test rax, rax
L001a: je short L002e
L001c: mov rcx, rsi
L001f: mov rdx, rax
L0022: cmp [rcx], ecx
L0024: add rsp, 0x20
L0028: pop rsi
L0029: jmp Test+Test+LinearExpr.Equals(LinearExpr)
L002e: xor eax, eax
L0030: add rsp, 0x20
L0034: pop rsi
L0035: ret

;Test+Test+LinearExpr+Decision.get_Item()
L0000: push rdi
L0001: push rsi
L0002: push rbx
L0003: mov rbx, rdx
L0006: lea rsi, [rcx+0x10]
L000a: mov rdi, rbx
L000d: call 0x00007ff9b182a130
L0012: call 0x00007ff9b182a130
L0017: mov rax, rbx
L001a: pop rbx
L001b: pop rsi
L001c: pop rdi
L001d: ret

;Test+Test+LinearExpr.get_IsFloat()
L0000: cmp dword ptr [rcx+8], 0
L0004: sete al
L0007: movzx eax, al
L000a: ret

;Test+Test+LinearExpr.get_IsDecision()
L0000: cmp dword ptr [rcx+8], 1
L0004: sete al
L0007: movzx eax, al
L000a: ret

;Test+Test+LinearExpr.get_IsScale()
L0000: cmp dword ptr [rcx+8], 2
L0004: sete al
L0007: movzx eax, al
L000a: ret

;Test+Test+LinearExpr.get_IsAdd()
L0000: cmp dword ptr [rcx+8], 3
L0004: sete al
L0007: movzx eax, al
L000a: ret

;Test+Test+LinearExpr.get_Tag()
L0000: mov eax, [rcx+8]
L0003: ret

;Test+Test+LinearExpr.ToString()
L0000: push rdi
L0001: push rsi
L0002: sub rsp, 0x28
L0006: mov rsi, rcx
L0009: mov rcx, 0x7ff953af84a0
L0013: call 0x00007ff9b182a470
L0018: mov rdi, rax
L001b: mov rdx, 0x191f2a9b3f0
L0025: mov rdx, [rdx]
L0028: lea rcx, [rdi+8]
L002c: call 0x00007ff9b182a080
L0031: xor edx, edx
L0033: mov [rdi+0x10], rdx
L0037: mov [rdi+0x18], rdx
L003b: mov rdx, rdi
L003e: mov rcx, 0x7ff953af8780
L0048: call Microsoft.FSharp.Core.PrintfModule.PrintFormatToStringThen[[System.__Canon, System.Private.CoreLib]](Microsoft.FSharp.Core.PrintfFormat`4<System.__Canon,Microsoft.FSharp.Core.Unit,System.String,System.String>)
L004d: mov rcx, rax
L0050: mov rdx, rsi
L0053: mov rax, [rax]
L0056: mov rax, [rax+0x40]
L005a: mov rax, [rax+0x20]
L005e: add rsp, 0x28
L0062: pop rsi
L0063: pop rdi
L0064: jmp rax

;Test+Test+LinearExpr.CompareTo(LinearExpr)
L0000: push r14
L0002: push rdi
L0003: push rsi
L0004: push rbp
L0005: push rbx
L0006: sub rsp, 0x30
L000a: vzeroupper
L000d: xor eax, eax
L000f: mov [rsp+0x20], rax
L0014: mov [rsp+0x28], rax
L0019: mov rsi, rcx
L001c: mov rdi, rdx
L001f: test rsi, rsi
L0022: je L02e3
L0028: test rdi, rdi
L002b: je L02d3
L0031: mov ebx, [rsi+8]
L0034: mov eax, ebx
L0036: mov ebp, [rdi+8]
L0039: cmp eax, ebp
L003b: jne L02cf
L0041: cmp ebx, 3
L0044: ja short L005e
L0046: mov ecx, ebx
L0048: lea rdx, [Test+Test+LinearExpr.CompareTo(LinearExpr)]
L004f: mov edx, [rdx+rcx*4]
L0052: lea rax, [L001f]
L0059: add rdx, rax
L005c: jmp rdx
L005e: mov rcx, rsi
L0061: mov rdx, 0x7ff953af0ff8
L006b: cmp [rcx], rdx
L006e: je short L007b
L0070: mov rcx, rdx
L0073: mov rdx, rsi
L0076: call System.Runtime.CompilerServices.CastHelpers.ChkCastClassSpecial(Void*, System.Object)
L007b: mov rax, rdi
L007e: mov rcx, 0x7ff953af0ff8
L0088: cmp [rax], rcx
L008b: je short L0095
L008d: mov rdx, rdi
L0090: call System.Runtime.CompilerServices.CastHelpers.ChkCastClassSpecial(Void*, System.Object)
L0095: mov rcx, 0x191f2a86fa0
L009f: mov r14, [rcx]
L00a2: vmovsd xmm1, [rsi+0x10]
L00a7: vmovsd xmm2, [rax+0x10]
L00ac: vucomisd xmm2, xmm1
L00b0: ja L02e8
L00b6: vucomisd xmm1, xmm2
L00ba: ja L02d3
L00c0: vucomisd xmm1, xmm2
L00c4: jp short L00cc
L00c6: je L02f8
L00cc: mov rcx, r14
L00cf: add rsp, 0x30
L00d3: pop rbx
L00d4: pop rbp
L00d5: pop rsi
L00d6: pop rdi
L00d7: pop r14
L00d9: jmp Microsoft.FSharp.Core.LanguagePrimitives+HashCompare.GenericComparisonWithComparerIntrinsic[[System.Double, System.Private.CoreLib]](System.Collections.IComparer, Double, Double)
L00de: mov rcx, rsi
L00e1: mov rdx, 0x7ff953af1178
L00eb: cmp [rcx], rdx
L00ee: je short L00fb
L00f0: mov rcx, rdx
L00f3: mov rdx, rsi
L00f6: call System.Runtime.CompilerServices.CastHelpers.ChkCastClassSpecial(Void*, System.Object)
L00fb: mov rbx, rdi
L00fe: mov rcx, 0x7ff953af1178
L0108: cmp [rbx], rcx
L010b: je short L0118
L010d: mov rdx, rdi
L0110: call System.Runtime.CompilerServices.CastHelpers.ChkCastClassSpecial(Void*, System.Object)
L0115: mov rbx, rax
L0118: mov rcx, 0x191f2a86fa0
L0122: mov r14, [rcx]
L0125: add rsi, 0x10
L0129: mov rcx, [rsi]
L012c: mov [rsp+0x20], rcx
L0131: mov rcx, [rsi+8]
L0135: mov [rsp+0x28], rcx
L013a: add rbx, 0x10
L013e: mov rdi, [rbx]
L0141: mov rsi, [rbx+8]
L0145: mov rcx, 0x7ff953af0538
L014f: call 0x00007ff9b182a470
L0154: mov rbx, rax
L0157: lea rbp, [rbx+8]
L015b: mov rcx, rbp
L015e: mov rdx, rdi
L0161: call 0x00007ff9b182a050
L0166: lea rcx, [rbp+8]
L016a: mov rdx, rsi
L016d: call 0x00007ff9b182a050
L0172: mov rdx, rbx
L0175: lea rcx, [rsp+0x20]
L017a: mov r8, r14
L017d: call Test+Test+Decision.CompareTo(System.Object, System.Collections.IComparer)
L0182: jmp L0305
L0187: mov rcx, rsi
L018a: mov rdx, 0x7ff953af1320
L0194: cmp [rcx], rdx
L0197: je short L01a4
L0199: mov rcx, rdx
L019c: mov rdx, rsi
L019f: call System.Runtime.CompilerServices.CastHelpers.ChkCastClassSpecial(Void*, System.Object)
L01a4: mov rbx, rdi
L01a7: mov rcx, 0x7ff953af1320
L01b1: cmp [rbx], rcx
L01b4: je short L01c1
L01b6: mov rdx, rdi
L01b9: call System.Runtime.CompilerServices.CastHelpers.ChkCastClassSpecial(Void*, System.Object)
L01be: mov rbx, rax
L01c1: mov rcx, 0x191f2a86fa0
L01cb: mov r14, [rcx]
L01ce: vmovsd xmm1, [rsi+0x18]
L01d3: vmovsd xmm2, [rbx+0x18]
L01d8: vucomisd xmm2, xmm1
L01dc: jbe short L01e5
L01de: mov eax, 0xffffffff
L01e3: jmp short L0206
L01e5: vucomisd xmm1, xmm2
L01e9: jbe short L01f2
L01eb: mov eax, 1
L01f0: jmp short L0206
L01f2: vucomisd xmm1, xmm2
L01f6: jp short L01fe
L01f8: jne short L01fe
L01fa: xor eax, eax
L01fc: jmp short L0206
L01fe: mov rcx, r14
L0201: call Microsoft.FSharp.Core.LanguagePrimitives+HashCompare.GenericComparisonWithComparerIntrinsic[[System.Double, System.Private.CoreLib]](System.Collections.IComparer, Double, Double)
L0206: test eax, eax
L0208: jge short L020f
L020a: jmp L0305
L020f: test eax, eax
L0211: jle short L0218
L0213: jmp L0305
L0218: mov rcx, 0x191f2a86fa0
L0222: mov r14, [rcx]
L0225: mov rcx, [rsi+0x10]
L0229: mov rdx, [rbx+0x10]
L022d: mov r8, r14
L0230: cmp [rcx], ecx
L0232: add rsp, 0x30
L0236: pop rbx
L0237: pop rbp
L0238: pop rsi
L0239: pop rdi
L023a: pop r14
L023c: jmp Test+Test+LinearExpr.CompareTo(System.Object, System.Collections.IComparer)
L0241: mov rcx, rsi
L0244: mov rdx, 0x7ff953af14c8
L024e: cmp [rcx], rdx
L0251: je short L025e
L0253: mov rcx, rdx
L0256: mov rdx, rsi
L0259: call System.Runtime.CompilerServices.CastHelpers.ChkCastClassSpecial(Void*, System.Object)
L025e: mov rbx, rdi
L0261: mov rcx, 0x7ff953af14c8
L026b: cmp [rbx], rcx
L026e: je short L027b
L0270: mov rdx, rdi
L0273: call System.Runtime.CompilerServices.CastHelpers.ChkCastClassSpecial(Void*, System.Object)
L0278: mov rbx, rax
L027b: mov rcx, 0x191f2a86fa0
L0285: mov r14, [rcx]
L0288: mov rcx, [rsi+0x10]
L028c: mov rdx, [rbx+0x10]
L0290: mov r8, r14
L0293: cmp [rcx], ecx
L0295: call Test+Test+LinearExpr.CompareTo(System.Object, System.Collections.IComparer)
L029a: test eax, eax
L029c: jge short L02a0
L029e: jmp short L0305
L02a0: test eax, eax
L02a2: jle short L02a6
L02a4: jmp short L0305
L02a6: mov rcx, 0x191f2a86fa0
L02b0: mov r14, [rcx]
L02b3: mov rcx, [rsi+0x18]
L02b7: mov rdx, [rbx+0x18]
L02bb: mov r8, r14
L02be: cmp [rcx], ecx
L02c0: add rsp, 0x30
L02c4: pop rbx
L02c5: pop rbp
L02c6: pop rsi
L02c7: pop rdi
L02c8: pop r14
L02ca: jmp Test+Test+LinearExpr.CompareTo(System.Object, System.Collections.IComparer)
L02cf: sub eax, ebp
L02d1: jmp short L0305
L02d3: mov eax, 1
L02d8: add rsp, 0x30
L02dc: pop rbx
L02dd: pop rbp
L02de: pop rsi
L02df: pop rdi
L02e0: pop r14
L02e2: ret
L02e3: test rdi, rdi
L02e6: je short L02f8
L02e8: mov eax, 0xffffffff
L02ed: add rsp, 0x30
L02f1: pop rbx
L02f2: pop rbp
L02f3: pop rsi
L02f4: pop rdi
L02f5: pop r14
L02f7: ret
L02f8: xor eax, eax
L02fa: add rsp, 0x30
L02fe: pop rbx
L02ff: pop rbp
L0300: pop rsi
L0301: pop rdi
L0302: pop r14
L0304: ret
L0305: add rsp, 0x30
L0309: pop rbx
L030a: pop rbp
L030b: pop rsi
L030c: pop rdi
L030d: pop r14
L030f: ret

;Test+Test+LinearExpr.CompareTo(System.Object)
L0000: push rsi
L0001: sub rsp, 0x20
L0005: mov rsi, rcx
L0008: mov rax, rdx
L000b: test rax, rax
L000e: je short L0024
L0010: mov rcx, 0x7ff953af0bf0
L001a: cmp [rax], rcx
L001d: je short L0024
L001f: call System.Runtime.CompilerServices.CastHelpers.ChkCastClassSpecial(Void*, System.Object)
L0024: mov rcx, rsi
L0027: mov rdx, rax
L002a: cmp [rcx], ecx
L002c: add rsp, 0x20
L0030: pop rsi
L0031: jmp Test+Test+LinearExpr.CompareTo(LinearExpr)

;Test+Test+LinearExpr.CompareTo(System.Object, System.Collections.IComparer)
L0000: push r14
L0002: push rdi
L0003: push rsi
L0004: push rbp
L0005: push rbx
L0006: sub rsp, 0x30
L000a: vzeroupper
L000d: xor eax, eax
L000f: mov [rsp+0x20], rax
L0014: mov [rsp+0x28], rax
L0019: mov rsi, rdx
L001c: mov rdi, r8
L001f: mov rbx, rcx
L0022: mov rbp, rsi
L0025: test rbp, rbp
L0028: je short L0045
L002a: mov rcx, 0x7ff953af0bf0
L0034: cmp [rbp], rcx
L0038: je short L0045
L003a: mov rdx, rsi
L003d: call System.Runtime.CompilerServices.CastHelpers.ChkCastClassSpecial(Void*, System.Object)
L0042: mov rbp, rax
L0045: test rbx, rbx
L0048: je L02d5
L004e: test rbp, rbp
L0051: je short L006b
L0053: mov rcx, 0x7ff953af0bf0
L005d: cmp [rbp], rcx
L0061: je short L006b
L0063: mov rdx, rbp
L0066: call System.Runtime.CompilerServices.CastHelpers.ChkCastClassSpecial(Void*, System.Object)
L006b: test rbp, rbp
L006e: je L02c5
L0074: mov r14d, [rbx+8]
L0078: mov eax, r14d
L007b: mov esi, [rbp+8]
L007e: cmp eax, esi
L0080: jne L02c1
L0086: cmp r14d, 3
L008a: ja L01ad
L0090: mov ecx, r14d
L0093: lea rdx, [Test+Test+LinearExpr.CompareTo(System.Object, System.Collections.IComparer)]
L009a: mov edx, [rdx+rcx*4]
L009d: lea rax, [L001f]
L00a4: add rdx, rax
L00a7: jmp rdx
L00a9: mov rsi, rbx
L00ac: mov rcx, 0x7ff953af1320
L00b6: cmp [rsi], rcx
L00b9: je short L00c6
L00bb: mov rdx, rbx
L00be: call System.Runtime.CompilerServices.CastHelpers.ChkCastClassSpecial(Void*, System.Object)
L00c3: mov rsi, rax
L00c6: mov rbx, rbp
L00c9: mov rcx, 0x7ff953af1320
L00d3: cmp [rbx], rcx
L00d6: je short L00e3
L00d8: mov rdx, rbp
L00db: call System.Runtime.CompilerServices.CastHelpers.ChkCastClassSpecial(Void*, System.Object)
L00e0: mov rbx, rax
L00e3: vmovsd xmm1, [rsi+0x18]
L00e8: vmovsd xmm2, [rbx+0x18]
L00ed: vucomisd xmm2, xmm1
L00f1: jbe short L00fa
L00f3: mov eax, 0xffffffff
L00f8: jmp short L011b
L00fa: vucomisd xmm1, xmm2
L00fe: jbe short L0107
L0100: mov eax, 1
L0105: jmp short L011b
L0107: vucomisd xmm1, xmm2
L010b: jp short L0113
L010d: jne short L0113
L010f: xor eax, eax
L0111: jmp short L011b
L0113: mov rcx, rdi
L0116: call Microsoft.FSharp.Core.LanguagePrimitives+HashCompare.GenericComparisonWithComparerIntrinsic[[System.Double, System.Private.CoreLib]](System.Collections.IComparer, Double, Double)
L011b: test eax, eax
L011d: jl L02b9
L0123: test eax, eax
L0125: jg L02bb
L012b: mov rcx, [rsi+0x10]
L012f: mov rdx, [rbx+0x10]
L0133: mov rsi, rdx
L0136: mov rbx, rcx
L0139: jmp L0022
L013e: mov rsi, rbx
L0141: mov rcx, 0x7ff953af14c8
L014b: cmp [rsi], rcx
L014e: je short L015b
L0150: mov rdx, rbx
L0153: call System.Runtime.CompilerServices.CastHelpers.ChkCastClassSpecial(Void*, System.Object)
L0158: mov rsi, rax
L015b: mov rbx, rbp
L015e: mov rcx, 0x7ff953af14c8
L0168: cmp [rbx], rcx
L016b: je short L0178
L016d: mov rdx, rbp
L0170: call System.Runtime.CompilerServices.CastHelpers.ChkCastClassSpecial(Void*, System.Object)
L0175: mov rbx, rax
L0178: mov rcx, [rsi+0x10]
L017c: mov rdx, [rbx+0x10]
L0180: mov r8, rdi
L0183: cmp [rcx], ecx
L0185: call Test+Test+LinearExpr.CompareTo(System.Object, System.Collections.IComparer)
L018a: test eax, eax
L018c: jl L02bd
L0192: test eax, eax
L0194: jg L02bf
L019a: mov rcx, [rsi+0x18]
L019e: mov rdx, [rbx+0x18]
L01a2: mov rsi, rdx
L01a5: mov rbx, rcx
L01a8: jmp L0022
L01ad: mov rsi, rbx
L01b0: mov rcx, 0x7ff953af0ff8
L01ba: cmp [rsi], rcx
L01bd: je short L01ca
L01bf: mov rdx, rbx
L01c2: call System.Runtime.CompilerServices.CastHelpers.ChkCastClassSpecial(Void*, System.Object)
L01c7: mov rsi, rax
L01ca: mov rax, rbp
L01cd: mov rcx, 0x7ff953af0ff8
L01d7: cmp [rax], rcx
L01da: je short L01e4
L01dc: mov rdx, rbp
L01df: call System.Runtime.CompilerServices.CastHelpers.ChkCastClassSpecial(Void*, System.Object)
L01e4: vmovsd xmm1, [rsi+0x10]
L01e9: vmovsd xmm2, [rax+0x10]
L01ee: vucomisd xmm2, xmm1
L01f2: ja L02f9
L01f8: vucomisd xmm1, xmm2
L01fc: ja L02c5
L0202: vucomisd xmm1, xmm2
L0206: jp short L020e
L0208: je L0309
L020e: mov rcx, rdi
L0211: add rsp, 0x30
L0215: pop rbx
L0216: pop rbp
L0217: pop rsi
L0218: pop rdi
L0219: pop r14
L021b: jmp Microsoft.FSharp.Core.LanguagePrimitives+HashCompare.GenericComparisonWithComparerIntrinsic[[System.Double, System.Private.CoreLib]](System.Collections.IComparer, Double, Double)
L0220: mov rsi, rbx
L0223: mov rcx, 0x7ff953af1178
L022d: cmp [rsi], rcx
L0230: je short L023d
L0232: mov rdx, rbx
L0235: call System.Runtime.CompilerServices.CastHelpers.ChkCastClassSpecial(Void*, System.Object)
L023a: mov rsi, rax
L023d: mov rbx, rbp
L0240: mov rcx, 0x7ff953af1178
L024a: cmp [rbx], rcx
L024d: je short L025a
L024f: mov rdx, rbp
L0252: call System.Runtime.CompilerServices.CastHelpers.ChkCastClassSpecial(Void*, System.Object)
L0257: mov rbx, rax
L025a: add rsi, 0x10
L025e: mov rcx, [rsi]
L0261: mov [rsp+0x20], rcx
L0266: mov rcx, [rsi+8]
L026a: mov [rsp+0x28], rcx
L026f: add rbx, 0x10
L0273: mov rsi, [rbx]
L0276: mov rbx, [rbx+8]
L027a: mov rcx, 0x7ff953af0538
L0284: call 0x00007ff9b182a470
L0289: mov rbp, rax
L028c: lea r14, [rbp+8]
L0290: mov rcx, r14
L0293: mov rdx, rsi
L0296: call 0x00007ff9b182a050
L029b: lea rcx, [r14+8]
L029f: mov rdx, rbx
L02a2: call 0x00007ff9b182a050
L02a7: mov rdx, rbp
L02aa: lea rcx, [rsp+0x20]
L02af: mov r8, rdi
L02b2: call Test+Test+Decision.CompareTo(System.Object, System.Collections.IComparer)
L02b7: jmp short L0316
L02b9: jmp short L0316
L02bb: jmp short L0316
L02bd: jmp short L0316
L02bf: jmp short L0316
L02c1: sub eax, esi
L02c3: jmp short L0316
L02c5: mov eax, 1
L02ca: add rsp, 0x30
L02ce: pop rbx
L02cf: pop rbp
L02d0: pop rsi
L02d1: pop rdi
L02d2: pop r14
L02d4: ret
L02d5: mov rax, rsi
L02d8: test rax, rax
L02db: je short L02f4
L02dd: mov rcx, 0x7ff953af0bf0
L02e7: cmp [rax], rcx
L02ea: je short L02f4
L02ec: mov rdx, rsi
L02ef: call System.Runtime.CompilerServices.CastHelpers.ChkCastClassSpecial(Void*, System.Object)
L02f4: test rax, rax
L02f7: je short L0309
L02f9: mov eax, 0xffffffff
L02fe: add rsp, 0x30
L0302: pop rbx
L0303: pop rbp
L0304: pop rsi
L0305: pop rdi
L0306: pop r14
L0308: ret
L0309: xor eax, eax
L030b: add rsp, 0x30
L030f: pop rbx
L0310: pop rbp
L0311: pop rsi
L0312: pop rdi
L0313: pop r14
L0315: ret
L0316: add rsp, 0x30
L031a: pop rbx
L031b: pop rbp
L031c: pop rsi
L031d: pop rdi
L031e: pop r14
L0320: ret

;Test+Test+LinearExpr.GetHashCode(System.Collections.IEqualityComparer)
L0000: push rdi
L0001: push rsi
L0002: push rbp
L0003: push rbx
L0004: sub rsp, 0x38
L0008: vzeroupper
L000b: xor eax, eax
L000d: mov [rsp+0x28], rax
L0012: mov [rsp+0x30], rax
L0017: mov rsi, rcx
L001a: mov rdi, rdx
L001d: test rsi, rsi
L0020: je L017b
L0026: mov ebx, [rsi+8]
L0029: cmp ebx, 3
L002c: ja short L0046
L002e: mov ecx, ebx
L0030: lea rdx, [Test+Test+LinearExpr.GetHashCode(System.Collections.IEqualityComparer)]
L0037: mov edx, [rdx+rcx*4]
L003a: lea rax, [L001d]
L0041: add rdx, rax
L0044: jmp rdx
L0046: mov rcx, rsi
L0049: mov rdx, 0x7ff953af0ff8
L0053: cmp [rcx], rdx
L0056: je short L0063
L0058: mov rcx, rdx
L005b: mov rdx, rsi
L005e: call System.Runtime.CompilerServices.CastHelpers.ChkCastClassSpecial(Void*, System.Object)
L0063: vmovsd xmm1, [rsi+0x10]
L0068: mov rcx, rdi
L006b: call Microsoft.FSharp.Core.LanguagePrimitives+HashCompare.GenericHashWithComparerIntrinsic[[System.Double, System.Private.CoreLib]](System.Collections.IEqualityComparer, Double)
L0070: lea ebp, [rax-0x61c88647]
L0076: mov eax, ebp
L0078: jmp L0172
L007d: mov rcx, rsi
L0080: mov rdx, 0x7ff953af1178
L008a: cmp [rcx], rdx
L008d: je short L009a
L008f: mov rcx, rdx
L0092: mov rdx, rsi
L0095: call System.Runtime.CompilerServices.CastHelpers.ChkCastClassSpecial(Void*, System.Object)
L009a: add rsi, 0x10
L009e: mov rcx, [rsi]
L00a1: mov [rsp+0x28], rcx
L00a6: mov rcx, [rsi+8]
L00aa: mov [rsp+0x30], rcx
L00af: lea rcx, [rsp+0x28]
L00b4: mov rdx, rdi
L00b7: call Test+Test+Decision.GetHashCode(System.Collections.IEqualityComparer)
L00bc: lea ebp, [rax-0x61c88607]
L00c2: mov eax, ebp
L00c4: jmp L0172
L00c9: mov rcx, rsi
L00cc: mov rdx, 0x7ff953af1320
L00d6: cmp [rcx], rdx
L00d9: je short L00e6
L00db: mov rcx, rdx
L00de: mov rdx, rsi
L00e1: call System.Runtime.CompilerServices.CastHelpers.ChkCastClassSpecial(Void*, System.Object)
L00e6: mov rcx, [rsi+0x10]
L00ea: mov rdx, rdi
L00ed: cmp [rcx], ecx
L00ef: call Test+Test+LinearExpr.GetHashCode(System.Collections.IEqualityComparer)
L00f4: lea ebp, [rax-0x61c885c7]
L00fa: vmovsd xmm1, [rsi+0x18]
L00ff: mov rcx, rdi
L0102: call Microsoft.FSharp.Core.LanguagePrimitives+HashCompare.GenericHashWithComparerIntrinsic[[System.Double, System.Private.CoreLib]](System.Collections.IEqualityComparer, Double)
L0107: mov ecx, ebp
L0109: shl ecx, 6
L010c: add ecx, eax
L010e: mov edx, ebp
L0110: sar edx, 2
L0113: lea ebp, [rcx+rdx-0x61c88647]
L011a: mov eax, ebp
L011c: jmp short L0172
L011e: mov rcx, rsi
L0121: mov rdx, 0x7ff953af14c8
L012b: cmp [rcx], rdx
L012e: je short L013b
L0130: mov rcx, rdx
L0133: mov rdx, rsi
L0136: call System.Runtime.CompilerServices.CastHelpers.ChkCastClassSpecial(Void*, System.Object)
L013b: mov rcx, [rsi+0x18]
L013f: mov rdx, rdi
L0142: cmp [rcx], ecx
L0144: call Test+Test+LinearExpr.GetHashCode(System.Collections.IEqualityComparer)
L0149: lea ebp, [rax-0x61c88587]
L014f: mov rcx, [rsi+0x10]
L0153: mov rdx, rdi
L0156: cmp [rcx], ecx
L0158: call Test+Test+LinearExpr.GetHashCode(System.Collections.IEqualityComparer)
L015d: mov edx, ebp
L015f: shl edx, 6
L0162: add eax, edx
L0164: mov edx, ebp
L0166: sar edx, 2
L0169: lea ebp, [rax+rdx-0x61c88647]
L0170: mov eax, ebp
L0172: add rsp, 0x38
L0176: pop rbx
L0177: pop rbp
L0178: pop rsi
L0179: pop rdi
L017a: ret
L017b: xor eax, eax
L017d: add rsp, 0x38
L0181: pop rbx
L0182: pop rbp
L0183: pop rsi
L0184: pop rdi
L0185: ret

;Test+Test+LinearExpr.GetHashCode()
L0000: mov rdx, 0x191f2a86fa8
L000a: mov rdx, [rdx]
L000d: cmp [rcx], ecx
L000f: jmp Test+Test+LinearExpr.GetHashCode(System.Collections.IEqualityComparer)

;Test+Test+LinearExpr.Equals(System.Object, System.Collections.IEqualityComparer)
L0000: push r14
L0002: push rdi
L0003: push rsi
L0004: push rbp
L0005: push rbx
L0006: sub rsp, 0x30
L000a: vzeroupper
L000d: xor eax, eax
L000f: mov [rsp+0x20], rax
L0014: mov [rsp+0x28], rax
L0019: mov rsi, r8
L001c: mov rdi, rcx
L001f: test rdi, rdi
L0022: je L023a
L0028: mov rcx, 0x7ff953af0bf0
L0032: call System.Runtime.CompilerServices.CastHelpers.IsInstanceOfClass(Void*, System.Object)
L0037: mov rbx, rax
L003a: test rbx, rbx
L003d: je L022d
L0043: mov ebp, [rdi+8]
L0046: mov ecx, ebp
L0048: mov edx, [rbx+8]
L004b: cmp ecx, edx
L004d: jne L022d
L0053: cmp ebp, 3
L0056: ja L013a
L005c: mov ecx, ebp
L005e: lea rdx, [Test+Test+LinearExpr.Equals(System.Object, System.Collections.IEqualityComparer)]
L0065: mov edx, [rdx+rcx*4]
L0068: lea rax, [L001c]
L006f: add rdx, rax
L0072: jmp rdx
L0074: mov r14, rdi
L0077: mov rcx, 0x7ff953af1320
L0081: cmp [r14], rcx
L0084: je short L0091
L0086: mov rdx, rdi
L0089: call System.Runtime.CompilerServices.CastHelpers.ChkCastClassSpecial(Void*, System.Object)
L008e: mov r14, rax
L0091: mov rdi, rbx
L0094: mov rcx, 0x7ff953af1320
L009e: cmp [rdi], rcx
L00a1: je short L00ae
L00a3: mov rdx, rbx
L00a6: call System.Runtime.CompilerServices.CastHelpers.ChkCastClassSpecial(Void*, System.Object)
L00ab: mov rdi, rax
L00ae: vmovsd xmm0, [r14+0x18]
L00b4: vucomisd xmm0, [rdi+0x18]
L00b9: jp L022d
L00bf: jne L022d
L00c5: mov rcx, [r14+0x10]
L00c9: mov rdx, [rdi+0x10]
L00cd: mov rdi, rcx
L00d0: jmp L001f
L00d5: mov rbp, rdi
L00d8: mov rcx, 0x7ff953af14c8
L00e2: cmp [rbp], rcx
L00e6: je short L00f3
L00e8: mov rdx, rdi
L00eb: call System.Runtime.CompilerServices.CastHelpers.ChkCastClassSpecial(Void*, System.Object)
L00f0: mov rbp, rax
L00f3: mov rdi, rbx
L00f6: mov rcx, 0x7ff953af14c8
L0100: cmp [rdi], rcx
L0103: je short L0110
L0105: mov rdx, rbx
L0108: call System.Runtime.CompilerServices.CastHelpers.ChkCastClassSpecial(Void*, System.Object)
L010d: mov rdi, rax
L0110: mov rcx, [rbp+0x10]
L0114: mov rdx, [rdi+0x10]
L0118: mov r8, rsi
L011b: cmp [rcx], ecx
L011d: call Test+Test+LinearExpr.Equals(System.Object, System.Collections.IEqualityComparer)
L0122: test eax, eax
L0124: je L022d
L012a: mov rcx, [rbp+0x18]
L012e: mov rdx, [rdi+0x18]
L0132: mov rdi, rcx
L0135: jmp L001f
L013a: mov rbp, rdi
L013d: mov rcx, 0x7ff953af0ff8
L0147: cmp [rbp], rcx
L014b: je short L0158
L014d: mov rdx, rdi
L0150: call System.Runtime.CompilerServices.CastHelpers.ChkCastClassSpecial(Void*, System.Object)
L0155: mov rbp, rax
L0158: mov rax, rbx
L015b: mov rcx, 0x7ff953af0ff8
L0165: cmp [rax], rcx
L0168: je short L0172
L016a: mov rdx, rbx
L016d: call System.Runtime.CompilerServices.CastHelpers.ChkCastClassSpecial(Void*, System.Object)
L0172: vmovsd xmm0, [rbp+0x10]
L0177: vucomisd xmm0, [rax+0x10]
L017c: setnp r14b
L0180: jp short L0186
L0182: sete r14b
L0186: movzx r14d, r14b
L018a: jmp L0245
L018f: mov rbp, rdi
L0192: mov rcx, 0x7ff953af1178
L019c: cmp [rbp], rcx
L01a0: je short L01ad
L01a2: mov rdx, rdi
L01a5: call System.Runtime.CompilerServices.CastHelpers.ChkCastClassSpecial(Void*, System.Object)
L01aa: mov rbp, rax
L01ad: mov rdi, rbx
L01b0: mov rcx, 0x7ff953af1178
L01ba: cmp [rdi], rcx
L01bd: je short L01ca
L01bf: mov rdx, rbx
L01c2: call System.Runtime.CompilerServices.CastHelpers.ChkCastClassSpecial(Void*, System.Object)
L01c7: mov rdi, rax
L01ca: add rbp, 0x10
L01ce: mov rcx, [rbp]
L01d2: mov [rsp+0x20], rcx
L01d7: mov rcx, [rbp+8]
L01db: mov [rsp+0x28], rcx
L01e0: add rdi, 0x10
L01e4: mov rbx, [rdi]
L01e7: mov rdi, [rdi+8]
L01eb: mov rcx, 0x7ff953af0538
L01f5: call 0x00007ff9b182a470
L01fa: mov rbp, rax
L01fd: lea r14, [rbp+8]
L0201: mov rcx, r14
L0204: mov rdx, rbx
L0207: call 0x00007ff9b182a050
L020c: lea rcx, [r14+8]
L0210: mov rdx, rdi
L0213: call 0x00007ff9b182a050
L0218: mov rdx, rbp
L021b: lea rcx, [rsp+0x20]
L0220: mov r8, rsi
L0223: call Test+Test+Decision.Equals(System.Object, System.Collections.IEqualityComparer)
L0228: mov r14d, eax
L022b: jmp short L0245
L022d: xor eax, eax
L022f: add rsp, 0x30
L0233: pop rbx
L0234: pop rbp
L0235: pop rsi
L0236: pop rdi
L0237: pop r14
L0239: ret
L023a: test rdx, rdx
L023d: sete r14b
L0241: movzx r14d, r14b
L0245: movzx eax, r14b
L0249: add rsp, 0x30
L024d: pop rbx
L024e: pop rbp
L024f: pop rsi
L0250: pop rdi
L0251: pop r14
L0253: ret

;Test+Test+LinearExpr.Equals(LinearExpr)
L0000: push rdi
L0001: push rsi
L0002: push rbx
L0003: sub rsp, 0x40
L0007: vzeroupper
L000a: vmovaps [rsp+0x30], xmm6
L0010: vmovaps [rsp+0x20], xmm7
L0016: mov rsi, rdx
L0019: mov rdx, rcx
L001c: test rdx, rdx
L001f: je L022c
L0025: test rsi, rsi
L0028: je L0216
L002e: mov edi, [rdx+8]
L0031: mov ecx, edi
L0033: mov eax, [rsi+8]
L0036: cmp ecx, eax
L0038: jne L0216
L003e: cmp edi, 3
L0041: ja L012e
L0047: mov ecx, edi
L0049: lea rax, [Test+Test+LinearExpr.Equals(LinearExpr)]
L0050: mov eax, [rax+rcx*4]
L0053: lea r8, [L0019]
L005a: add rax, r8
L005d: jmp rax
L005f: mov rbx, rdx
L0062: mov rcx, 0x7ff953af1320
L006c: cmp [rbx], rcx
L006f: je short L0079
L0071: call System.Runtime.CompilerServices.CastHelpers.ChkCastClassSpecial(Void*, System.Object)
L0076: mov rbx, rax
L0079: mov rdi, rsi
L007c: mov rcx, 0x7ff953af1320
L0086: cmp [rdi], rcx
L0089: je short L0096
L008b: mov rdx, rsi
L008e: call System.Runtime.CompilerServices.CastHelpers.ChkCastClassSpecial(Void*, System.Object)
L0093: mov rdi, rax
L0096: vmovsd xmm6, [rbx+0x18]
L009b: vmovsd xmm7, [rdi+0x18]
L00a0: vucomisd xmm6, xmm7
L00a4: jp short L00a8
L00a6: je short L00c6
L00a8: vucomisd xmm6, xmm6
L00ac: jp short L00b4
L00ae: je L0216
L00b4: vucomisd xmm7, xmm7
L00b8: setp cl
L00bb: movzx ecx, cl
L00be: test ecx, ecx
L00c0: je L0216
L00c6: mov rdx, [rbx+0x10]
L00ca: mov rsi, [rdi+0x10]
L00ce: jmp L001c
L00d3: mov rdi, rdx
L00d6: mov rcx, 0x7ff953af14c8
L00e0: cmp [rdi], rcx
L00e3: je short L00ed
L00e5: call System.Runtime.CompilerServices.CastHelpers.ChkCastClassSpecial(Void*, System.Object)
L00ea: mov rdi, rax
L00ed: mov rbx, rsi
L00f0: mov rcx, 0x7ff953af14c8
L00fa: cmp [rbx], rcx
L00fd: je short L010a
L00ff: mov rdx, rsi
L0102: call System.Runtime.CompilerServices.CastHelpers.ChkCastClassSpecial(Void*, System.Object)
L0107: mov rbx, rax
L010a: mov rcx, [rdi+0x10]
L010e: mov rdx, [rbx+0x10]
L0112: cmp [rcx], ecx
L0114: call Test+Test+LinearExpr.Equals(LinearExpr)
L0119: test eax, eax
L011b: je L0216
L0121: mov rdx, [rdi+0x18]
L0125: mov rsi, [rbx+0x18]
L0129: jmp L001c
L012e: mov rdi, rdx
L0131: mov rcx, 0x7ff953af0ff8
L013b: cmp [rdi], rcx
L013e: je short L0148
L0140: call System.Runtime.CompilerServices.CastHelpers.ChkCastClassSpecial(Void*, System.Object)
L0145: mov rdi, rax
L0148: mov rax, rsi
L014b: mov rcx, 0x7ff953af0ff8
L0155: cmp [rax], rcx
L0158: je short L0162
L015a: mov rdx, rsi
L015d: call System.Runtime.CompilerServices.CastHelpers.ChkCastClassSpecial(Void*, System.Object)
L0162: vmovsd xmm6, [rdi+0x10]
L0167: vmovsd xmm7, [rax+0x10]
L016c: vucomisd xmm6, xmm7
L0170: jp short L018d
L0172: jne short L018d
L0174: mov eax, 1
L0179: vmovaps xmm6, [rsp+0x30]
L017f: vmovaps xmm7, [rsp+0x20]
L0185: add rsp, 0x40
L0189: pop rbx
L018a: pop rsi
L018b: pop rdi
L018c: ret
L018d: vucomisd xmm6, xmm6
L0191: jp short L0199
L0193: je L0216
L0199: vucomisd xmm7, xmm7
L019d: setp bl
L01a0: movzx ebx, bl
L01a3: jmp L0235
L01a8: mov rdi, rdx
L01ab: mov rcx, 0x7ff953af1178
L01b5: cmp [rdi], rcx
L01b8: je short L01c2
L01ba: call System.Runtime.CompilerServices.CastHelpers.ChkCastClassSpecial(Void*, System.Object)
L01bf: mov rdi, rax
L01c2: mov rbx, rsi
L01c5: mov rcx, 0x7ff953af1178
L01cf: cmp [rbx], rcx
L01d2: je short L01df
L01d4: mov rdx, rsi
L01d7: call System.Runtime.CompilerServices.CastHelpers.ChkCastClassSpecial(Void*, System.Object)
L01dc: mov rbx, rax
L01df: add rdi, 0x10
L01e3: mov rcx, [rdi]
L01e6: mov rsi, [rdi+8]
L01ea: add rbx, 0x10
L01ee: mov rdx, [rbx]
L01f1: mov rdi, [rbx+8]
L01f5: cmp [rcx], ecx
L01f7: call Test+Test+DecisionName.Equals(DecisionName)
L01fc: test eax, eax
L01fe: je short L0212
L0200: mov rcx, rsi
L0203: mov rdx, rdi
L0206: cmp [rcx], ecx
L0208: call Test+Test+DecisionType.Equals(DecisionType)
L020d: movzx ebx, al
L0210: jmp short L0214
L0212: xor ebx, ebx
L0214: jmp short L0235
L0216: xor eax, eax
L0218: vmovaps xmm6, [rsp+0x30]
L021e: vmovaps xmm7, [rsp+0x20]
L0224: add rsp, 0x40
L0228: pop rbx
L0229: pop rsi
L022a: pop rdi
L022b: ret
L022c: test rsi, rsi
L022f: sete bl
L0232: movzx ebx, bl
L0235: movzx eax, bl
L0238: vmovaps xmm6, [rsp+0x30]
L023e: vmovaps xmm7, [rsp+0x20]
L0244: add rsp, 0x40
L0248: pop rbx
L0249: pop rsi
L024a: pop rdi
L024b: ret

;Test+Test+LinearExpr.Equals(System.Object)
L0000: push rsi
L0001: sub rsp, 0x20
L0005: mov rsi, rcx
L0008: mov rcx, 0x7ff953af0bf0
L0012: call System.Runtime.CompilerServices.CastHelpers.IsInstanceOfClass(Void*, System.Object)
L0017: test rax, rax
L001a: je short L002e
L001c: mov rcx, rsi
L001f: mov rdx, rax
L0022: cmp [rcx], ecx
L0024: add rsp, 0x20
L0028: pop rsi
L0029: jmp Test+Test+LinearExpr.Equals(LinearExpr)
L002e: xor eax, eax
L0030: add rsp, 0x20
L0034: pop rsi
L0035: ret

;Test+Test+LinearExpr+Scale.get_scale()
L0000: vzeroupper
L0003: vmovsd xmm0, [rcx+0x18]
L0008: ret

;Test+Test+LinearExpr+Scale.get_expr()
L0000: mov rax, [rcx+0x10]
L0004: ret

;Test+Test+LinearExpr.get_IsFloat()
L0000: cmp dword ptr [rcx+8], 0
L0004: sete al
L0007: movzx eax, al
L000a: ret

;Test+Test+LinearExpr.get_IsDecision()
L0000: cmp dword ptr [rcx+8], 1
L0004: sete al
L0007: movzx eax, al
L000a: ret

;Test+Test+LinearExpr.get_IsScale()
L0000: cmp dword ptr [rcx+8], 2
L0004: sete al
L0007: movzx eax, al
L000a: ret

;Test+Test+LinearExpr.get_IsAdd()
L0000: cmp dword ptr [rcx+8], 3
L0004: sete al
L0007: movzx eax, al
L000a: ret

;Test+Test+LinearExpr.get_Tag()
L0000: mov eax, [rcx+8]
L0003: ret

;Test+Test+LinearExpr.ToString()
L0000: push rdi
L0001: push rsi
L0002: sub rsp, 0x28
L0006: mov rsi, rcx
L0009: mov rcx, 0x7ff953af84a0
L0013: call 0x00007ff9b182a470
L0018: mov rdi, rax
L001b: mov rdx, 0x191f2a9b3f0
L0025: mov rdx, [rdx]
L0028: lea rcx, [rdi+8]
L002c: call 0x00007ff9b182a080
L0031: xor edx, edx
L0033: mov [rdi+0x10], rdx
L0037: mov [rdi+0x18], rdx
L003b: mov rdx, rdi
L003e: mov rcx, 0x7ff953af8780
L0048: call Microsoft.FSharp.Core.PrintfModule.PrintFormatToStringThen[[System.__Canon, System.Private.CoreLib]](Microsoft.FSharp.Core.PrintfFormat`4<System.__Canon,Microsoft.FSharp.Core.Unit,System.String,System.String>)
L004d: mov rcx, rax
L0050: mov rdx, rsi
L0053: mov rax, [rax]
L0056: mov rax, [rax+0x40]
L005a: mov rax, [rax+0x20]
L005e: add rsp, 0x28
L0062: pop rsi
L0063: pop rdi
L0064: jmp rax

;Test+Test+LinearExpr.CompareTo(LinearExpr)
L0000: push r14
L0002: push rdi
L0003: push rsi
L0004: push rbp
L0005: push rbx
L0006: sub rsp, 0x30
L000a: vzeroupper
L000d: xor eax, eax
L000f: mov [rsp+0x20], rax
L0014: mov [rsp+0x28], rax
L0019: mov rsi, rcx
L001c: mov rdi, rdx
L001f: test rsi, rsi
L0022: je L02e3
L0028: test rdi, rdi
L002b: je L02d3
L0031: mov ebx, [rsi+8]
L0034: mov eax, ebx
L0036: mov ebp, [rdi+8]
L0039: cmp eax, ebp
L003b: jne L02cf
L0041: cmp ebx, 3
L0044: ja short L005e
L0046: mov ecx, ebx
L0048: lea rdx, [Test+Test+LinearExpr.CompareTo(LinearExpr)]
L004f: mov edx, [rdx+rcx*4]
L0052: lea rax, [L001f]
L0059: add rdx, rax
L005c: jmp rdx
L005e: mov rcx, rsi
L0061: mov rdx, 0x7ff953af0ff8
L006b: cmp [rcx], rdx
L006e: je short L007b
L0070: mov rcx, rdx
L0073: mov rdx, rsi
L0076: call System.Runtime.CompilerServices.CastHelpers.ChkCastClassSpecial(Void*, System.Object)
L007b: mov rax, rdi
L007e: mov rcx, 0x7ff953af0ff8
L0088: cmp [rax], rcx
L008b: je short L0095
L008d: mov rdx, rdi
L0090: call System.Runtime.CompilerServices.CastHelpers.ChkCastClassSpecial(Void*, System.Object)
L0095: mov rcx, 0x191f2a86fa0
L009f: mov r14, [rcx]
L00a2: vmovsd xmm1, [rsi+0x10]
L00a7: vmovsd xmm2, [rax+0x10]
L00ac: vucomisd xmm2, xmm1
L00b0: ja L02e8
L00b6: vucomisd xmm1, xmm2
L00ba: ja L02d3
L00c0: vucomisd xmm1, xmm2
L00c4: jp short L00cc
L00c6: je L02f8
L00cc: mov rcx, r14
L00cf: add rsp, 0x30
L00d3: pop rbx
L00d4: pop rbp
L00d5: pop rsi
L00d6: pop rdi
L00d7: pop r14
L00d9: jmp Microsoft.FSharp.Core.LanguagePrimitives+HashCompare.GenericComparisonWithComparerIntrinsic[[System.Double, System.Private.CoreLib]](System.Collections.IComparer, Double, Double)
L00de: mov rcx, rsi
L00e1: mov rdx, 0x7ff953af1178
L00eb: cmp [rcx], rdx
L00ee: je short L00fb
L00f0: mov rcx, rdx
L00f3: mov rdx, rsi
L00f6: call System.Runtime.CompilerServices.CastHelpers.ChkCastClassSpecial(Void*, System.Object)
L00fb: mov rbx, rdi
L00fe: mov rcx, 0x7ff953af1178
L0108: cmp [rbx], rcx
L010b: je short L0118
L010d: mov rdx, rdi
L0110: call System.Runtime.CompilerServices.CastHelpers.ChkCastClassSpecial(Void*, System.Object)
L0115: mov rbx, rax
L0118: mov rcx, 0x191f2a86fa0
L0122: mov r14, [rcx]
L0125: add rsi, 0x10
L0129: mov rcx, [rsi]
L012c: mov [rsp+0x20], rcx
L0131: mov rcx, [rsi+8]
L0135: mov [rsp+0x28], rcx
L013a: add rbx, 0x10
L013e: mov rdi, [rbx]
L0141: mov rsi, [rbx+8]
L0145: mov rcx, 0x7ff953af0538
L014f: call 0x00007ff9b182a470
L0154: mov rbx, rax
L0157: lea rbp, [rbx+8]
L015b: mov rcx, rbp
L015e: mov rdx, rdi
L0161: call 0x00007ff9b182a050
L0166: lea rcx, [rbp+8]
L016a: mov rdx, rsi
L016d: call 0x00007ff9b182a050
L0172: mov rdx, rbx
L0175: lea rcx, [rsp+0x20]
L017a: mov r8, r14
L017d: call Test+Test+Decision.CompareTo(System.Object, System.Collections.IComparer)
L0182: jmp L0305
L0187: mov rcx, rsi
L018a: mov rdx, 0x7ff953af1320
L0194: cmp [rcx], rdx
L0197: je short L01a4
L0199: mov rcx, rdx
L019c: mov rdx, rsi
L019f: call System.Runtime.CompilerServices.CastHelpers.ChkCastClassSpecial(Void*, System.Object)
L01a4: mov rbx, rdi
L01a7: mov rcx, 0x7ff953af1320
L01b1: cmp [rbx], rcx
L01b4: je short L01c1
L01b6: mov rdx, rdi
L01b9: call System.Runtime.CompilerServices.CastHelpers.ChkCastClassSpecial(Void*, System.Object)
L01be: mov rbx, rax
L01c1: mov rcx, 0x191f2a86fa0
L01cb: mov r14, [rcx]
L01ce: vmovsd xmm1, [rsi+0x18]
L01d3: vmovsd xmm2, [rbx+0x18]
L01d8: vucomisd xmm2, xmm1
L01dc: jbe short L01e5
L01de: mov eax, 0xffffffff
L01e3: jmp short L0206
L01e5: vucomisd xmm1, xmm2
L01e9: jbe short L01f2
L01eb: mov eax, 1
L01f0: jmp short L0206
L01f2: vucomisd xmm1, xmm2
L01f6: jp short L01fe
L01f8: jne short L01fe
L01fa: xor eax, eax
L01fc: jmp short L0206
L01fe: mov rcx, r14
L0201: call Microsoft.FSharp.Core.LanguagePrimitives+HashCompare.GenericComparisonWithComparerIntrinsic[[System.Double, System.Private.CoreLib]](System.Collections.IComparer, Double, Double)
L0206: test eax, eax
L0208: jge short L020f
L020a: jmp L0305
L020f: test eax, eax
L0211: jle short L0218
L0213: jmp L0305
L0218: mov rcx, 0x191f2a86fa0
L0222: mov r14, [rcx]
L0225: mov rcx, [rsi+0x10]
L0229: mov rdx, [rbx+0x10]
L022d: mov r8, r14
L0230: cmp [rcx], ecx
L0232: add rsp, 0x30
L0236: pop rbx
L0237: pop rbp
L0238: pop rsi
L0239: pop rdi
L023a: pop r14
L023c: jmp Test+Test+LinearExpr.CompareTo(System.Object, System.Collections.IComparer)
L0241: mov rcx, rsi
L0244: mov rdx, 0x7ff953af14c8
L024e: cmp [rcx], rdx
L0251: je short L025e
L0253: mov rcx, rdx
L0256: mov rdx, rsi
L0259: call System.Runtime.CompilerServices.CastHelpers.ChkCastClassSpecial(Void*, System.Object)
L025e: mov rbx, rdi
L0261: mov rcx, 0x7ff953af14c8
L026b: cmp [rbx], rcx
L026e: je short L027b
L0270: mov rdx, rdi
L0273: call System.Runtime.CompilerServices.CastHelpers.ChkCastClassSpecial(Void*, System.Object)
L0278: mov rbx, rax
L027b: mov rcx, 0x191f2a86fa0
L0285: mov r14, [rcx]
L0288: mov rcx, [rsi+0x10]
L028c: mov rdx, [rbx+0x10]
L0290: mov r8, r14
L0293: cmp [rcx], ecx
L0295: call Test+Test+LinearExpr.CompareTo(System.Object, System.Collections.IComparer)
L029a: test eax, eax
L029c: jge short L02a0
L029e: jmp short L0305
L02a0: test eax, eax
L02a2: jle short L02a6
L02a4: jmp short L0305
L02a6: mov rcx, 0x191f2a86fa0
L02b0: mov r14, [rcx]
L02b3: mov rcx, [rsi+0x18]
L02b7: mov rdx, [rbx+0x18]
L02bb: mov r8, r14
L02be: cmp [rcx], ecx
L02c0: add rsp, 0x30
L02c4: pop rbx
L02c5: pop rbp
L02c6: pop rsi
L02c7: pop rdi
L02c8: pop r14
L02ca: jmp Test+Test+LinearExpr.CompareTo(System.Object, System.Collections.IComparer)
L02cf: sub eax, ebp
L02d1: jmp short L0305
L02d3: mov eax, 1
L02d8: add rsp, 0x30
L02dc: pop rbx
L02dd: pop rbp
L02de: pop rsi
L02df: pop rdi
L02e0: pop r14
L02e2: ret
L02e3: test rdi, rdi
L02e6: je short L02f8
L02e8: mov eax, 0xffffffff
L02ed: add rsp, 0x30
L02f1: pop rbx
L02f2: pop rbp
L02f3: pop rsi
L02f4: pop rdi
L02f5: pop r14
L02f7: ret
L02f8: xor eax, eax
L02fa: add rsp, 0x30
L02fe: pop rbx
L02ff: pop rbp
L0300: pop rsi
L0301: pop rdi
L0302: pop r14
L0304: ret
L0305: add rsp, 0x30
L0309: pop rbx
L030a: pop rbp
L030b: pop rsi
L030c: pop rdi
L030d: pop r14
L030f: ret

;Test+Test+LinearExpr.CompareTo(System.Object)
L0000: push rsi
L0001: sub rsp, 0x20
L0005: mov rsi, rcx
L0008: mov rax, rdx
L000b: test rax, rax
L000e: je short L0024
L0010: mov rcx, 0x7ff953af0bf0
L001a: cmp [rax], rcx
L001d: je short L0024
L001f: call System.Runtime.CompilerServices.CastHelpers.ChkCastClassSpecial(Void*, System.Object)
L0024: mov rcx, rsi
L0027: mov rdx, rax
L002a: cmp [rcx], ecx
L002c: add rsp, 0x20
L0030: pop rsi
L0031: jmp Test+Test+LinearExpr.CompareTo(LinearExpr)

;Test+Test+LinearExpr.CompareTo(System.Object, System.Collections.IComparer)
L0000: push r14
L0002: push rdi
L0003: push rsi
L0004: push rbp
L0005: push rbx
L0006: sub rsp, 0x30
L000a: vzeroupper
L000d: xor eax, eax
L000f: mov [rsp+0x20], rax
L0014: mov [rsp+0x28], rax
L0019: mov rsi, rdx
L001c: mov rdi, r8
L001f: mov rbx, rcx
L0022: mov rbp, rsi
L0025: test rbp, rbp
L0028: je short L0045
L002a: mov rcx, 0x7ff953af0bf0
L0034: cmp [rbp], rcx
L0038: je short L0045
L003a: mov rdx, rsi
L003d: call System.Runtime.CompilerServices.CastHelpers.ChkCastClassSpecial(Void*, System.Object)
L0042: mov rbp, rax
L0045: test rbx, rbx
L0048: je L02d5
L004e: test rbp, rbp
L0051: je short L006b
L0053: mov rcx, 0x7ff953af0bf0
L005d: cmp [rbp], rcx
L0061: je short L006b
L0063: mov rdx, rbp
L0066: call System.Runtime.CompilerServices.CastHelpers.ChkCastClassSpecial(Void*, System.Object)
L006b: test rbp, rbp
L006e: je L02c5
L0074: mov r14d, [rbx+8]
L0078: mov eax, r14d
L007b: mov esi, [rbp+8]
L007e: cmp eax, esi
L0080: jne L02c1
L0086: cmp r14d, 3
L008a: ja L01ad
L0090: mov ecx, r14d
L0093: lea rdx, [Test+Test+LinearExpr.CompareTo(System.Object, System.Collections.IComparer)]
L009a: mov edx, [rdx+rcx*4]
L009d: lea rax, [L001f]
L00a4: add rdx, rax
L00a7: jmp rdx
L00a9: mov rsi, rbx
L00ac: mov rcx, 0x7ff953af1320
L00b6: cmp [rsi], rcx
L00b9: je short L00c6
L00bb: mov rdx, rbx
L00be: call System.Runtime.CompilerServices.CastHelpers.ChkCastClassSpecial(Void*, System.Object)
L00c3: mov rsi, rax
L00c6: mov rbx, rbp
L00c9: mov rcx, 0x7ff953af1320
L00d3: cmp [rbx], rcx
L00d6: je short L00e3
L00d8: mov rdx, rbp
L00db: call System.Runtime.CompilerServices.CastHelpers.ChkCastClassSpecial(Void*, System.Object)
L00e0: mov rbx, rax
L00e3: vmovsd xmm1, [rsi+0x18]
L00e8: vmovsd xmm2, [rbx+0x18]
L00ed: vucomisd xmm2, xmm1
L00f1: jbe short L00fa
L00f3: mov eax, 0xffffffff
L00f8: jmp short L011b
L00fa: vucomisd xmm1, xmm2
L00fe: jbe short L0107
L0100: mov eax, 1
L0105: jmp short L011b
L0107: vucomisd xmm1, xmm2
L010b: jp short L0113
L010d: jne short L0113
L010f: xor eax, eax
L0111: jmp short L011b
L0113: mov rcx, rdi
L0116: call Microsoft.FSharp.Core.LanguagePrimitives+HashCompare.GenericComparisonWithComparerIntrinsic[[System.Double, System.Private.CoreLib]](System.Collections.IComparer, Double, Double)
L011b: test eax, eax
L011d: jl L02b9
L0123: test eax, eax
L0125: jg L02bb
L012b: mov rcx, [rsi+0x10]
L012f: mov rdx, [rbx+0x10]
L0133: mov rsi, rdx
L0136: mov rbx, rcx
L0139: jmp L0022
L013e: mov rsi, rbx
L0141: mov rcx, 0x7ff953af14c8
L014b: cmp [rsi], rcx
L014e: je short L015b
L0150: mov rdx, rbx
L0153: call System.Runtime.CompilerServices.CastHelpers.ChkCastClassSpecial(Void*, System.Object)
L0158: mov rsi, rax
L015b: mov rbx, rbp
L015e: mov rcx, 0x7ff953af14c8
L0168: cmp [rbx], rcx
L016b: je short L0178
L016d: mov rdx, rbp
L0170: call System.Runtime.CompilerServices.CastHelpers.ChkCastClassSpecial(Void*, System.Object)
L0175: mov rbx, rax
L0178: mov rcx, [rsi+0x10]
L017c: mov rdx, [rbx+0x10]
L0180: mov r8, rdi
L0183: cmp [rcx], ecx
L0185: call Test+Test+LinearExpr.CompareTo(System.Object, System.Collections.IComparer)
L018a: test eax, eax
L018c: jl L02bd
L0192: test eax, eax
L0194: jg L02bf
L019a: mov rcx, [rsi+0x18]
L019e: mov rdx, [rbx+0x18]
L01a2: mov rsi, rdx
L01a5: mov rbx, rcx
L01a8: jmp L0022
L01ad: mov rsi, rbx
L01b0: mov rcx, 0x7ff953af0ff8
L01ba: cmp [rsi], rcx
L01bd: je short L01ca
L01bf: mov rdx, rbx
L01c2: call System.Runtime.CompilerServices.CastHelpers.ChkCastClassSpecial(Void*, System.Object)
L01c7: mov rsi, rax
L01ca: mov rax, rbp
L01cd: mov rcx, 0x7ff953af0ff8
L01d7: cmp [rax], rcx
L01da: je short L01e4
L01dc: mov rdx, rbp
L01df: call System.Runtime.CompilerServices.CastHelpers.ChkCastClassSpecial(Void*, System.Object)
L01e4: vmovsd xmm1, [rsi+0x10]
L01e9: vmovsd xmm2, [rax+0x10]
L01ee: vucomisd xmm2, xmm1
L01f2: ja L02f9
L01f8: vucomisd xmm1, xmm2
L01fc: ja L02c5
L0202: vucomisd xmm1, xmm2
L0206: jp short L020e
L0208: je L0309
L020e: mov rcx, rdi
L0211: add rsp, 0x30
L0215: pop rbx
L0216: pop rbp
L0217: pop rsi
L0218: pop rdi
L0219: pop r14
L021b: jmp Microsoft.FSharp.Core.LanguagePrimitives+HashCompare.GenericComparisonWithComparerIntrinsic[[System.Double, System.Private.CoreLib]](System.Collections.IComparer, Double, Double)
L0220: mov rsi, rbx
L0223: mov rcx, 0x7ff953af1178
L022d: cmp [rsi], rcx
L0230: je short L023d
L0232: mov rdx, rbx
L0235: call System.Runtime.CompilerServices.CastHelpers.ChkCastClassSpecial(Void*, System.Object)
L023a: mov rsi, rax
L023d: mov rbx, rbp
L0240: mov rcx, 0x7ff953af1178
L024a: cmp [rbx], rcx
L024d: je short L025a
L024f: mov rdx, rbp
L0252: call System.Runtime.CompilerServices.CastHelpers.ChkCastClassSpecial(Void*, System.Object)
L0257: mov rbx, rax
L025a: add rsi, 0x10
L025e: mov rcx, [rsi]
L0261: mov [rsp+0x20], rcx
L0266: mov rcx, [rsi+8]
L026a: mov [rsp+0x28], rcx
L026f: add rbx, 0x10
L0273: mov rsi, [rbx]
L0276: mov rbx, [rbx+8]
L027a: mov rcx, 0x7ff953af0538
L0284: call 0x00007ff9b182a470
L0289: mov rbp, rax
L028c: lea r14, [rbp+8]
L0290: mov rcx, r14
L0293: mov rdx, rsi
L0296: call 0x00007ff9b182a050
L029b: lea rcx, [r14+8]
L029f: mov rdx, rbx
L02a2: call 0x00007ff9b182a050
L02a7: mov rdx, rbp
L02aa: lea rcx, [rsp+0x20]
L02af: mov r8, rdi
L02b2: call Test+Test+Decision.CompareTo(System.Object, System.Collections.IComparer)
L02b7: jmp short L0316
L02b9: jmp short L0316
L02bb: jmp short L0316
L02bd: jmp short L0316
L02bf: jmp short L0316
L02c1: sub eax, esi
L02c3: jmp short L0316
L02c5: mov eax, 1
L02ca: add rsp, 0x30
L02ce: pop rbx
L02cf: pop rbp
L02d0: pop rsi
L02d1: pop rdi
L02d2: pop r14
L02d4: ret
L02d5: mov rax, rsi
L02d8: test rax, rax
L02db: je short L02f4
L02dd: mov rcx, 0x7ff953af0bf0
L02e7: cmp [rax], rcx
L02ea: je short L02f4
L02ec: mov rdx, rsi
L02ef: call System.Runtime.CompilerServices.CastHelpers.ChkCastClassSpecial(Void*, System.Object)
L02f4: test rax, rax
L02f7: je short L0309
L02f9: mov eax, 0xffffffff
L02fe: add rsp, 0x30
L0302: pop rbx
L0303: pop rbp
L0304: pop rsi
L0305: pop rdi
L0306: pop r14
L0308: ret
L0309: xor eax, eax
L030b: add rsp, 0x30
L030f: pop rbx
L0310: pop rbp
L0311: pop rsi
L0312: pop rdi
L0313: pop r14
L0315: ret
L0316: add rsp, 0x30
L031a: pop rbx
L031b: pop rbp
L031c: pop rsi
L031d: pop rdi
L031e: pop r14
L0320: ret

;Test+Test+LinearExpr.GetHashCode(System.Collections.IEqualityComparer)
L0000: push rdi
L0001: push rsi
L0002: push rbp
L0003: push rbx
L0004: sub rsp, 0x38
L0008: vzeroupper
L000b: xor eax, eax
L000d: mov [rsp+0x28], rax
L0012: mov [rsp+0x30], rax
L0017: mov rsi, rcx
L001a: mov rdi, rdx
L001d: test rsi, rsi
L0020: je L017b
L0026: mov ebx, [rsi+8]
L0029: cmp ebx, 3
L002c: ja short L0046
L002e: mov ecx, ebx
L0030: lea rdx, [Test+Test+LinearExpr.GetHashCode(System.Collections.IEqualityComparer)]
L0037: mov edx, [rdx+rcx*4]
L003a: lea rax, [L001d]
L0041: add rdx, rax
L0044: jmp rdx
L0046: mov rcx, rsi
L0049: mov rdx, 0x7ff953af0ff8
L0053: cmp [rcx], rdx
L0056: je short L0063
L0058: mov rcx, rdx
L005b: mov rdx, rsi
L005e: call System.Runtime.CompilerServices.CastHelpers.ChkCastClassSpecial(Void*, System.Object)
L0063: vmovsd xmm1, [rsi+0x10]
L0068: mov rcx, rdi
L006b: call Microsoft.FSharp.Core.LanguagePrimitives+HashCompare.GenericHashWithComparerIntrinsic[[System.Double, System.Private.CoreLib]](System.Collections.IEqualityComparer, Double)
L0070: lea ebp, [rax-0x61c88647]
L0076: mov eax, ebp
L0078: jmp L0172
L007d: mov rcx, rsi
L0080: mov rdx, 0x7ff953af1178
L008a: cmp [rcx], rdx
L008d: je short L009a
L008f: mov rcx, rdx
L0092: mov rdx, rsi
L0095: call System.Runtime.CompilerServices.CastHelpers.ChkCastClassSpecial(Void*, System.Object)
L009a: add rsi, 0x10
L009e: mov rcx, [rsi]
L00a1: mov [rsp+0x28], rcx
L00a6: mov rcx, [rsi+8]
L00aa: mov [rsp+0x30], rcx
L00af: lea rcx, [rsp+0x28]
L00b4: mov rdx, rdi
L00b7: call Test+Test+Decision.GetHashCode(System.Collections.IEqualityComparer)
L00bc: lea ebp, [rax-0x61c88607]
L00c2: mov eax, ebp
L00c4: jmp L0172
L00c9: mov rcx, rsi
L00cc: mov rdx, 0x7ff953af1320
L00d6: cmp [rcx], rdx
L00d9: je short L00e6
L00db: mov rcx, rdx
L00de: mov rdx, rsi
L00e1: call System.Runtime.CompilerServices.CastHelpers.ChkCastClassSpecial(Void*, System.Object)
L00e6: mov rcx, [rsi+0x10]
L00ea: mov rdx, rdi
L00ed: cmp [rcx], ecx
L00ef: call Test+Test+LinearExpr.GetHashCode(System.Collections.IEqualityComparer)
L00f4: lea ebp, [rax-0x61c885c7]
L00fa: vmovsd xmm1, [rsi+0x18]
L00ff: mov rcx, rdi
L0102: call Microsoft.FSharp.Core.LanguagePrimitives+HashCompare.GenericHashWithComparerIntrinsic[[System.Double, System.Private.CoreLib]](System.Collections.IEqualityComparer, Double)
L0107: mov ecx, ebp
L0109: shl ecx, 6
L010c: add ecx, eax
L010e: mov edx, ebp
L0110: sar edx, 2
L0113: lea ebp, [rcx+rdx-0x61c88647]
L011a: mov eax, ebp
L011c: jmp short L0172
L011e: mov rcx, rsi
L0121: mov rdx, 0x7ff953af14c8
L012b: cmp [rcx], rdx
L012e: je short L013b
L0130: mov rcx, rdx
L0133: mov rdx, rsi
L0136: call System.Runtime.CompilerServices.CastHelpers.ChkCastClassSpecial(Void*, System.Object)
L013b: mov rcx, [rsi+0x18]
L013f: mov rdx, rdi
L0142: cmp [rcx], ecx
L0144: call Test+Test+LinearExpr.GetHashCode(System.Collections.IEqualityComparer)
L0149: lea ebp, [rax-0x61c88587]
L014f: mov rcx, [rsi+0x10]
L0153: mov rdx, rdi
L0156: cmp [rcx], ecx
L0158: call Test+Test+LinearExpr.GetHashCode(System.Collections.IEqualityComparer)
L015d: mov edx, ebp
L015f: shl edx, 6
L0162: add eax, edx
L0164: mov edx, ebp
L0166: sar edx, 2
L0169: lea ebp, [rax+rdx-0x61c88647]
L0170: mov eax, ebp
L0172: add rsp, 0x38
L0176: pop rbx
L0177: pop rbp
L0178: pop rsi
L0179: pop rdi
L017a: ret
L017b: xor eax, eax
L017d: add rsp, 0x38
L0181: pop rbx
L0182: pop rbp
L0183: pop rsi
L0184: pop rdi
L0185: ret

;Test+Test+LinearExpr.GetHashCode()
L0000: mov rdx, 0x191f2a86fa8
L000a: mov rdx, [rdx]
L000d: cmp [rcx], ecx
L000f: jmp Test+Test+LinearExpr.GetHashCode(System.Collections.IEqualityComparer)

;Test+Test+LinearExpr.Equals(System.Object, System.Collections.IEqualityComparer)
L0000: push r14
L0002: push rdi
L0003: push rsi
L0004: push rbp
L0005: push rbx
L0006: sub rsp, 0x30
L000a: vzeroupper
L000d: xor eax, eax
L000f: mov [rsp+0x20], rax
L0014: mov [rsp+0x28], rax
L0019: mov rsi, r8
L001c: mov rdi, rcx
L001f: test rdi, rdi
L0022: je L023a
L0028: mov rcx, 0x7ff953af0bf0
L0032: call System.Runtime.CompilerServices.CastHelpers.IsInstanceOfClass(Void*, System.Object)
L0037: mov rbx, rax
L003a: test rbx, rbx
L003d: je L022d
L0043: mov ebp, [rdi+8]
L0046: mov ecx, ebp
L0048: mov edx, [rbx+8]
L004b: cmp ecx, edx
L004d: jne L022d
L0053: cmp ebp, 3
L0056: ja L013a
L005c: mov ecx, ebp
L005e: lea rdx, [Test+Test+LinearExpr.Equals(System.Object, System.Collections.IEqualityComparer)]
L0065: mov edx, [rdx+rcx*4]
L0068: lea rax, [L001c]
L006f: add rdx, rax
L0072: jmp rdx
L0074: mov r14, rdi
L0077: mov rcx, 0x7ff953af1320
L0081: cmp [r14], rcx
L0084: je short L0091
L0086: mov rdx, rdi
L0089: call System.Runtime.CompilerServices.CastHelpers.ChkCastClassSpecial(Void*, System.Object)
L008e: mov r14, rax
L0091: mov rdi, rbx
L0094: mov rcx, 0x7ff953af1320
L009e: cmp [rdi], rcx
L00a1: je short L00ae
L00a3: mov rdx, rbx
L00a6: call System.Runtime.CompilerServices.CastHelpers.ChkCastClassSpecial(Void*, System.Object)
L00ab: mov rdi, rax
L00ae: vmovsd xmm0, [r14+0x18]
L00b4: vucomisd xmm0, [rdi+0x18]
L00b9: jp L022d
L00bf: jne L022d
L00c5: mov rcx, [r14+0x10]
L00c9: mov rdx, [rdi+0x10]
L00cd: mov rdi, rcx
L00d0: jmp L001f
L00d5: mov rbp, rdi
L00d8: mov rcx, 0x7ff953af14c8
L00e2: cmp [rbp], rcx
L00e6: je short L00f3
L00e8: mov rdx, rdi
L00eb: call System.Runtime.CompilerServices.CastHelpers.ChkCastClassSpecial(Void*, System.Object)
L00f0: mov rbp, rax
L00f3: mov rdi, rbx
L00f6: mov rcx, 0x7ff953af14c8
L0100: cmp [rdi], rcx
L0103: je short L0110
L0105: mov rdx, rbx
L0108: call System.Runtime.CompilerServices.CastHelpers.ChkCastClassSpecial(Void*, System.Object)
L010d: mov rdi, rax
L0110: mov rcx, [rbp+0x10]
L0114: mov rdx, [rdi+0x10]
L0118: mov r8, rsi
L011b: cmp [rcx], ecx
L011d: call Test+Test+LinearExpr.Equals(System.Object, System.Collections.IEqualityComparer)
L0122: test eax, eax
L0124: je L022d
L012a: mov rcx, [rbp+0x18]
L012e: mov rdx, [rdi+0x18]
L0132: mov rdi, rcx
L0135: jmp L001f
L013a: mov rbp, rdi
L013d: mov rcx, 0x7ff953af0ff8
L0147: cmp [rbp], rcx
L014b: je short L0158
L014d: mov rdx, rdi
L0150: call System.Runtime.CompilerServices.CastHelpers.ChkCastClassSpecial(Void*, System.Object)
L0155: mov rbp, rax
L0158: mov rax, rbx
L015b: mov rcx, 0x7ff953af0ff8
L0165: cmp [rax], rcx
L0168: je short L0172
L016a: mov rdx, rbx
L016d: call System.Runtime.CompilerServices.CastHelpers.ChkCastClassSpecial(Void*, System.Object)
L0172: vmovsd xmm0, [rbp+0x10]
L0177: vucomisd xmm0, [rax+0x10]
L017c: setnp r14b
L0180: jp short L0186
L0182: sete r14b
L0186: movzx r14d, r14b
L018a: jmp L0245
L018f: mov rbp, rdi
L0192: mov rcx, 0x7ff953af1178
L019c: cmp [rbp], rcx
L01a0: je short L01ad
L01a2: mov rdx, rdi
L01a5: call System.Runtime.CompilerServices.CastHelpers.ChkCastClassSpecial(Void*, System.Object)
L01aa: mov rbp, rax
L01ad: mov rdi, rbx
L01b0: mov rcx, 0x7ff953af1178
L01ba: cmp [rdi], rcx
L01bd: je short L01ca
L01bf: mov rdx, rbx
L01c2: call System.Runtime.CompilerServices.CastHelpers.ChkCastClassSpecial(Void*, System.Object)
L01c7: mov rdi, rax
L01ca: add rbp, 0x10
L01ce: mov rcx, [rbp]
L01d2: mov [rsp+0x20], rcx
L01d7: mov rcx, [rbp+8]
L01db: mov [rsp+0x28], rcx
L01e0: add rdi, 0x10
L01e4: mov rbx, [rdi]
L01e7: mov rdi, [rdi+8]
L01eb: mov rcx, 0x7ff953af0538
L01f5: call 0x00007ff9b182a470
L01fa: mov rbp, rax
L01fd: lea r14, [rbp+8]
L0201: mov rcx, r14
L0204: mov rdx, rbx
L0207: call 0x00007ff9b182a050
L020c: lea rcx, [r14+8]
L0210: mov rdx, rdi
L0213: call 0x00007ff9b182a050
L0218: mov rdx, rbp
L021b: lea rcx, [rsp+0x20]
L0220: mov r8, rsi
L0223: call Test+Test+Decision.Equals(System.Object, System.Collections.IEqualityComparer)
L0228: mov r14d, eax
L022b: jmp short L0245
L022d: xor eax, eax
L022f: add rsp, 0x30
L0233: pop rbx
L0234: pop rbp
L0235: pop rsi
L0236: pop rdi
L0237: pop r14
L0239: ret
L023a: test rdx, rdx
L023d: sete r14b
L0241: movzx r14d, r14b
L0245: movzx eax, r14b
L0249: add rsp, 0x30
L024d: pop rbx
L024e: pop rbp
L024f: pop rsi
L0250: pop rdi
L0251: pop r14
L0253: ret

;Test+Test+LinearExpr.Equals(LinearExpr)
L0000: push rdi
L0001: push rsi
L0002: push rbx
L0003: sub rsp, 0x40
L0007: vzeroupper
L000a: vmovaps [rsp+0x30], xmm6
L0010: vmovaps [rsp+0x20], xmm7
L0016: mov rsi, rdx
L0019: mov rdx, rcx
L001c: test rdx, rdx
L001f: je L022c
L0025: test rsi, rsi
L0028: je L0216
L002e: mov edi, [rdx+8]
L0031: mov ecx, edi
L0033: mov eax, [rsi+8]
L0036: cmp ecx, eax
L0038: jne L0216
L003e: cmp edi, 3
L0041: ja L012e
L0047: mov ecx, edi
L0049: lea rax, [Test+Test+LinearExpr.Equals(LinearExpr)]
L0050: mov eax, [rax+rcx*4]
L0053: lea r8, [L0019]
L005a: add rax, r8
L005d: jmp rax
L005f: mov rbx, rdx
L0062: mov rcx, 0x7ff953af1320
L006c: cmp [rbx], rcx
L006f: je short L0079
L0071: call System.Runtime.CompilerServices.CastHelpers.ChkCastClassSpecial(Void*, System.Object)
L0076: mov rbx, rax
L0079: mov rdi, rsi
L007c: mov rcx, 0x7ff953af1320
L0086: cmp [rdi], rcx
L0089: je short L0096
L008b: mov rdx, rsi
L008e: call System.Runtime.CompilerServices.CastHelpers.ChkCastClassSpecial(Void*, System.Object)
L0093: mov rdi, rax
L0096: vmovsd xmm6, [rbx+0x18]
L009b: vmovsd xmm7, [rdi+0x18]
L00a0: vucomisd xmm6, xmm7
L00a4: jp short L00a8
L00a6: je short L00c6
L00a8: vucomisd xmm6, xmm6
L00ac: jp short L00b4
L00ae: je L0216
L00b4: vucomisd xmm7, xmm7
L00b8: setp cl
L00bb: movzx ecx, cl
L00be: test ecx, ecx
L00c0: je L0216
L00c6: mov rdx, [rbx+0x10]
L00ca: mov rsi, [rdi+0x10]
L00ce: jmp L001c
L00d3: mov rdi, rdx
L00d6: mov rcx, 0x7ff953af14c8
L00e0: cmp [rdi], rcx
L00e3: je short L00ed
L00e5: call System.Runtime.CompilerServices.CastHelpers.ChkCastClassSpecial(Void*, System.Object)
L00ea: mov rdi, rax
L00ed: mov rbx, rsi
L00f0: mov rcx, 0x7ff953af14c8
L00fa: cmp [rbx], rcx
L00fd: je short L010a
L00ff: mov rdx, rsi
L0102: call System.Runtime.CompilerServices.CastHelpers.ChkCastClassSpecial(Void*, System.Object)
L0107: mov rbx, rax
L010a: mov rcx, [rdi+0x10]
L010e: mov rdx, [rbx+0x10]
L0112: cmp [rcx], ecx
L0114: call Test+Test+LinearExpr.Equals(LinearExpr)
L0119: test eax, eax
L011b: je L0216
L0121: mov rdx, [rdi+0x18]
L0125: mov rsi, [rbx+0x18]
L0129: jmp L001c
L012e: mov rdi, rdx
L0131: mov rcx, 0x7ff953af0ff8
L013b: cmp [rdi], rcx
L013e: je short L0148
L0140: call System.Runtime.CompilerServices.CastHelpers.ChkCastClassSpecial(Void*, System.Object)
L0145: mov rdi, rax
L0148: mov rax, rsi
L014b: mov rcx, 0x7ff953af0ff8
L0155: cmp [rax], rcx
L0158: je short L0162
L015a: mov rdx, rsi
L015d: call System.Runtime.CompilerServices.CastHelpers.ChkCastClassSpecial(Void*, System.Object)
L0162: vmovsd xmm6, [rdi+0x10]
L0167: vmovsd xmm7, [rax+0x10]
L016c: vucomisd xmm6, xmm7
L0170: jp short L018d
L0172: jne short L018d
L0174: mov eax, 1
L0179: vmovaps xmm6, [rsp+0x30]
L017f: vmovaps xmm7, [rsp+0x20]
L0185: add rsp, 0x40
L0189: pop rbx
L018a: pop rsi
L018b: pop rdi
L018c: ret
L018d: vucomisd xmm6, xmm6
L0191: jp short L0199
L0193: je L0216
L0199: vucomisd xmm7, xmm7
L019d: setp bl
L01a0: movzx ebx, bl
L01a3: jmp L0235
L01a8: mov rdi, rdx
L01ab: mov rcx, 0x7ff953af1178
L01b5: cmp [rdi], rcx
L01b8: je short L01c2
L01ba: call System.Runtime.CompilerServices.CastHelpers.ChkCastClassSpecial(Void*, System.Object)
L01bf: mov rdi, rax
L01c2: mov rbx, rsi
L01c5: mov rcx, 0x7ff953af1178
L01cf: cmp [rbx], rcx
L01d2: je short L01df
L01d4: mov rdx, rsi
L01d7: call System.Runtime.CompilerServices.CastHelpers.ChkCastClassSpecial(Void*, System.Object)
L01dc: mov rbx, rax
L01df: add rdi, 0x10
L01e3: mov rcx, [rdi]
L01e6: mov rsi, [rdi+8]
L01ea: add rbx, 0x10
L01ee: mov rdx, [rbx]
L01f1: mov rdi, [rbx+8]
L01f5: cmp [rcx], ecx
L01f7: call Test+Test+DecisionName.Equals(DecisionName)
L01fc: test eax, eax
L01fe: je short L0212
L0200: mov rcx, rsi
L0203: mov rdx, rdi
L0206: cmp [rcx], ecx
L0208: call Test+Test+DecisionType.Equals(DecisionType)
L020d: movzx ebx, al
L0210: jmp short L0214
L0212: xor ebx, ebx
L0214: jmp short L0235
L0216: xor eax, eax
L0218: vmovaps xmm6, [rsp+0x30]
L021e: vmovaps xmm7, [rsp+0x20]
L0224: add rsp, 0x40
L0228: pop rbx
L0229: pop rsi
L022a: pop rdi
L022b: ret
L022c: test rsi, rsi
L022f: sete bl
L0232: movzx ebx, bl
L0235: movzx eax, bl
L0238: vmovaps xmm6, [rsp+0x30]
L023e: vmovaps xmm7, [rsp+0x20]
L0244: add rsp, 0x40
L0248: pop rbx
L0249: pop rsi
L024a: pop rdi
L024b: ret

;Test+Test+LinearExpr.Equals(System.Object)
L0000: push rsi
L0001: sub rsp, 0x20
L0005: mov rsi, rcx
L0008: mov rcx, 0x7ff953af0bf0
L0012: call System.Runtime.CompilerServices.CastHelpers.IsInstanceOfClass(Void*, System.Object)
L0017: test rax, rax
L001a: je short L002e
L001c: mov rcx, rsi
L001f: mov rdx, rax
L0022: cmp [rcx], ecx
L0024: add rsp, 0x20
L0028: pop rsi
L0029: jmp Test+Test+LinearExpr.Equals(LinearExpr)
L002e: xor eax, eax
L0030: add rsp, 0x20
L0034: pop rsi
L0035: ret

;Test+Test+LinearExpr+Add.get_lExpr()
L0000: mov rax, [rcx+0x10]
L0004: ret

;Test+Test+LinearExpr+Add.get_rExpr()
L0000: mov rax, [rcx+0x18]
L0004: ret

;Test+Test+LinearExpr.get_IsFloat()
L0000: cmp dword ptr [rcx+8], 0
L0004: sete al
L0007: movzx eax, al
L000a: ret

;Test+Test+LinearExpr.get_IsDecision()
L0000: cmp dword ptr [rcx+8], 1
L0004: sete al
L0007: movzx eax, al
L000a: ret

;Test+Test+LinearExpr.get_IsScale()
L0000: cmp dword ptr [rcx+8], 2
L0004: sete al
L0007: movzx eax, al
L000a: ret

;Test+Test+LinearExpr.get_IsAdd()
L0000: cmp dword ptr [rcx+8], 3
L0004: sete al
L0007: movzx eax, al
L000a: ret

;Test+Test+LinearExpr.get_Tag()
L0000: mov eax, [rcx+8]
L0003: ret

;Test+Test+LinearExpr.ToString()
L0000: push rdi
L0001: push rsi
L0002: sub rsp, 0x28
L0006: mov rsi, rcx
L0009: mov rcx, 0x7ff953af84a0
L0013: call 0x00007ff9b182a470
L0018: mov rdi, rax
L001b: mov rdx, 0x191f2a9b3f0
L0025: mov rdx, [rdx]
L0028: lea rcx, [rdi+8]
L002c: call 0x00007ff9b182a080
L0031: xor edx, edx
L0033: mov [rdi+0x10], rdx
L0037: mov [rdi+0x18], rdx
L003b: mov rdx, rdi
L003e: mov rcx, 0x7ff953af8780
L0048: call Microsoft.FSharp.Core.PrintfModule.PrintFormatToStringThen[[System.__Canon, System.Private.CoreLib]](Microsoft.FSharp.Core.PrintfFormat`4<System.__Canon,Microsoft.FSharp.Core.Unit,System.String,System.String>)
L004d: mov rcx, rax
L0050: mov rdx, rsi
L0053: mov rax, [rax]
L0056: mov rax, [rax+0x40]
L005a: mov rax, [rax+0x20]
L005e: add rsp, 0x28
L0062: pop rsi
L0063: pop rdi
L0064: jmp rax

;Test+Test+LinearExpr.CompareTo(LinearExpr)
L0000: push r14
L0002: push rdi
L0003: push rsi
L0004: push rbp
L0005: push rbx
L0006: sub rsp, 0x30
L000a: vzeroupper
L000d: xor eax, eax
L000f: mov [rsp+0x20], rax
L0014: mov [rsp+0x28], rax
L0019: mov rsi, rcx
L001c: mov rdi, rdx
L001f: test rsi, rsi
L0022: je L02e3
L0028: test rdi, rdi
L002b: je L02d3
L0031: mov ebx, [rsi+8]
L0034: mov eax, ebx
L0036: mov ebp, [rdi+8]
L0039: cmp eax, ebp
L003b: jne L02cf
L0041: cmp ebx, 3
L0044: ja short L005e
L0046: mov ecx, ebx
L0048: lea rdx, [Test+Test+LinearExpr.CompareTo(LinearExpr)]
L004f: mov edx, [rdx+rcx*4]
L0052: lea rax, [L001f]
L0059: add rdx, rax
L005c: jmp rdx
L005e: mov rcx, rsi
L0061: mov rdx, 0x7ff953af0ff8
L006b: cmp [rcx], rdx
L006e: je short L007b
L0070: mov rcx, rdx
L0073: mov rdx, rsi
L0076: call System.Runtime.CompilerServices.CastHelpers.ChkCastClassSpecial(Void*, System.Object)
L007b: mov rax, rdi
L007e: mov rcx, 0x7ff953af0ff8
L0088: cmp [rax], rcx
L008b: je short L0095
L008d: mov rdx, rdi
L0090: call System.Runtime.CompilerServices.CastHelpers.ChkCastClassSpecial(Void*, System.Object)
L0095: mov rcx, 0x191f2a86fa0
L009f: mov r14, [rcx]
L00a2: vmovsd xmm1, [rsi+0x10]
L00a7: vmovsd xmm2, [rax+0x10]
L00ac: vucomisd xmm2, xmm1
L00b0: ja L02e8
L00b6: vucomisd xmm1, xmm2
L00ba: ja L02d3
L00c0: vucomisd xmm1, xmm2
L00c4: jp short L00cc
L00c6: je L02f8
L00cc: mov rcx, r14
L00cf: add rsp, 0x30
L00d3: pop rbx
L00d4: pop rbp
L00d5: pop rsi
L00d6: pop rdi
L00d7: pop r14
L00d9: jmp Microsoft.FSharp.Core.LanguagePrimitives+HashCompare.GenericComparisonWithComparerIntrinsic[[System.Double, System.Private.CoreLib]](System.Collections.IComparer, Double, Double)
L00de: mov rcx, rsi
L00e1: mov rdx, 0x7ff953af1178
L00eb: cmp [rcx], rdx
L00ee: je short L00fb
L00f0: mov rcx, rdx
L00f3: mov rdx, rsi
L00f6: call System.Runtime.CompilerServices.CastHelpers.ChkCastClassSpecial(Void*, System.Object)
L00fb: mov rbx, rdi
L00fe: mov rcx, 0x7ff953af1178
L0108: cmp [rbx], rcx
L010b: je short L0118
L010d: mov rdx, rdi
L0110: call System.Runtime.CompilerServices.CastHelpers.ChkCastClassSpecial(Void*, System.Object)
L0115: mov rbx, rax
L0118: mov rcx, 0x191f2a86fa0
L0122: mov r14, [rcx]
L0125: add rsi, 0x10
L0129: mov rcx, [rsi]
L012c: mov [rsp+0x20], rcx
L0131: mov rcx, [rsi+8]
L0135: mov [rsp+0x28], rcx
L013a: add rbx, 0x10
L013e: mov rdi, [rbx]
L0141: mov rsi, [rbx+8]
L0145: mov rcx, 0x7ff953af0538
L014f: call 0x00007ff9b182a470
L0154: mov rbx, rax
L0157: lea rbp, [rbx+8]
L015b: mov rcx, rbp
L015e: mov rdx, rdi
L0161: call 0x00007ff9b182a050
L0166: lea rcx, [rbp+8]
L016a: mov rdx, rsi
L016d: call 0x00007ff9b182a050
L0172: mov rdx, rbx
L0175: lea rcx, [rsp+0x20]
L017a: mov r8, r14
L017d: call Test+Test+Decision.CompareTo(System.Object, System.Collections.IComparer)
L0182: jmp L0305
L0187: mov rcx, rsi
L018a: mov rdx, 0x7ff953af1320
L0194: cmp [rcx], rdx
L0197: je short L01a4
L0199: mov rcx, rdx
L019c: mov rdx, rsi
L019f: call System.Runtime.CompilerServices.CastHelpers.ChkCastClassSpecial(Void*, System.Object)
L01a4: mov rbx, rdi
L01a7: mov rcx, 0x7ff953af1320
L01b1: cmp [rbx], rcx
L01b4: je short L01c1
L01b6: mov rdx, rdi
L01b9: call System.Runtime.CompilerServices.CastHelpers.ChkCastClassSpecial(Void*, System.Object)
L01be: mov rbx, rax
L01c1: mov rcx, 0x191f2a86fa0
L01cb: mov r14, [rcx]
L01ce: vmovsd xmm1, [rsi+0x18]
L01d3: vmovsd xmm2, [rbx+0x18]
L01d8: vucomisd xmm2, xmm1
L01dc: jbe short L01e5
L01de: mov eax, 0xffffffff
L01e3: jmp short L0206
L01e5: vucomisd xmm1, xmm2
L01e9: jbe short L01f2
L01eb: mov eax, 1
L01f0: jmp short L0206
L01f2: vucomisd xmm1, xmm2
L01f6: jp short L01fe
L01f8: jne short L01fe
L01fa: xor eax, eax
L01fc: jmp short L0206
L01fe: mov rcx, r14
L0201: call Microsoft.FSharp.Core.LanguagePrimitives+HashCompare.GenericComparisonWithComparerIntrinsic[[System.Double, System.Private.CoreLib]](System.Collections.IComparer, Double, Double)
L0206: test eax, eax
L0208: jge short L020f
L020a: jmp L0305
L020f: test eax, eax
L0211: jle short L0218
L0213: jmp L0305
L0218: mov rcx, 0x191f2a86fa0
L0222: mov r14, [rcx]
L0225: mov rcx, [rsi+0x10]
L0229: mov rdx, [rbx+0x10]
L022d: mov r8, r14
L0230: cmp [rcx], ecx
L0232: add rsp, 0x30
L0236: pop rbx
L0237: pop rbp
L0238: pop rsi
L0239: pop rdi
L023a: pop r14
L023c: jmp Test+Test+LinearExpr.CompareTo(System.Object, System.Collections.IComparer)
L0241: mov rcx, rsi
L0244: mov rdx, 0x7ff953af14c8
L024e: cmp [rcx], rdx
L0251: je short L025e
L0253: mov rcx, rdx
L0256: mov rdx, rsi
L0259: call System.Runtime.CompilerServices.CastHelpers.ChkCastClassSpecial(Void*, System.Object)
L025e: mov rbx, rdi
L0261: mov rcx, 0x7ff953af14c8
L026b: cmp [rbx], rcx
L026e: je short L027b
L0270: mov rdx, rdi
L0273: call System.Runtime.CompilerServices.CastHelpers.ChkCastClassSpecial(Void*, System.Object)
L0278: mov rbx, rax
L027b: mov rcx, 0x191f2a86fa0
L0285: mov r14, [rcx]
L0288: mov rcx, [rsi+0x10]
L028c: mov rdx, [rbx+0x10]
L0290: mov r8, r14
L0293: cmp [rcx], ecx
L0295: call Test+Test+LinearExpr.CompareTo(System.Object, System.Collections.IComparer)
L029a: test eax, eax
L029c: jge short L02a0
L029e: jmp short L0305
L02a0: test eax, eax
L02a2: jle short L02a6
L02a4: jmp short L0305
L02a6: mov rcx, 0x191f2a86fa0
L02b0: mov r14, [rcx]
L02b3: mov rcx, [rsi+0x18]
L02b7: mov rdx, [rbx+0x18]
L02bb: mov r8, r14
L02be: cmp [rcx], ecx
L02c0: add rsp, 0x30
L02c4: pop rbx
L02c5: pop rbp
L02c6: pop rsi
L02c7: pop rdi
L02c8: pop r14
L02ca: jmp Test+Test+LinearExpr.CompareTo(System.Object, System.Collections.IComparer)
L02cf: sub eax, ebp
L02d1: jmp short L0305
L02d3: mov eax, 1
L02d8: add rsp, 0x30
L02dc: pop rbx
L02dd: pop rbp
L02de: pop rsi
L02df: pop rdi
L02e0: pop r14
L02e2: ret
L02e3: test rdi, rdi
L02e6: je short L02f8
L02e8: mov eax, 0xffffffff
L02ed: add rsp, 0x30
L02f1: pop rbx
L02f2: pop rbp
L02f3: pop rsi
L02f4: pop rdi
L02f5: pop r14
L02f7: ret
L02f8: xor eax, eax
L02fa: add rsp, 0x30
L02fe: pop rbx
L02ff: pop rbp
L0300: pop rsi
L0301: pop rdi
L0302: pop r14
L0304: ret
L0305: add rsp, 0x30
L0309: pop rbx
L030a: pop rbp
L030b: pop rsi
L030c: pop rdi
L030d: pop r14
L030f: ret

;Test+Test+LinearExpr.CompareTo(System.Object)
L0000: push rsi
L0001: sub rsp, 0x20
L0005: mov rsi, rcx
L0008: mov rax, rdx
L000b: test rax, rax
L000e: je short L0024
L0010: mov rcx, 0x7ff953af0bf0
L001a: cmp [rax], rcx
L001d: je short L0024
L001f: call System.Runtime.CompilerServices.CastHelpers.ChkCastClassSpecial(Void*, System.Object)
L0024: mov rcx, rsi
L0027: mov rdx, rax
L002a: cmp [rcx], ecx
L002c: add rsp, 0x20
L0030: pop rsi
L0031: jmp Test+Test+LinearExpr.CompareTo(LinearExpr)

;Test+Test+LinearExpr.CompareTo(System.Object, System.Collections.IComparer)
L0000: push r14
L0002: push rdi
L0003: push rsi
L0004: push rbp
L0005: push rbx
L0006: sub rsp, 0x30
L000a: vzeroupper
L000d: xor eax, eax
L000f: mov [rsp+0x20], rax
L0014: mov [rsp+0x28], rax
L0019: mov rsi, rdx
L001c: mov rdi, r8
L001f: mov rbx, rcx
L0022: mov rbp, rsi
L0025: test rbp, rbp
L0028: je short L0045
L002a: mov rcx, 0x7ff953af0bf0
L0034: cmp [rbp], rcx
L0038: je short L0045
L003a: mov rdx, rsi
L003d: call System.Runtime.CompilerServices.CastHelpers.ChkCastClassSpecial(Void*, System.Object)
L0042: mov rbp, rax
L0045: test rbx, rbx
L0048: je L02d5
L004e: test rbp, rbp
L0051: je short L006b
L0053: mov rcx, 0x7ff953af0bf0
L005d: cmp [rbp], rcx
L0061: je short L006b
L0063: mov rdx, rbp
L0066: call System.Runtime.CompilerServices.CastHelpers.ChkCastClassSpecial(Void*, System.Object)
L006b: test rbp, rbp
L006e: je L02c5
L0074: mov r14d, [rbx+8]
L0078: mov eax, r14d
L007b: mov esi, [rbp+8]
L007e: cmp eax, esi
L0080: jne L02c1
L0086: cmp r14d, 3
L008a: ja L01ad
L0090: mov ecx, r14d
L0093: lea rdx, [Test+Test+LinearExpr.CompareTo(System.Object, System.Collections.IComparer)]
L009a: mov edx, [rdx+rcx*4]
L009d: lea rax, [L001f]
L00a4: add rdx, rax
L00a7: jmp rdx
L00a9: mov rsi, rbx
L00ac: mov rcx, 0x7ff953af1320
L00b6: cmp [rsi], rcx
L00b9: je short L00c6
L00bb: mov rdx, rbx
L00be: call System.Runtime.CompilerServices.CastHelpers.ChkCastClassSpecial(Void*, System.Object)
L00c3: mov rsi, rax
L00c6: mov rbx, rbp
L00c9: mov rcx, 0x7ff953af1320
L00d3: cmp [rbx], rcx
L00d6: je short L00e3
L00d8: mov rdx, rbp
L00db: call System.Runtime.CompilerServices.CastHelpers.ChkCastClassSpecial(Void*, System.Object)
L00e0: mov rbx, rax
L00e3: vmovsd xmm1, [rsi+0x18]
L00e8: vmovsd xmm2, [rbx+0x18]
L00ed: vucomisd xmm2, xmm1
L00f1: jbe short L00fa
L00f3: mov eax, 0xffffffff
L00f8: jmp short L011b
L00fa: vucomisd xmm1, xmm2
L00fe: jbe short L0107
L0100: mov eax, 1
L0105: jmp short L011b
L0107: vucomisd xmm1, xmm2
L010b: jp short L0113
L010d: jne short L0113
L010f: xor eax, eax
L0111: jmp short L011b
L0113: mov rcx, rdi
L0116: call Microsoft.FSharp.Core.LanguagePrimitives+HashCompare.GenericComparisonWithComparerIntrinsic[[System.Double, System.Private.CoreLib]](System.Collections.IComparer, Double, Double)
L011b: test eax, eax
L011d: jl L02b9
L0123: test eax, eax
L0125: jg L02bb
L012b: mov rcx, [rsi+0x10]
L012f: mov rdx, [rbx+0x10]
L0133: mov rsi, rdx
L0136: mov rbx, rcx
L0139: jmp L0022
L013e: mov rsi, rbx
L0141: mov rcx, 0x7ff953af14c8
L014b: cmp [rsi], rcx
L014e: je short L015b
L0150: mov rdx, rbx
L0153: call System.Runtime.CompilerServices.CastHelpers.ChkCastClassSpecial(Void*, System.Object)
L0158: mov rsi, rax
L015b: mov rbx, rbp
L015e: mov rcx, 0x7ff953af14c8
L0168: cmp [rbx], rcx
L016b: je short L0178
L016d: mov rdx, rbp
L0170: call System.Runtime.CompilerServices.CastHelpers.ChkCastClassSpecial(Void*, System.Object)
L0175: mov rbx, rax
L0178: mov rcx, [rsi+0x10]
L017c: mov rdx, [rbx+0x10]
L0180: mov r8, rdi
L0183: cmp [rcx], ecx
L0185: call Test+Test+LinearExpr.CompareTo(System.Object, System.Collections.IComparer)
L018a: test eax, eax
L018c: jl L02bd
L0192: test eax, eax
L0194: jg L02bf
L019a: mov rcx, [rsi+0x18]
L019e: mov rdx, [rbx+0x18]
L01a2: mov rsi, rdx
L01a5: mov rbx, rcx
L01a8: jmp L0022
L01ad: mov rsi, rbx
L01b0: mov rcx, 0x7ff953af0ff8
L01ba: cmp [rsi], rcx
L01bd: je short L01ca
L01bf: mov rdx, rbx
L01c2: call System.Runtime.CompilerServices.CastHelpers.ChkCastClassSpecial(Void*, System.Object)
L01c7: mov rsi, rax
L01ca: mov rax, rbp
L01cd: mov rcx, 0x7ff953af0ff8
L01d7: cmp [rax], rcx
L01da: je short L01e4
L01dc: mov rdx, rbp
L01df: call System.Runtime.CompilerServices.CastHelpers.ChkCastClassSpecial(Void*, System.Object)
L01e4: vmovsd xmm1, [rsi+0x10]
L01e9: vmovsd xmm2, [rax+0x10]
L01ee: vucomisd xmm2, xmm1
L01f2: ja L02f9
L01f8: vucomisd xmm1, xmm2
L01fc: ja L02c5
L0202: vucomisd xmm1, xmm2
L0206: jp short L020e
L0208: je L0309
L020e: mov rcx, rdi
L0211: add rsp, 0x30
L0215: pop rbx
L0216: pop rbp
L0217: pop rsi
L0218: pop rdi
L0219: pop r14
L021b: jmp Microsoft.FSharp.Core.LanguagePrimitives+HashCompare.GenericComparisonWithComparerIntrinsic[[System.Double, System.Private.CoreLib]](System.Collections.IComparer, Double, Double)
L0220: mov rsi, rbx
L0223: mov rcx, 0x7ff953af1178
L022d: cmp [rsi], rcx
L0230: je short L023d
L0232: mov rdx, rbx
L0235: call System.Runtime.CompilerServices.CastHelpers.ChkCastClassSpecial(Void*, System.Object)
L023a: mov rsi, rax
L023d: mov rbx, rbp
L0240: mov rcx, 0x7ff953af1178
L024a: cmp [rbx], rcx
L024d: je short L025a
L024f: mov rdx, rbp
L0252: call System.Runtime.CompilerServices.CastHelpers.ChkCastClassSpecial(Void*, System.Object)
L0257: mov rbx, rax
L025a: add rsi, 0x10
L025e: mov rcx, [rsi]
L0261: mov [rsp+0x20], rcx
L0266: mov rcx, [rsi+8]
L026a: mov [rsp+0x28], rcx
L026f: add rbx, 0x10
L0273: mov rsi, [rbx]
L0276: mov rbx, [rbx+8]
L027a: mov rcx, 0x7ff953af0538
L0284: call 0x00007ff9b182a470
L0289: mov rbp, rax
L028c: lea r14, [rbp+8]
L0290: mov rcx, r14
L0293: mov rdx, rsi
L0296: call 0x00007ff9b182a050
L029b: lea rcx, [r14+8]
L029f: mov rdx, rbx
L02a2: call 0x00007ff9b182a050
L02a7: mov rdx, rbp
L02aa: lea rcx, [rsp+0x20]
L02af: mov r8, rdi
L02b2: call Test+Test+Decision.CompareTo(System.Object, System.Collections.IComparer)
L02b7: jmp short L0316
L02b9: jmp short L0316
L02bb: jmp short L0316
L02bd: jmp short L0316
L02bf: jmp short L0316
L02c1: sub eax, esi
L02c3: jmp short L0316
L02c5: mov eax, 1
L02ca: add rsp, 0x30
L02ce: pop rbx
L02cf: pop rbp
L02d0: pop rsi
L02d1: pop rdi
L02d2: pop r14
L02d4: ret
L02d5: mov rax, rsi
L02d8: test rax, rax
L02db: je short L02f4
L02dd: mov rcx, 0x7ff953af0bf0
L02e7: cmp [rax], rcx
L02ea: je short L02f4
L02ec: mov rdx, rsi
L02ef: call System.Runtime.CompilerServices.CastHelpers.ChkCastClassSpecial(Void*, System.Object)
L02f4: test rax, rax
L02f7: je short L0309
L02f9: mov eax, 0xffffffff
L02fe: add rsp, 0x30
L0302: pop rbx
L0303: pop rbp
L0304: pop rsi
L0305: pop rdi
L0306: pop r14
L0308: ret
L0309: xor eax, eax
L030b: add rsp, 0x30
L030f: pop rbx
L0310: pop rbp
L0311: pop rsi
L0312: pop rdi
L0313: pop r14
L0315: ret
L0316: add rsp, 0x30
L031a: pop rbx
L031b: pop rbp
L031c: pop rsi
L031d: pop rdi
L031e: pop r14
L0320: ret

;Test+Test+LinearExpr.GetHashCode(System.Collections.IEqualityComparer)
L0000: push rdi
L0001: push rsi
L0002: push rbp
L0003: push rbx
L0004: sub rsp, 0x38
L0008: vzeroupper
L000b: xor eax, eax
L000d: mov [rsp+0x28], rax
L0012: mov [rsp+0x30], rax
L0017: mov rsi, rcx
L001a: mov rdi, rdx
L001d: test rsi, rsi
L0020: je L017b
L0026: mov ebx, [rsi+8]
L0029: cmp ebx, 3
L002c: ja short L0046
L002e: mov ecx, ebx
L0030: lea rdx, [Test+Test+LinearExpr.GetHashCode(System.Collections.IEqualityComparer)]
L0037: mov edx, [rdx+rcx*4]
L003a: lea rax, [L001d]
L0041: add rdx, rax
L0044: jmp rdx
L0046: mov rcx, rsi
L0049: mov rdx, 0x7ff953af0ff8
L0053: cmp [rcx], rdx
L0056: je short L0063
L0058: mov rcx, rdx
L005b: mov rdx, rsi
L005e: call System.Runtime.CompilerServices.CastHelpers.ChkCastClassSpecial(Void*, System.Object)
L0063: vmovsd xmm1, [rsi+0x10]
L0068: mov rcx, rdi
L006b: call Microsoft.FSharp.Core.LanguagePrimitives+HashCompare.GenericHashWithComparerIntrinsic[[System.Double, System.Private.CoreLib]](System.Collections.IEqualityComparer, Double)
L0070: lea ebp, [rax-0x61c88647]
L0076: mov eax, ebp
L0078: jmp L0172
L007d: mov rcx, rsi
L0080: mov rdx, 0x7ff953af1178
L008a: cmp [rcx], rdx
L008d: je short L009a
L008f: mov rcx, rdx
L0092: mov rdx, rsi
L0095: call System.Runtime.CompilerServices.CastHelpers.ChkCastClassSpecial(Void*, System.Object)
L009a: add rsi, 0x10
L009e: mov rcx, [rsi]
L00a1: mov [rsp+0x28], rcx
L00a6: mov rcx, [rsi+8]
L00aa: mov [rsp+0x30], rcx
L00af: lea rcx, [rsp+0x28]
L00b4: mov rdx, rdi
L00b7: call Test+Test+Decision.GetHashCode(System.Collections.IEqualityComparer)
L00bc: lea ebp, [rax-0x61c88607]
L00c2: mov eax, ebp
L00c4: jmp L0172
L00c9: mov rcx, rsi
L00cc: mov rdx, 0x7ff953af1320
L00d6: cmp [rcx], rdx
L00d9: je short L00e6
L00db: mov rcx, rdx
L00de: mov rdx, rsi
L00e1: call System.Runtime.CompilerServices.CastHelpers.ChkCastClassSpecial(Void*, System.Object)
L00e6: mov rcx, [rsi+0x10]
L00ea: mov rdx, rdi
L00ed: cmp [rcx], ecx
L00ef: call Test+Test+LinearExpr.GetHashCode(System.Collections.IEqualityComparer)
L00f4: lea ebp, [rax-0x61c885c7]
L00fa: vmovsd xmm1, [rsi+0x18]
L00ff: mov rcx, rdi
L0102: call Microsoft.FSharp.Core.LanguagePrimitives+HashCompare.GenericHashWithComparerIntrinsic[[System.Double, System.Private.CoreLib]](System.Collections.IEqualityComparer, Double)
L0107: mov ecx, ebp
L0109: shl ecx, 6
L010c: add ecx, eax
L010e: mov edx, ebp
L0110: sar edx, 2
L0113: lea ebp, [rcx+rdx-0x61c88647]
L011a: mov eax, ebp
L011c: jmp short L0172
L011e: mov rcx, rsi
L0121: mov rdx, 0x7ff953af14c8
L012b: cmp [rcx], rdx
L012e: je short L013b
L0130: mov rcx, rdx
L0133: mov rdx, rsi
L0136: call System.Runtime.CompilerServices.CastHelpers.ChkCastClassSpecial(Void*, System.Object)
L013b: mov rcx, [rsi+0x18]
L013f: mov rdx, rdi
L0142: cmp [rcx], ecx
L0144: call Test+Test+LinearExpr.GetHashCode(System.Collections.IEqualityComparer)
L0149: lea ebp, [rax-0x61c88587]
L014f: mov rcx, [rsi+0x10]
L0153: mov rdx, rdi
L0156: cmp [rcx], ecx
L0158: call Test+Test+LinearExpr.GetHashCode(System.Collections.IEqualityComparer)
L015d: mov edx, ebp
L015f: shl edx, 6
L0162: add eax, edx
L0164: mov edx, ebp
L0166: sar edx, 2
L0169: lea ebp, [rax+rdx-0x61c88647]
L0170: mov eax, ebp
L0172: add rsp, 0x38
L0176: pop rbx
L0177: pop rbp
L0178: pop rsi
L0179: pop rdi
L017a: ret
L017b: xor eax, eax
L017d: add rsp, 0x38
L0181: pop rbx
L0182: pop rbp
L0183: pop rsi
L0184: pop rdi
L0185: ret

;Test+Test+LinearExpr.GetHashCode()
L0000: mov rdx, 0x191f2a86fa8
L000a: mov rdx, [rdx]
L000d: cmp [rcx], ecx
L000f: jmp Test+Test+LinearExpr.GetHashCode(System.Collections.IEqualityComparer)

;Test+Test+LinearExpr.Equals(System.Object, System.Collections.IEqualityComparer)
L0000: push r14
L0002: push rdi
L0003: push rsi
L0004: push rbp
L0005: push rbx
L0006: sub rsp, 0x30
L000a: vzeroupper
L000d: xor eax, eax
L000f: mov [rsp+0x20], rax
L0014: mov [rsp+0x28], rax
L0019: mov rsi, r8
L001c: mov rdi, rcx
L001f: test rdi, rdi
L0022: je L023a
L0028: mov rcx, 0x7ff953af0bf0
L0032: call System.Runtime.CompilerServices.CastHelpers.IsInstanceOfClass(Void*, System.Object)
L0037: mov rbx, rax
L003a: test rbx, rbx
L003d: je L022d
L0043: mov ebp, [rdi+8]
L0046: mov ecx, ebp
L0048: mov edx, [rbx+8]
L004b: cmp ecx, edx
L004d: jne L022d
L0053: cmp ebp, 3
L0056: ja L013a
L005c: mov ecx, ebp
L005e: lea rdx, [Test+Test+LinearExpr.Equals(System.Object, System.Collections.IEqualityComparer)]
L0065: mov edx, [rdx+rcx*4]
L0068: lea rax, [L001c]
L006f: add rdx, rax
L0072: jmp rdx
L0074: mov r14, rdi
L0077: mov rcx, 0x7ff953af1320
L0081: cmp [r14], rcx
L0084: je short L0091
L0086: mov rdx, rdi
L0089: call System.Runtime.CompilerServices.CastHelpers.ChkCastClassSpecial(Void*, System.Object)
L008e: mov r14, rax
L0091: mov rdi, rbx
L0094: mov rcx, 0x7ff953af1320
L009e: cmp [rdi], rcx
L00a1: je short L00ae
L00a3: mov rdx, rbx
L00a6: call System.Runtime.CompilerServices.CastHelpers.ChkCastClassSpecial(Void*, System.Object)
L00ab: mov rdi, rax
L00ae: vmovsd xmm0, [r14+0x18]
L00b4: vucomisd xmm0, [rdi+0x18]
L00b9: jp L022d
L00bf: jne L022d
L00c5: mov rcx, [r14+0x10]
L00c9: mov rdx, [rdi+0x10]
L00cd: mov rdi, rcx
L00d0: jmp L001f
L00d5: mov rbp, rdi
L00d8: mov rcx, 0x7ff953af14c8
L00e2: cmp [rbp], rcx
L00e6: je short L00f3
L00e8: mov rdx, rdi
L00eb: call System.Runtime.CompilerServices.CastHelpers.ChkCastClassSpecial(Void*, System.Object)
L00f0: mov rbp, rax
L00f3: mov rdi, rbx
L00f6: mov rcx, 0x7ff953af14c8
L0100: cmp [rdi], rcx
L0103: je short L0110
L0105: mov rdx, rbx
L0108: call System.Runtime.CompilerServices.CastHelpers.ChkCastClassSpecial(Void*, System.Object)
L010d: mov rdi, rax
L0110: mov rcx, [rbp+0x10]
L0114: mov rdx, [rdi+0x10]
L0118: mov r8, rsi
L011b: cmp [rcx], ecx
L011d: call Test+Test+LinearExpr.Equals(System.Object, System.Collections.IEqualityComparer)
L0122: test eax, eax
L0124: je L022d
L012a: mov rcx, [rbp+0x18]
L012e: mov rdx, [rdi+0x18]
L0132: mov rdi, rcx
L0135: jmp L001f
L013a: mov rbp, rdi
L013d: mov rcx, 0x7ff953af0ff8
L0147: cmp [rbp], rcx
L014b: je short L0158
L014d: mov rdx, rdi
L0150: call System.Runtime.CompilerServices.CastHelpers.ChkCastClassSpecial(Void*, System.Object)
L0155: mov rbp, rax
L0158: mov rax, rbx
L015b: mov rcx, 0x7ff953af0ff8
L0165: cmp [rax], rcx
L0168: je short L0172
L016a: mov rdx, rbx
L016d: call System.Runtime.CompilerServices.CastHelpers.ChkCastClassSpecial(Void*, System.Object)
L0172: vmovsd xmm0, [rbp+0x10]
L0177: vucomisd xmm0, [rax+0x10]
L017c: setnp r14b
L0180: jp short L0186
L0182: sete r14b
L0186: movzx r14d, r14b
L018a: jmp L0245
L018f: mov rbp, rdi
L0192: mov rcx, 0x7ff953af1178
L019c: cmp [rbp], rcx
L01a0: je short L01ad
L01a2: mov rdx, rdi
L01a5: call System.Runtime.CompilerServices.CastHelpers.ChkCastClassSpecial(Void*, System.Object)
L01aa: mov rbp, rax
L01ad: mov rdi, rbx
L01b0: mov rcx, 0x7ff953af1178
L01ba: cmp [rdi], rcx
L01bd: je short L01ca
L01bf: mov rdx, rbx
L01c2: call System.Runtime.CompilerServices.CastHelpers.ChkCastClassSpecial(Void*, System.Object)
L01c7: mov rdi, rax
L01ca: add rbp, 0x10
L01ce: mov rcx, [rbp]
L01d2: mov [rsp+0x20], rcx
L01d7: mov rcx, [rbp+8]
L01db: mov [rsp+0x28], rcx
L01e0: add rdi, 0x10
L01e4: mov rbx, [rdi]
L01e7: mov rdi, [rdi+8]
L01eb: mov rcx, 0x7ff953af0538
L01f5: call 0x00007ff9b182a470
L01fa: mov rbp, rax
L01fd: lea r14, [rbp+8]
L0201: mov rcx, r14
L0204: mov rdx, rbx
L0207: call 0x00007ff9b182a050
L020c: lea rcx, [r14+8]
L0210: mov rdx, rdi
L0213: call 0x00007ff9b182a050
L0218: mov rdx, rbp
L021b: lea rcx, [rsp+0x20]
L0220: mov r8, rsi
L0223: call Test+Test+Decision.Equals(System.Object, System.Collections.IEqualityComparer)
L0228: mov r14d, eax
L022b: jmp short L0245
L022d: xor eax, eax
L022f: add rsp, 0x30
L0233: pop rbx
L0234: pop rbp
L0235: pop rsi
L0236: pop rdi
L0237: pop r14
L0239: ret
L023a: test rdx, rdx
L023d: sete r14b
L0241: movzx r14d, r14b
L0245: movzx eax, r14b
L0249: add rsp, 0x30
L024d: pop rbx
L024e: pop rbp
L024f: pop rsi
L0250: pop rdi
L0251: pop r14
L0253: ret

;Test+Test+LinearExpr.Equals(LinearExpr)
L0000: push rdi
L0001: push rsi
L0002: push rbx
L0003: sub rsp, 0x40
L0007: vzeroupper
L000a: vmovaps [rsp+0x30], xmm6
L0010: vmovaps [rsp+0x20], xmm7
L0016: mov rsi, rdx
L0019: mov rdx, rcx
L001c: test rdx, rdx
L001f: je L022c
L0025: test rsi, rsi
L0028: je L0216
L002e: mov edi, [rdx+8]
L0031: mov ecx, edi
L0033: mov eax, [rsi+8]
L0036: cmp ecx, eax
L0038: jne L0216
L003e: cmp edi, 3
L0041: ja L012e
L0047: mov ecx, edi
L0049: lea rax, [Test+Test+LinearExpr.Equals(LinearExpr)]
L0050: mov eax, [rax+rcx*4]
L0053: lea r8, [L0019]
L005a: add rax, r8
L005d: jmp rax
L005f: mov rbx, rdx
L0062: mov rcx, 0x7ff953af1320
L006c: cmp [rbx], rcx
L006f: je short L0079
L0071: call System.Runtime.CompilerServices.CastHelpers.ChkCastClassSpecial(Void*, System.Object)
L0076: mov rbx, rax
L0079: mov rdi, rsi
L007c: mov rcx, 0x7ff953af1320
L0086: cmp [rdi], rcx
L0089: je short L0096
L008b: mov rdx, rsi
L008e: call System.Runtime.CompilerServices.CastHelpers.ChkCastClassSpecial(Void*, System.Object)
L0093: mov rdi, rax
L0096: vmovsd xmm6, [rbx+0x18]
L009b: vmovsd xmm7, [rdi+0x18]
L00a0: vucomisd xmm6, xmm7
L00a4: jp short L00a8
L00a6: je short L00c6
L00a8: vucomisd xmm6, xmm6
L00ac: jp short L00b4
L00ae: je L0216
L00b4: vucomisd xmm7, xmm7
L00b8: setp cl
L00bb: movzx ecx, cl
L00be: test ecx, ecx
L00c0: je L0216
L00c6: mov rdx, [rbx+0x10]
L00ca: mov rsi, [rdi+0x10]
L00ce: jmp L001c
L00d3: mov rdi, rdx
L00d6: mov rcx, 0x7ff953af14c8
L00e0: cmp [rdi], rcx
L00e3: je short L00ed
L00e5: call System.Runtime.CompilerServices.CastHelpers.ChkCastClassSpecial(Void*, System.Object)
L00ea: mov rdi, rax
L00ed: mov rbx, rsi
L00f0: mov rcx, 0x7ff953af14c8
L00fa: cmp [rbx], rcx
L00fd: je short L010a
L00ff: mov rdx, rsi
L0102: call System.Runtime.CompilerServices.CastHelpers.ChkCastClassSpecial(Void*, System.Object)
L0107: mov rbx, rax
L010a: mov rcx, [rdi+0x10]
L010e: mov rdx, [rbx+0x10]
L0112: cmp [rcx], ecx
L0114: call Test+Test+LinearExpr.Equals(LinearExpr)
L0119: test eax, eax
L011b: je L0216
L0121: mov rdx, [rdi+0x18]
L0125: mov rsi, [rbx+0x18]
L0129: jmp L001c
L012e: mov rdi, rdx
L0131: mov rcx, 0x7ff953af0ff8
L013b: cmp [rdi], rcx
L013e: je short L0148
L0140: call System.Runtime.CompilerServices.CastHelpers.ChkCastClassSpecial(Void*, System.Object)
L0145: mov rdi, rax
L0148: mov rax, rsi
L014b: mov rcx, 0x7ff953af0ff8
L0155: cmp [rax], rcx
L0158: je short L0162
L015a: mov rdx, rsi
L015d: call System.Runtime.CompilerServices.CastHelpers.ChkCastClassSpecial(Void*, System.Object)
L0162: vmovsd xmm6, [rdi+0x10]
L0167: vmovsd xmm7, [rax+0x10]
L016c: vucomisd xmm6, xmm7
L0170: jp short L018d
L0172: jne short L018d
L0174: mov eax, 1
L0179: vmovaps xmm6, [rsp+0x30]
L017f: vmovaps xmm7, [rsp+0x20]
L0185: add rsp, 0x40
L0189: pop rbx
L018a: pop rsi
L018b: pop rdi
L018c: ret
L018d: vucomisd xmm6, xmm6
L0191: jp short L0199
L0193: je L0216
L0199: vucomisd xmm7, xmm7
L019d: setp bl
L01a0: movzx ebx, bl
L01a3: jmp L0235
L01a8: mov rdi, rdx
L01ab: mov rcx, 0x7ff953af1178
L01b5: cmp [rdi], rcx
L01b8: je short L01c2
L01ba: call System.Runtime.CompilerServices.CastHelpers.ChkCastClassSpecial(Void*, System.Object)
L01bf: mov rdi, rax
L01c2: mov rbx, rsi
L01c5: mov rcx, 0x7ff953af1178
L01cf: cmp [rbx], rcx
L01d2: je short L01df
L01d4: mov rdx, rsi
L01d7: call System.Runtime.CompilerServices.CastHelpers.ChkCastClassSpecial(Void*, System.Object)
L01dc: mov rbx, rax
L01df: add rdi, 0x10
L01e3: mov rcx, [rdi]
L01e6: mov rsi, [rdi+8]
L01ea: add rbx, 0x10
L01ee: mov rdx, [rbx]
L01f1: mov rdi, [rbx+8]
L01f5: cmp [rcx], ecx
L01f7: call Test+Test+DecisionName.Equals(DecisionName)
L01fc: test eax, eax
L01fe: je short L0212
L0200: mov rcx, rsi
L0203: mov rdx, rdi
L0206: cmp [rcx], ecx
L0208: call Test+Test+DecisionType.Equals(DecisionType)
L020d: movzx ebx, al
L0210: jmp short L0214
L0212: xor ebx, ebx
L0214: jmp short L0235
L0216: xor eax, eax
L0218: vmovaps xmm6, [rsp+0x30]
L021e: vmovaps xmm7, [rsp+0x20]
L0224: add rsp, 0x40
L0228: pop rbx
L0229: pop rsi
L022a: pop rdi
L022b: ret
L022c: test rsi, rsi
L022f: sete bl
L0232: movzx ebx, bl
L0235: movzx eax, bl
L0238: vmovaps xmm6, [rsp+0x30]
L023e: vmovaps xmm7, [rsp+0x20]
L0244: add rsp, 0x40
L0248: pop rbx
L0249: pop rsi
L024a: pop rdi
L024b: ret

;Test+Test+LinearExpr.Equals(System.Object)
L0000: push rsi
L0001: sub rsp, 0x20
L0005: mov rsi, rcx
L0008: mov rcx, 0x7ff953af0bf0
L0012: call System.Runtime.CompilerServices.CastHelpers.IsInstanceOfClass(Void*, System.Object)
L0017: test rax, rax
L001a: je short L002e
L001c: mov rcx, rsi
L001f: mov rdx, rax
L0022: cmp [rcx], ecx
L0024: add rsp, 0x20
L0028: pop rsi
L0029: jmp Test+Test+LinearExpr.Equals(LinearExpr)
L002e: xor eax, eax
L0030: add rsp, 0x20
L0034: pop rsi
L0035: ret

;Test+Test+LinearExpr+Float.get_Item()
L0000: vzeroupper
L0003: vmovsd xmm0, [rcx+0x10]
L0008: ret

;Test+Test+LinearExpr.get_IsFloat()
L0000: cmp dword ptr [rcx+8], 0
L0004: sete al
L0007: movzx eax, al
L000a: ret

;Test+Test+LinearExpr.get_IsDecision()
L0000: cmp dword ptr [rcx+8], 1
L0004: sete al
L0007: movzx eax, al
L000a: ret

;Test+Test+LinearExpr.get_IsScale()
L0000: cmp dword ptr [rcx+8], 2
L0004: sete al
L0007: movzx eax, al
L000a: ret

;Test+Test+LinearExpr.get_IsAdd()
L0000: cmp dword ptr [rcx+8], 3
L0004: sete al
L0007: movzx eax, al
L000a: ret

;Test+Test+LinearExpr.get_Tag()
L0000: mov eax, [rcx+8]
L0003: ret

;Test+Test+LinearExpr.ToString()
L0000: push rdi
L0001: push rsi
L0002: sub rsp, 0x28
L0006: mov rsi, rcx
L0009: mov rcx, 0x7ff953af84a0
L0013: call 0x00007ff9b182a470
L0018: mov rdi, rax
L001b: mov rdx, 0x191f2a9b3f0
L0025: mov rdx, [rdx]
L0028: lea rcx, [rdi+8]
L002c: call 0x00007ff9b182a080
L0031: xor edx, edx
L0033: mov [rdi+0x10], rdx
L0037: mov [rdi+0x18], rdx
L003b: mov rdx, rdi
L003e: mov rcx, 0x7ff953af8780
L0048: call Microsoft.FSharp.Core.PrintfModule.PrintFormatToStringThen[[System.__Canon, System.Private.CoreLib]](Microsoft.FSharp.Core.PrintfFormat`4<System.__Canon,Microsoft.FSharp.Core.Unit,System.String,System.String>)
L004d: mov rcx, rax
L0050: mov rdx, rsi
L0053: mov rax, [rax]
L0056: mov rax, [rax+0x40]
L005a: mov rax, [rax+0x20]
L005e: add rsp, 0x28
L0062: pop rsi
L0063: pop rdi
L0064: jmp rax

;Test+Test+LinearExpr.CompareTo(LinearExpr)
L0000: push r14
L0002: push rdi
L0003: push rsi
L0004: push rbp
L0005: push rbx
L0006: sub rsp, 0x30
L000a: vzeroupper
L000d: xor eax, eax
L000f: mov [rsp+0x20], rax
L0014: mov [rsp+0x28], rax
L0019: mov rsi, rcx
L001c: mov rdi, rdx
L001f: test rsi, rsi
L0022: je L02e3
L0028: test rdi, rdi
L002b: je L02d3
L0031: mov ebx, [rsi+8]
L0034: mov eax, ebx
L0036: mov ebp, [rdi+8]
L0039: cmp eax, ebp
L003b: jne L02cf
L0041: cmp ebx, 3
L0044: ja short L005e
L0046: mov ecx, ebx
L0048: lea rdx, [Test+Test+LinearExpr.CompareTo(LinearExpr)]
L004f: mov edx, [rdx+rcx*4]
L0052: lea rax, [L001f]
L0059: add rdx, rax
L005c: jmp rdx
L005e: mov rcx, rsi
L0061: mov rdx, 0x7ff953af0ff8
L006b: cmp [rcx], rdx
L006e: je short L007b
L0070: mov rcx, rdx
L0073: mov rdx, rsi
L0076: call System.Runtime.CompilerServices.CastHelpers.ChkCastClassSpecial(Void*, System.Object)
L007b: mov rax, rdi
L007e: mov rcx, 0x7ff953af0ff8
L0088: cmp [rax], rcx
L008b: je short L0095
L008d: mov rdx, rdi
L0090: call System.Runtime.CompilerServices.CastHelpers.ChkCastClassSpecial(Void*, System.Object)
L0095: mov rcx, 0x191f2a86fa0
L009f: mov r14, [rcx]
L00a2: vmovsd xmm1, [rsi+0x10]
L00a7: vmovsd xmm2, [rax+0x10]
L00ac: vucomisd xmm2, xmm1
L00b0: ja L02e8
L00b6: vucomisd xmm1, xmm2
L00ba: ja L02d3
L00c0: vucomisd xmm1, xmm2
L00c4: jp short L00cc
L00c6: je L02f8
L00cc: mov rcx, r14
L00cf: add rsp, 0x30
L00d3: pop rbx
L00d4: pop rbp
L00d5: pop rsi
L00d6: pop rdi
L00d7: pop r14
L00d9: jmp Microsoft.FSharp.Core.LanguagePrimitives+HashCompare.GenericComparisonWithComparerIntrinsic[[System.Double, System.Private.CoreLib]](System.Collections.IComparer, Double, Double)
L00de: mov rcx, rsi
L00e1: mov rdx, 0x7ff953af1178
L00eb: cmp [rcx], rdx
L00ee: je short L00fb
L00f0: mov rcx, rdx
L00f3: mov rdx, rsi
L00f6: call System.Runtime.CompilerServices.CastHelpers.ChkCastClassSpecial(Void*, System.Object)
L00fb: mov rbx, rdi
L00fe: mov rcx, 0x7ff953af1178
L0108: cmp [rbx], rcx
L010b: je short L0118
L010d: mov rdx, rdi
L0110: call System.Runtime.CompilerServices.CastHelpers.ChkCastClassSpecial(Void*, System.Object)
L0115: mov rbx, rax
L0118: mov rcx, 0x191f2a86fa0
L0122: mov r14, [rcx]
L0125: add rsi, 0x10
L0129: mov rcx, [rsi]
L012c: mov [rsp+0x20], rcx
L0131: mov rcx, [rsi+8]
L0135: mov [rsp+0x28], rcx
L013a: add rbx, 0x10
L013e: mov rdi, [rbx]
L0141: mov rsi, [rbx+8]
L0145: mov rcx, 0x7ff953af0538
L014f: call 0x00007ff9b182a470
L0154: mov rbx, rax
L0157: lea rbp, [rbx+8]
L015b: mov rcx, rbp
L015e: mov rdx, rdi
L0161: call 0x00007ff9b182a050
L0166: lea rcx, [rbp+8]
L016a: mov rdx, rsi
L016d: call 0x00007ff9b182a050
L0172: mov rdx, rbx
L0175: lea rcx, [rsp+0x20]
L017a: mov r8, r14
L017d: call Test+Test+Decision.CompareTo(System.Object, System.Collections.IComparer)
L0182: jmp L0305
L0187: mov rcx, rsi
L018a: mov rdx, 0x7ff953af1320
L0194: cmp [rcx], rdx
L0197: je short L01a4
L0199: mov rcx, rdx
L019c: mov rdx, rsi
L019f: call System.Runtime.CompilerServices.CastHelpers.ChkCastClassSpecial(Void*, System.Object)
L01a4: mov rbx, rdi
L01a7: mov rcx, 0x7ff953af1320
L01b1: cmp [rbx], rcx
L01b4: je short L01c1
L01b6: mov rdx, rdi
L01b9: call System.Runtime.CompilerServices.CastHelpers.ChkCastClassSpecial(Void*, System.Object)
L01be: mov rbx, rax
L01c1: mov rcx, 0x191f2a86fa0
L01cb: mov r14, [rcx]
L01ce: vmovsd xmm1, [rsi+0x18]
L01d3: vmovsd xmm2, [rbx+0x18]
L01d8: vucomisd xmm2, xmm1
L01dc: jbe short L01e5
L01de: mov eax, 0xffffffff
L01e3: jmp short L0206
L01e5: vucomisd xmm1, xmm2
L01e9: jbe short L01f2
L01eb: mov eax, 1
L01f0: jmp short L0206
L01f2: vucomisd xmm1, xmm2
L01f6: jp short L01fe
L01f8: jne short L01fe
L01fa: xor eax, eax
L01fc: jmp short L0206
L01fe: mov rcx, r14
L0201: call Microsoft.FSharp.Core.LanguagePrimitives+HashCompare.GenericComparisonWithComparerIntrinsic[[System.Double, System.Private.CoreLib]](System.Collections.IComparer, Double, Double)
L0206: test eax, eax
L0208: jge short L020f
L020a: jmp L0305
L020f: test eax, eax
L0211: jle short L0218
L0213: jmp L0305
L0218: mov rcx, 0x191f2a86fa0
L0222: mov r14, [rcx]
L0225: mov rcx, [rsi+0x10]
L0229: mov rdx, [rbx+0x10]
L022d: mov r8, r14
L0230: cmp [rcx], ecx
L0232: add rsp, 0x30
L0236: pop rbx
L0237: pop rbp
L0238: pop rsi
L0239: pop rdi
L023a: pop r14
L023c: jmp Test+Test+LinearExpr.CompareTo(System.Object, System.Collections.IComparer)
L0241: mov rcx, rsi
L0244: mov rdx, 0x7ff953af14c8
L024e: cmp [rcx], rdx
L0251: je short L025e
L0253: mov rcx, rdx
L0256: mov rdx, rsi
L0259: call System.Runtime.CompilerServices.CastHelpers.ChkCastClassSpecial(Void*, System.Object)
L025e: mov rbx, rdi
L0261: mov rcx, 0x7ff953af14c8
L026b: cmp [rbx], rcx
L026e: je short L027b
L0270: mov rdx, rdi
L0273: call System.Runtime.CompilerServices.CastHelpers.ChkCastClassSpecial(Void*, System.Object)
L0278: mov rbx, rax
L027b: mov rcx, 0x191f2a86fa0
L0285: mov r14, [rcx]
L0288: mov rcx, [rsi+0x10]
L028c: mov rdx, [rbx+0x10]
L0290: mov r8, r14
L0293: cmp [rcx], ecx
L0295: call Test+Test+LinearExpr.CompareTo(System.Object, System.Collections.IComparer)
L029a: test eax, eax
L029c: jge short L02a0
L029e: jmp short L0305
L02a0: test eax, eax
L02a2: jle short L02a6
L02a4: jmp short L0305
L02a6: mov rcx, 0x191f2a86fa0
L02b0: mov r14, [rcx]
L02b3: mov rcx, [rsi+0x18]
L02b7: mov rdx, [rbx+0x18]
L02bb: mov r8, r14
L02be: cmp [rcx], ecx
L02c0: add rsp, 0x30
L02c4: pop rbx
L02c5: pop rbp
L02c6: pop rsi
L02c7: pop rdi
L02c8: pop r14
L02ca: jmp Test+Test+LinearExpr.CompareTo(System.Object, System.Collections.IComparer)
L02cf: sub eax, ebp
L02d1: jmp short L0305
L02d3: mov eax, 1
L02d8: add rsp, 0x30
L02dc: pop rbx
L02dd: pop rbp
L02de: pop rsi
L02df: pop rdi
L02e0: pop r14
L02e2: ret
L02e3: test rdi, rdi
L02e6: je short L02f8
L02e8: mov eax, 0xffffffff
L02ed: add rsp, 0x30
L02f1: pop rbx
L02f2: pop rbp
L02f3: pop rsi
L02f4: pop rdi
L02f5: pop r14
L02f7: ret
L02f8: xor eax, eax
L02fa: add rsp, 0x30
L02fe: pop rbx
L02ff: pop rbp
L0300: pop rsi
L0301: pop rdi
L0302: pop r14
L0304: ret
L0305: add rsp, 0x30
L0309: pop rbx
L030a: pop rbp
L030b: pop rsi
L030c: pop rdi
L030d: pop r14
L030f: ret

;Test+Test+LinearExpr.CompareTo(System.Object)
L0000: push rsi
L0001: sub rsp, 0x20
L0005: mov rsi, rcx
L0008: mov rax, rdx
L000b: test rax, rax
L000e: je short L0024
L0010: mov rcx, 0x7ff953af0bf0
L001a: cmp [rax], rcx
L001d: je short L0024
L001f: call System.Runtime.CompilerServices.CastHelpers.ChkCastClassSpecial(Void*, System.Object)
L0024: mov rcx, rsi
L0027: mov rdx, rax
L002a: cmp [rcx], ecx
L002c: add rsp, 0x20
L0030: pop rsi
L0031: jmp Test+Test+LinearExpr.CompareTo(LinearExpr)

;Test+Test+LinearExpr.CompareTo(System.Object, System.Collections.IComparer)
L0000: push r14
L0002: push rdi
L0003: push rsi
L0004: push rbp
L0005: push rbx
L0006: sub rsp, 0x30
L000a: vzeroupper
L000d: xor eax, eax
L000f: mov [rsp+0x20], rax
L0014: mov [rsp+0x28], rax
L0019: mov rsi, rdx
L001c: mov rdi, r8
L001f: mov rbx, rcx
L0022: mov rbp, rsi
L0025: test rbp, rbp
L0028: je short L0045
L002a: mov rcx, 0x7ff953af0bf0
L0034: cmp [rbp], rcx
L0038: je short L0045
L003a: mov rdx, rsi
L003d: call System.Runtime.CompilerServices.CastHelpers.ChkCastClassSpecial(Void*, System.Object)
L0042: mov rbp, rax
L0045: test rbx, rbx
L0048: je L02d5
L004e: test rbp, rbp
L0051: je short L006b
L0053: mov rcx, 0x7ff953af0bf0
L005d: cmp [rbp], rcx
L0061: je short L006b
L0063: mov rdx, rbp
L0066: call System.Runtime.CompilerServices.CastHelpers.ChkCastClassSpecial(Void*, System.Object)
L006b: test rbp, rbp
L006e: je L02c5
L0074: mov r14d, [rbx+8]
L0078: mov eax, r14d
L007b: mov esi, [rbp+8]
L007e: cmp eax, esi
L0080: jne L02c1
L0086: cmp r14d, 3
L008a: ja L01ad
L0090: mov ecx, r14d
L0093: lea rdx, [Test+Test+LinearExpr.CompareTo(System.Object, System.Collections.IComparer)]
L009a: mov edx, [rdx+rcx*4]
L009d: lea rax, [L001f]
L00a4: add rdx, rax
L00a7: jmp rdx
L00a9: mov rsi, rbx
L00ac: mov rcx, 0x7ff953af1320
L00b6: cmp [rsi], rcx
L00b9: je short L00c6
L00bb: mov rdx, rbx
L00be: call System.Runtime.CompilerServices.CastHelpers.ChkCastClassSpecial(Void*, System.Object)
L00c3: mov rsi, rax
L00c6: mov rbx, rbp
L00c9: mov rcx, 0x7ff953af1320
L00d3: cmp [rbx], rcx
L00d6: je short L00e3
L00d8: mov rdx, rbp
L00db: call System.Runtime.CompilerServices.CastHelpers.ChkCastClassSpecial(Void*, System.Object)
L00e0: mov rbx, rax
L00e3: vmovsd xmm1, [rsi+0x18]
L00e8: vmovsd xmm2, [rbx+0x18]
L00ed: vucomisd xmm2, xmm1
L00f1: jbe short L00fa
L00f3: mov eax, 0xffffffff
L00f8: jmp short L011b
L00fa: vucomisd xmm1, xmm2
L00fe: jbe short L0107
L0100: mov eax, 1
L0105: jmp short L011b
L0107: vucomisd xmm1, xmm2
L010b: jp short L0113
L010d: jne short L0113
L010f: xor eax, eax
L0111: jmp short L011b
L0113: mov rcx, rdi
L0116: call Microsoft.FSharp.Core.LanguagePrimitives+HashCompare.GenericComparisonWithComparerIntrinsic[[System.Double, System.Private.CoreLib]](System.Collections.IComparer, Double, Double)
L011b: test eax, eax
L011d: jl L02b9
L0123: test eax, eax
L0125: jg L02bb
L012b: mov rcx, [rsi+0x10]
L012f: mov rdx, [rbx+0x10]
L0133: mov rsi, rdx
L0136: mov rbx, rcx
L0139: jmp L0022
L013e: mov rsi, rbx
L0141: mov rcx, 0x7ff953af14c8
L014b: cmp [rsi], rcx
L014e: je short L015b
L0150: mov rdx, rbx
L0153: call System.Runtime.CompilerServices.CastHelpers.ChkCastClassSpecial(Void*, System.Object)
L0158: mov rsi, rax
L015b: mov rbx, rbp
L015e: mov rcx, 0x7ff953af14c8
L0168: cmp [rbx], rcx
L016b: je short L0178
L016d: mov rdx, rbp
L0170: call System.Runtime.CompilerServices.CastHelpers.ChkCastClassSpecial(Void*, System.Object)
L0175: mov rbx, rax
L0178: mov rcx, [rsi+0x10]
L017c: mov rdx, [rbx+0x10]
L0180: mov r8, rdi
L0183: cmp [rcx], ecx
L0185: call Test+Test+LinearExpr.CompareTo(System.Object, System.Collections.IComparer)
L018a: test eax, eax
L018c: jl L02bd
L0192: test eax, eax
L0194: jg L02bf
L019a: mov rcx, [rsi+0x18]
L019e: mov rdx, [rbx+0x18]
L01a2: mov rsi, rdx
L01a5: mov rbx, rcx
L01a8: jmp L0022
L01ad: mov rsi, rbx
L01b0: mov rcx, 0x7ff953af0ff8
L01ba: cmp [rsi], rcx
L01bd: je short L01ca
L01bf: mov rdx, rbx
L01c2: call System.Runtime.CompilerServices.CastHelpers.ChkCastClassSpecial(Void*, System.Object)
L01c7: mov rsi, rax
L01ca: mov rax, rbp
L01cd: mov rcx, 0x7ff953af0ff8
L01d7: cmp [rax], rcx
L01da: je short L01e4
L01dc: mov rdx, rbp
L01df: call System.Runtime.CompilerServices.CastHelpers.ChkCastClassSpecial(Void*, System.Object)
L01e4: vmovsd xmm1, [rsi+0x10]
L01e9: vmovsd xmm2, [rax+0x10]
L01ee: vucomisd xmm2, xmm1
L01f2: ja L02f9
L01f8: vucomisd xmm1, xmm2
L01fc: ja L02c5
L0202: vucomisd xmm1, xmm2
L0206: jp short L020e
L0208: je L0309
L020e: mov rcx, rdi
L0211: add rsp, 0x30
L0215: pop rbx
L0216: pop rbp
L0217: pop rsi
L0218: pop rdi
L0219: pop r14
L021b: jmp Microsoft.FSharp.Core.LanguagePrimitives+HashCompare.GenericComparisonWithComparerIntrinsic[[System.Double, System.Private.CoreLib]](System.Collections.IComparer, Double, Double)
L0220: mov rsi, rbx
L0223: mov rcx, 0x7ff953af1178
L022d: cmp [rsi], rcx
L0230: je short L023d
L0232: mov rdx, rbx
L0235: call System.Runtime.CompilerServices.CastHelpers.ChkCastClassSpecial(Void*, System.Object)
L023a: mov rsi, rax
L023d: mov rbx, rbp
L0240: mov rcx, 0x7ff953af1178
L024a: cmp [rbx], rcx
L024d: je short L025a
L024f: mov rdx, rbp
L0252: call System.Runtime.CompilerServices.CastHelpers.ChkCastClassSpecial(Void*, System.Object)
L0257: mov rbx, rax
L025a: add rsi, 0x10
L025e: mov rcx, [rsi]
L0261: mov [rsp+0x20], rcx
L0266: mov rcx, [rsi+8]
L026a: mov [rsp+0x28], rcx
L026f: add rbx, 0x10
L0273: mov rsi, [rbx]
L0276: mov rbx, [rbx+8]
L027a: mov rcx, 0x7ff953af0538
L0284: call 0x00007ff9b182a470
L0289: mov rbp, rax
L028c: lea r14, [rbp+8]
L0290: mov rcx, r14
L0293: mov rdx, rsi
L0296: call 0x00007ff9b182a050
L029b: lea rcx, [r14+8]
L029f: mov rdx, rbx
L02a2: call 0x00007ff9b182a050
L02a7: mov rdx, rbp
L02aa: lea rcx, [rsp+0x20]
L02af: mov r8, rdi
L02b2: call Test+Test+Decision.CompareTo(System.Object, System.Collections.IComparer)
L02b7: jmp short L0316
L02b9: jmp short L0316
L02bb: jmp short L0316
L02bd: jmp short L0316
L02bf: jmp short L0316
L02c1: sub eax, esi
L02c3: jmp short L0316
L02c5: mov eax, 1
L02ca: add rsp, 0x30
L02ce: pop rbx
L02cf: pop rbp
L02d0: pop rsi
L02d1: pop rdi
L02d2: pop r14
L02d4: ret
L02d5: mov rax, rsi
L02d8: test rax, rax
L02db: je short L02f4
L02dd: mov rcx, 0x7ff953af0bf0
L02e7: cmp [rax], rcx
L02ea: je short L02f4
L02ec: mov rdx, rsi
L02ef: call System.Runtime.CompilerServices.CastHelpers.ChkCastClassSpecial(Void*, System.Object)
L02f4: test rax, rax
L02f7: je short L0309
L02f9: mov eax, 0xffffffff
L02fe: add rsp, 0x30
L0302: pop rbx
L0303: pop rbp
L0304: pop rsi
L0305: pop rdi
L0306: pop r14
L0308: ret
L0309: xor eax, eax
L030b: add rsp, 0x30
L030f: pop rbx
L0310: pop rbp
L0311: pop rsi
L0312: pop rdi
L0313: pop r14
L0315: ret
L0316: add rsp, 0x30
L031a: pop rbx
L031b: pop rbp
L031c: pop rsi
L031d: pop rdi
L031e: pop r14
L0320: ret

;Test+Test+LinearExpr.GetHashCode(System.Collections.IEqualityComparer)
L0000: push rdi
L0001: push rsi
L0002: push rbp
L0003: push rbx
L0004: sub rsp, 0x38
L0008: vzeroupper
L000b: xor eax, eax
L000d: mov [rsp+0x28], rax
L0012: mov [rsp+0x30], rax
L0017: mov rsi, rcx
L001a: mov rdi, rdx
L001d: test rsi, rsi
L0020: je L017b
L0026: mov ebx, [rsi+8]
L0029: cmp ebx, 3
L002c: ja short L0046
L002e: mov ecx, ebx
L0030: lea rdx, [Test+Test+LinearExpr.GetHashCode(System.Collections.IEqualityComparer)]
L0037: mov edx, [rdx+rcx*4]
L003a: lea rax, [L001d]
L0041: add rdx, rax
L0044: jmp rdx
L0046: mov rcx, rsi
L0049: mov rdx, 0x7ff953af0ff8
L0053: cmp [rcx], rdx
L0056: je short L0063
L0058: mov rcx, rdx
L005b: mov rdx, rsi
L005e: call System.Runtime.CompilerServices.CastHelpers.ChkCastClassSpecial(Void*, System.Object)
L0063: vmovsd xmm1, [rsi+0x10]
L0068: mov rcx, rdi
L006b: call Microsoft.FSharp.Core.LanguagePrimitives+HashCompare.GenericHashWithComparerIntrinsic[[System.Double, System.Private.CoreLib]](System.Collections.IEqualityComparer, Double)
L0070: lea ebp, [rax-0x61c88647]
L0076: mov eax, ebp
L0078: jmp L0172
L007d: mov rcx, rsi
L0080: mov rdx, 0x7ff953af1178
L008a: cmp [rcx], rdx
L008d: je short L009a
L008f: mov rcx, rdx
L0092: mov rdx, rsi
L0095: call System.Runtime.CompilerServices.CastHelpers.ChkCastClassSpecial(Void*, System.Object)
L009a: add rsi, 0x10
L009e: mov rcx, [rsi]
L00a1: mov [rsp+0x28], rcx
L00a6: mov rcx, [rsi+8]
L00aa: mov [rsp+0x30], rcx
L00af: lea rcx, [rsp+0x28]
L00b4: mov rdx, rdi
L00b7: call Test+Test+Decision.GetHashCode(System.Collections.IEqualityComparer)
L00bc: lea ebp, [rax-0x61c88607]
L00c2: mov eax, ebp
L00c4: jmp L0172
L00c9: mov rcx, rsi
L00cc: mov rdx, 0x7ff953af1320
L00d6: cmp [rcx], rdx
L00d9: je short L00e6
L00db: mov rcx, rdx
L00de: mov rdx, rsi
L00e1: call System.Runtime.CompilerServices.CastHelpers.ChkCastClassSpecial(Void*, System.Object)
L00e6: mov rcx, [rsi+0x10]
L00ea: mov rdx, rdi
L00ed: cmp [rcx], ecx
L00ef: call Test+Test+LinearExpr.GetHashCode(System.Collections.IEqualityComparer)
L00f4: lea ebp, [rax-0x61c885c7]
L00fa: vmovsd xmm1, [rsi+0x18]
L00ff: mov rcx, rdi
L0102: call Microsoft.FSharp.Core.LanguagePrimitives+HashCompare.GenericHashWithComparerIntrinsic[[System.Double, System.Private.CoreLib]](System.Collections.IEqualityComparer, Double)
L0107: mov ecx, ebp
L0109: shl ecx, 6
L010c: add ecx, eax
L010e: mov edx, ebp
L0110: sar edx, 2
L0113: lea ebp, [rcx+rdx-0x61c88647]
L011a: mov eax, ebp
L011c: jmp short L0172
L011e: mov rcx, rsi
L0121: mov rdx, 0x7ff953af14c8
L012b: cmp [rcx], rdx
L012e: je short L013b
L0130: mov rcx, rdx
L0133: mov rdx, rsi
L0136: call System.Runtime.CompilerServices.CastHelpers.ChkCastClassSpecial(Void*, System.Object)
L013b: mov rcx, [rsi+0x18]
L013f: mov rdx, rdi
L0142: cmp [rcx], ecx
L0144: call Test+Test+LinearExpr.GetHashCode(System.Collections.IEqualityComparer)
L0149: lea ebp, [rax-0x61c88587]
L014f: mov rcx, [rsi+0x10]
L0153: mov rdx, rdi
L0156: cmp [rcx], ecx
L0158: call Test+Test+LinearExpr.GetHashCode(System.Collections.IEqualityComparer)
L015d: mov edx, ebp
L015f: shl edx, 6
L0162: add eax, edx
L0164: mov edx, ebp
L0166: sar edx, 2
L0169: lea ebp, [rax+rdx-0x61c88647]
L0170: mov eax, ebp
L0172: add rsp, 0x38
L0176: pop rbx
L0177: pop rbp
L0178: pop rsi
L0179: pop rdi
L017a: ret
L017b: xor eax, eax
L017d: add rsp, 0x38
L0181: pop rbx
L0182: pop rbp
L0183: pop rsi
L0184: pop rdi
L0185: ret

;Test+Test+LinearExpr.GetHashCode()
L0000: mov rdx, 0x191f2a86fa8
L000a: mov rdx, [rdx]
L000d: cmp [rcx], ecx
L000f: jmp Test+Test+LinearExpr.GetHashCode(System.Collections.IEqualityComparer)

;Test+Test+LinearExpr.Equals(System.Object, System.Collections.IEqualityComparer)
L0000: push r14
L0002: push rdi
L0003: push rsi
L0004: push rbp
L0005: push rbx
L0006: sub rsp, 0x30
L000a: vzeroupper
L000d: xor eax, eax
L000f: mov [rsp+0x20], rax
L0014: mov [rsp+0x28], rax
L0019: mov rsi, r8
L001c: mov rdi, rcx
L001f: test rdi, rdi
L0022: je L023a
L0028: mov rcx, 0x7ff953af0bf0
L0032: call System.Runtime.CompilerServices.CastHelpers.IsInstanceOfClass(Void*, System.Object)
L0037: mov rbx, rax
L003a: test rbx, rbx
L003d: je L022d
L0043: mov ebp, [rdi+8]
L0046: mov ecx, ebp
L0048: mov edx, [rbx+8]
L004b: cmp ecx, edx
L004d: jne L022d
L0053: cmp ebp, 3
L0056: ja L013a
L005c: mov ecx, ebp
L005e: lea rdx, [Test+Test+LinearExpr.Equals(System.Object, System.Collections.IEqualityComparer)]
L0065: mov edx, [rdx+rcx*4]
L0068: lea rax, [L001c]
L006f: add rdx, rax
L0072: jmp rdx
L0074: mov r14, rdi
L0077: mov rcx, 0x7ff953af1320
L0081: cmp [r14], rcx
L0084: je short L0091
L0086: mov rdx, rdi
L0089: call System.Runtime.CompilerServices.CastHelpers.ChkCastClassSpecial(Void*, System.Object)
L008e: mov r14, rax
L0091: mov rdi, rbx
L0094: mov rcx, 0x7ff953af1320
L009e: cmp [rdi], rcx
L00a1: je short L00ae
L00a3: mov rdx, rbx
L00a6: call System.Runtime.CompilerServices.CastHelpers.ChkCastClassSpecial(Void*, System.Object)
L00ab: mov rdi, rax
L00ae: vmovsd xmm0, [r14+0x18]
L00b4: vucomisd xmm0, [rdi+0x18]
L00b9: jp L022d
L00bf: jne L022d
L00c5: mov rcx, [r14+0x10]
L00c9: mov rdx, [rdi+0x10]
L00cd: mov rdi, rcx
L00d0: jmp L001f
L00d5: mov rbp, rdi
L00d8: mov rcx, 0x7ff953af14c8
L00e2: cmp [rbp], rcx
L00e6: je short L00f3
L00e8: mov rdx, rdi
L00eb: call System.Runtime.CompilerServices.CastHelpers.ChkCastClassSpecial(Void*, System.Object)
L00f0: mov rbp, rax
L00f3: mov rdi, rbx
L00f6: mov rcx, 0x7ff953af14c8
L0100: cmp [rdi], rcx
L0103: je short L0110
L0105: mov rdx, rbx
L0108: call System.Runtime.CompilerServices.CastHelpers.ChkCastClassSpecial(Void*, System.Object)
L010d: mov rdi, rax
L0110: mov rcx, [rbp+0x10]
L0114: mov rdx, [rdi+0x10]
L0118: mov r8, rsi
L011b: cmp [rcx], ecx
L011d: call Test+Test+LinearExpr.Equals(System.Object, System.Collections.IEqualityComparer)
L0122: test eax, eax
L0124: je L022d
L012a: mov rcx, [rbp+0x18]
L012e: mov rdx, [rdi+0x18]
L0132: mov rdi, rcx
L0135: jmp L001f
L013a: mov rbp, rdi
L013d: mov rcx, 0x7ff953af0ff8
L0147: cmp [rbp], rcx
L014b: je short L0158
L014d: mov rdx, rdi
L0150: call System.Runtime.CompilerServices.CastHelpers.ChkCastClassSpecial(Void*, System.Object)
L0155: mov rbp, rax
L0158: mov rax, rbx
L015b: mov rcx, 0x7ff953af0ff8
L0165: cmp [rax], rcx
L0168: je short L0172
L016a: mov rdx, rbx
L016d: call System.Runtime.CompilerServices.CastHelpers.ChkCastClassSpecial(Void*, System.Object)
L0172: vmovsd xmm0, [rbp+0x10]
L0177: vucomisd xmm0, [rax+0x10]
L017c: setnp r14b
L0180: jp short L0186
L0182: sete r14b
L0186: movzx r14d, r14b
L018a: jmp L0245
L018f: mov rbp, rdi
L0192: mov rcx, 0x7ff953af1178
L019c: cmp [rbp], rcx
L01a0: je short L01ad
L01a2: mov rdx, rdi
L01a5: call System.Runtime.CompilerServices.CastHelpers.ChkCastClassSpecial(Void*, System.Object)
L01aa: mov rbp, rax
L01ad: mov rdi, rbx
L01b0: mov rcx, 0x7ff953af1178
L01ba: cmp [rdi], rcx
L01bd: je short L01ca
L01bf: mov rdx, rbx
L01c2: call System.Runtime.CompilerServices.CastHelpers.ChkCastClassSpecial(Void*, System.Object)
L01c7: mov rdi, rax
L01ca: add rbp, 0x10
L01ce: mov rcx, [rbp]
L01d2: mov [rsp+0x20], rcx
L01d7: mov rcx, [rbp+8]
L01db: mov [rsp+0x28], rcx
L01e0: add rdi, 0x10
L01e4: mov rbx, [rdi]
L01e7: mov rdi, [rdi+8]
L01eb: mov rcx, 0x7ff953af0538
L01f5: call 0x00007ff9b182a470
L01fa: mov rbp, rax
L01fd: lea r14, [rbp+8]
L0201: mov rcx, r14
L0204: mov rdx, rbx
L0207: call 0x00007ff9b182a050
L020c: lea rcx, [r14+8]
L0210: mov rdx, rdi
L0213: call 0x00007ff9b182a050
L0218: mov rdx, rbp
L021b: lea rcx, [rsp+0x20]
L0220: mov r8, rsi
L0223: call Test+Test+Decision.Equals(System.Object, System.Collections.IEqualityComparer)
L0228: mov r14d, eax
L022b: jmp short L0245
L022d: xor eax, eax
L022f: add rsp, 0x30
L0233: pop rbx
L0234: pop rbp
L0235: pop rsi
L0236: pop rdi
L0237: pop r14
L0239: ret
L023a: test rdx, rdx
L023d: sete r14b
L0241: movzx r14d, r14b
L0245: movzx eax, r14b
L0249: add rsp, 0x30
L024d: pop rbx
L024e: pop rbp
L024f: pop rsi
L0250: pop rdi
L0251: pop r14
L0253: ret

;Test+Test+LinearExpr.Equals(LinearExpr)
L0000: push rdi
L0001: push rsi
L0002: push rbx
L0003: sub rsp, 0x40
L0007: vzeroupper
L000a: vmovaps [rsp+0x30], xmm6
L0010: vmovaps [rsp+0x20], xmm7
L0016: mov rsi, rdx
L0019: mov rdx, rcx
L001c: test rdx, rdx
L001f: je L022c
L0025: test rsi, rsi
L0028: je L0216
L002e: mov edi, [rdx+8]
L0031: mov ecx, edi
L0033: mov eax, [rsi+8]
L0036: cmp ecx, eax
L0038: jne L0216
L003e: cmp edi, 3
L0041: ja L012e
L0047: mov ecx, edi
L0049: lea rax, [Test+Test+LinearExpr.Equals(LinearExpr)]
L0050: mov eax, [rax+rcx*4]
L0053: lea r8, [L0019]
L005a: add rax, r8
L005d: jmp rax
L005f: mov rbx, rdx
L0062: mov rcx, 0x7ff953af1320
L006c: cmp [rbx], rcx
L006f: je short L0079
L0071: call System.Runtime.CompilerServices.CastHelpers.ChkCastClassSpecial(Void*, System.Object)
L0076: mov rbx, rax
L0079: mov rdi, rsi
L007c: mov rcx, 0x7ff953af1320
L0086: cmp [rdi], rcx
L0089: je short L0096
L008b: mov rdx, rsi
L008e: call System.Runtime.CompilerServices.CastHelpers.ChkCastClassSpecial(Void*, System.Object)
L0093: mov rdi, rax
L0096: vmovsd xmm6, [rbx+0x18]
L009b: vmovsd xmm7, [rdi+0x18]
L00a0: vucomisd xmm6, xmm7
L00a4: jp short L00a8
L00a6: je short L00c6
L00a8: vucomisd xmm6, xmm6
L00ac: jp short L00b4
L00ae: je L0216
L00b4: vucomisd xmm7, xmm7
L00b8: setp cl
L00bb: movzx ecx, cl
L00be: test ecx, ecx
L00c0: je L0216
L00c6: mov rdx, [rbx+0x10]
L00ca: mov rsi, [rdi+0x10]
L00ce: jmp L001c
L00d3: mov rdi, rdx
L00d6: mov rcx, 0x7ff953af14c8
L00e0: cmp [rdi], rcx
L00e3: je short L00ed
L00e5: call System.Runtime.CompilerServices.CastHelpers.ChkCastClassSpecial(Void*, System.Object)
L00ea: mov rdi, rax
L00ed: mov rbx, rsi
L00f0: mov rcx, 0x7ff953af14c8
L00fa: cmp [rbx], rcx
L00fd: je short L010a
L00ff: mov rdx, rsi
L0102: call System.Runtime.CompilerServices.CastHelpers.ChkCastClassSpecial(Void*, System.Object)
L0107: mov rbx, rax
L010a: mov rcx, [rdi+0x10]
L010e: mov rdx, [rbx+0x10]
L0112: cmp [rcx], ecx
L0114: call Test+Test+LinearExpr.Equals(LinearExpr)
L0119: test eax, eax
L011b: je L0216
L0121: mov rdx, [rdi+0x18]
L0125: mov rsi, [rbx+0x18]
L0129: jmp L001c
L012e: mov rdi, rdx
L0131: mov rcx, 0x7ff953af0ff8
L013b: cmp [rdi], rcx
L013e: je short L0148
L0140: call System.Runtime.CompilerServices.CastHelpers.ChkCastClassSpecial(Void*, System.Object)
L0145: mov rdi, rax
L0148: mov rax, rsi
L014b: mov rcx, 0x7ff953af0ff8
L0155: cmp [rax], rcx
L0158: je short L0162
L015a: mov rdx, rsi
L015d: call System.Runtime.CompilerServices.CastHelpers.ChkCastClassSpecial(Void*, System.Object)
L0162: vmovsd xmm6, [rdi+0x10]
L0167: vmovsd xmm7, [rax+0x10]
L016c: vucomisd xmm6, xmm7
L0170: jp short L018d
L0172: jne short L018d
L0174: mov eax, 1
L0179: vmovaps xmm6, [rsp+0x30]
L017f: vmovaps xmm7, [rsp+0x20]
L0185: add rsp, 0x40
L0189: pop rbx
L018a: pop rsi
L018b: pop rdi
L018c: ret
L018d: vucomisd xmm6, xmm6
L0191: jp short L0199
L0193: je L0216
L0199: vucomisd xmm7, xmm7
L019d: setp bl
L01a0: movzx ebx, bl
L01a3: jmp L0235
L01a8: mov rdi, rdx
L01ab: mov rcx, 0x7ff953af1178
L01b5: cmp [rdi], rcx
L01b8: je short L01c2
L01ba: call System.Runtime.CompilerServices.CastHelpers.ChkCastClassSpecial(Void*, System.Object)
L01bf: mov rdi, rax
L01c2: mov rbx, rsi
L01c5: mov rcx, 0x7ff953af1178
L01cf: cmp [rbx], rcx
L01d2: je short L01df
L01d4: mov rdx, rsi
L01d7: call System.Runtime.CompilerServices.CastHelpers.ChkCastClassSpecial(Void*, System.Object)
L01dc: mov rbx, rax
L01df: add rdi, 0x10
L01e3: mov rcx, [rdi]
L01e6: mov rsi, [rdi+8]
L01ea: add rbx, 0x10
L01ee: mov rdx, [rbx]
L01f1: mov rdi, [rbx+8]
L01f5: cmp [rcx], ecx
L01f7: call Test+Test+DecisionName.Equals(DecisionName)
L01fc: test eax, eax
L01fe: je short L0212
L0200: mov rcx, rsi
L0203: mov rdx, rdi
L0206: cmp [rcx], ecx
L0208: call Test+Test+DecisionType.Equals(DecisionType)
L020d: movzx ebx, al
L0210: jmp short L0214
L0212: xor ebx, ebx
L0214: jmp short L0235
L0216: xor eax, eax
L0218: vmovaps xmm6, [rsp+0x30]
L021e: vmovaps xmm7, [rsp+0x20]
L0224: add rsp, 0x40
L0228: pop rbx
L0229: pop rsi
L022a: pop rdi
L022b: ret
L022c: test rsi, rsi
L022f: sete bl
L0232: movzx ebx, bl
L0235: movzx eax, bl
L0238: vmovaps xmm6, [rsp+0x30]
L023e: vmovaps xmm7, [rsp+0x20]
L0244: add rsp, 0x40
L0248: pop rbx
L0249: pop rsi
L024a: pop rdi
L024b: ret

;Test+Test+LinearExpr.Equals(System.Object)
L0000: push rsi
L0001: sub rsp, 0x20
L0005: mov rsi, rcx
L0008: mov rcx, 0x7ff953af0bf0
L0012: call System.Runtime.CompilerServices.CastHelpers.IsInstanceOfClass(Void*, System.Object)
L0017: test rax, rax
L001a: je short L002e
L001c: mov rcx, rsi
L001f: mov rdx, rax
L0022: cmp [rcx], ecx
L0024: add rsp, 0x20
L0028: pop rsi
L0029: jmp Test+Test+LinearExpr.Equals(LinearExpr)
L002e: xor eax, eax
L0030: add rsp, 0x20
L0034: pop rsi
L0035: ret

;Test+Test+LinearExpr+Decision.get_Item()
L0000: push rdi
L0001: push rsi
L0002: push rbx
L0003: mov rbx, rdx
L0006: lea rsi, [rcx+0x10]
L000a: mov rdi, rbx
L000d: call 0x00007ff9b182a130
L0012: call 0x00007ff9b182a130
L0017: mov rax, rbx
L001a: pop rbx
L001b: pop rsi
L001c: pop rdi
L001d: ret

;Test+Test+LinearExpr.get_IsFloat()
L0000: cmp dword ptr [rcx+8], 0
L0004: sete al
L0007: movzx eax, al
L000a: ret

;Test+Test+LinearExpr.get_IsDecision()
L0000: cmp dword ptr [rcx+8], 1
L0004: sete al
L0007: movzx eax, al
L000a: ret

;Test+Test+LinearExpr.get_IsScale()
L0000: cmp dword ptr [rcx+8], 2
L0004: sete al
L0007: movzx eax, al
L000a: ret

;Test+Test+LinearExpr.get_IsAdd()
L0000: cmp dword ptr [rcx+8], 3
L0004: sete al
L0007: movzx eax, al
L000a: ret

;Test+Test+LinearExpr.get_Tag()
L0000: mov eax, [rcx+8]
L0003: ret

;Test+Test+LinearExpr.ToString()
L0000: push rdi
L0001: push rsi
L0002: sub rsp, 0x28
L0006: mov rsi, rcx
L0009: mov rcx, 0x7ff953af84a0
L0013: call 0x00007ff9b182a470
L0018: mov rdi, rax
L001b: mov rdx, 0x191f2a9b3f0
L0025: mov rdx, [rdx]
L0028: lea rcx, [rdi+8]
L002c: call 0x00007ff9b182a080
L0031: xor edx, edx
L0033: mov [rdi+0x10], rdx
L0037: mov [rdi+0x18], rdx
L003b: mov rdx, rdi
L003e: mov rcx, 0x7ff953af8780
L0048: call Microsoft.FSharp.Core.PrintfModule.PrintFormatToStringThen[[System.__Canon, System.Private.CoreLib]](Microsoft.FSharp.Core.PrintfFormat`4<System.__Canon,Microsoft.FSharp.Core.Unit,System.String,System.String>)
L004d: mov rcx, rax
L0050: mov rdx, rsi
L0053: mov rax, [rax]
L0056: mov rax, [rax+0x40]
L005a: mov rax, [rax+0x20]
L005e: add rsp, 0x28
L0062: pop rsi
L0063: pop rdi
L0064: jmp rax

;Test+Test+LinearExpr.CompareTo(LinearExpr)
L0000: push r14
L0002: push rdi
L0003: push rsi
L0004: push rbp
L0005: push rbx
L0006: sub rsp, 0x30
L000a: vzeroupper
L000d: xor eax, eax
L000f: mov [rsp+0x20], rax
L0014: mov [rsp+0x28], rax
L0019: mov rsi, rcx
L001c: mov rdi, rdx
L001f: test rsi, rsi
L0022: je L02e3
L0028: test rdi, rdi
L002b: je L02d3
L0031: mov ebx, [rsi+8]
L0034: mov eax, ebx
L0036: mov ebp, [rdi+8]
L0039: cmp eax, ebp
L003b: jne L02cf
L0041: cmp ebx, 3
L0044: ja short L005e
L0046: mov ecx, ebx
L0048: lea rdx, [Test+Test+LinearExpr.CompareTo(LinearExpr)]
L004f: mov edx, [rdx+rcx*4]
L0052: lea rax, [L001f]
L0059: add rdx, rax
L005c: jmp rdx
L005e: mov rcx, rsi
L0061: mov rdx, 0x7ff953af0ff8
L006b: cmp [rcx], rdx
L006e: je short L007b
L0070: mov rcx, rdx
L0073: mov rdx, rsi
L0076: call System.Runtime.CompilerServices.CastHelpers.ChkCastClassSpecial(Void*, System.Object)
L007b: mov rax, rdi
L007e: mov rcx, 0x7ff953af0ff8
L0088: cmp [rax], rcx
L008b: je short L0095
L008d: mov rdx, rdi
L0090: call System.Runtime.CompilerServices.CastHelpers.ChkCastClassSpecial(Void*, System.Object)
L0095: mov rcx, 0x191f2a86fa0
L009f: mov r14, [rcx]
L00a2: vmovsd xmm1, [rsi+0x10]
L00a7: vmovsd xmm2, [rax+0x10]
L00ac: vucomisd xmm2, xmm1
L00b0: ja L02e8
L00b6: vucomisd xmm1, xmm2
L00ba: ja L02d3
L00c0: vucomisd xmm1, xmm2
L00c4: jp short L00cc
L00c6: je L02f8
L00cc: mov rcx, r14
L00cf: add rsp, 0x30
L00d3: pop rbx
L00d4: pop rbp
L00d5: pop rsi
L00d6: pop rdi
L00d7: pop r14
L00d9: jmp Microsoft.FSharp.Core.LanguagePrimitives+HashCompare.GenericComparisonWithComparerIntrinsic[[System.Double, System.Private.CoreLib]](System.Collections.IComparer, Double, Double)
L00de: mov rcx, rsi
L00e1: mov rdx, 0x7ff953af1178
L00eb: cmp [rcx], rdx
L00ee: je short L00fb
L00f0: mov rcx, rdx
L00f3: mov rdx, rsi
L00f6: call System.Runtime.CompilerServices.CastHelpers.ChkCastClassSpecial(Void*, System.Object)
L00fb: mov rbx, rdi
L00fe: mov rcx, 0x7ff953af1178
L0108: cmp [rbx], rcx
L010b: je short L0118
L010d: mov rdx, rdi
L0110: call System.Runtime.CompilerServices.CastHelpers.ChkCastClassSpecial(Void*, System.Object)
L0115: mov rbx, rax
L0118: mov rcx, 0x191f2a86fa0
L0122: mov r14, [rcx]
L0125: add rsi, 0x10
L0129: mov rcx, [rsi]
L012c: mov [rsp+0x20], rcx
L0131: mov rcx, [rsi+8]
L0135: mov [rsp+0x28], rcx
L013a: add rbx, 0x10
L013e: mov rdi, [rbx]
L0141: mov rsi, [rbx+8]
L0145: mov rcx, 0x7ff953af0538
L014f: call 0x00007ff9b182a470
L0154: mov rbx, rax
L0157: lea rbp, [rbx+8]
L015b: mov rcx, rbp
L015e: mov rdx, rdi
L0161: call 0x00007ff9b182a050
L0166: lea rcx, [rbp+8]
L016a: mov rdx, rsi
L016d: call 0x00007ff9b182a050
L0172: mov rdx, rbx
L0175: lea rcx, [rsp+0x20]
L017a: mov r8, r14
L017d: call Test+Test+Decision.CompareTo(System.Object, System.Collections.IComparer)
L0182: jmp L0305
L0187: mov rcx, rsi
L018a: mov rdx, 0x7ff953af1320
L0194: cmp [rcx], rdx
L0197: je short L01a4
L0199: mov rcx, rdx
L019c: mov rdx, rsi
L019f: call System.Runtime.CompilerServices.CastHelpers.ChkCastClassSpecial(Void*, System.Object)
L01a4: mov rbx, rdi
L01a7: mov rcx, 0x7ff953af1320
L01b1: cmp [rbx], rcx
L01b4: je short L01c1
L01b6: mov rdx, rdi
L01b9: call System.Runtime.CompilerServices.CastHelpers.ChkCastClassSpecial(Void*, System.Object)
L01be: mov rbx, rax
L01c1: mov rcx, 0x191f2a86fa0
L01cb: mov r14, [rcx]
L01ce: vmovsd xmm1, [rsi+0x18]
L01d3: vmovsd xmm2, [rbx+0x18]
L01d8: vucomisd xmm2, xmm1
L01dc: jbe short L01e5
L01de: mov eax, 0xffffffff
L01e3: jmp short L0206
L01e5: vucomisd xmm1, xmm2
L01e9: jbe short L01f2
L01eb: mov eax, 1
L01f0: jmp short L0206
L01f2: vucomisd xmm1, xmm2
L01f6: jp short L01fe
L01f8: jne short L01fe
L01fa: xor eax, eax
L01fc: jmp short L0206
L01fe: mov rcx, r14
L0201: call Microsoft.FSharp.Core.LanguagePrimitives+HashCompare.GenericComparisonWithComparerIntrinsic[[System.Double, System.Private.CoreLib]](System.Collections.IComparer, Double, Double)
L0206: test eax, eax
L0208: jge short L020f
L020a: jmp L0305
L020f: test eax, eax
L0211: jle short L0218
L0213: jmp L0305
L0218: mov rcx, 0x191f2a86fa0
L0222: mov r14, [rcx]
L0225: mov rcx, [rsi+0x10]
L0229: mov rdx, [rbx+0x10]
L022d: mov r8, r14
L0230: cmp [rcx], ecx
L0232: add rsp, 0x30
L0236: pop rbx
L0237: pop rbp
L0238: pop rsi
L0239: pop rdi
L023a: pop r14
L023c: jmp Test+Test+LinearExpr.CompareTo(System.Object, System.Collections.IComparer)
L0241: mov rcx, rsi
L0244: mov rdx, 0x7ff953af14c8
L024e: cmp [rcx], rdx
L0251: je short L025e
L0253: mov rcx, rdx
L0256: mov rdx, rsi
L0259: call System.Runtime.CompilerServices.CastHelpers.ChkCastClassSpecial(Void*, System.Object)
L025e: mov rbx, rdi
L0261: mov rcx, 0x7ff953af14c8
L026b: cmp [rbx], rcx
L026e: je short L027b
L0270: mov rdx, rdi
L0273: call System.Runtime.CompilerServices.CastHelpers.ChkCastClassSpecial(Void*, System.Object)
L0278: mov rbx, rax
L027b: mov rcx, 0x191f2a86fa0
L0285: mov r14, [rcx]
L0288: mov rcx, [rsi+0x10]
L028c: mov rdx, [rbx+0x10]
L0290: mov r8, r14
L0293: cmp [rcx], ecx
L0295: call Test+Test+LinearExpr.CompareTo(System.Object, System.Collections.IComparer)
L029a: test eax, eax
L029c: jge short L02a0
L029e: jmp short L0305
L02a0: test eax, eax
L02a2: jle short L02a6
L02a4: jmp short L0305
L02a6: mov rcx, 0x191f2a86fa0
L02b0: mov r14, [rcx]
L02b3: mov rcx, [rsi+0x18]
L02b7: mov rdx, [rbx+0x18]
L02bb: mov r8, r14
L02be: cmp [rcx], ecx
L02c0: add rsp, 0x30
L02c4: pop rbx
L02c5: pop rbp
L02c6: pop rsi
L02c7: pop rdi
L02c8: pop r14
L02ca: jmp Test+Test+LinearExpr.CompareTo(System.Object, System.Collections.IComparer)
L02cf: sub eax, ebp
L02d1: jmp short L0305
L02d3: mov eax, 1
L02d8: add rsp, 0x30
L02dc: pop rbx
L02dd: pop rbp
L02de: pop rsi
L02df: pop rdi
L02e0: pop r14
L02e2: ret
L02e3: test rdi, rdi
L02e6: je short L02f8
L02e8: mov eax, 0xffffffff
L02ed: add rsp, 0x30
L02f1: pop rbx
L02f2: pop rbp
L02f3: pop rsi
L02f4: pop rdi
L02f5: pop r14
L02f7: ret
L02f8: xor eax, eax
L02fa: add rsp, 0x30
L02fe: pop rbx
L02ff: pop rbp
L0300: pop rsi
L0301: pop rdi
L0302: pop r14
L0304: ret
L0305: add rsp, 0x30
L0309: pop rbx
L030a: pop rbp
L030b: pop rsi
L030c: pop rdi
L030d: pop r14
L030f: ret

;Test+Test+LinearExpr.CompareTo(System.Object)
L0000: push rsi
L0001: sub rsp, 0x20
L0005: mov rsi, rcx
L0008: mov rax, rdx
L000b: test rax, rax
L000e: je short L0024
L0010: mov rcx, 0x7ff953af0bf0
L001a: cmp [rax], rcx
L001d: je short L0024
L001f: call System.Runtime.CompilerServices.CastHelpers.ChkCastClassSpecial(Void*, System.Object)
L0024: mov rcx, rsi
L0027: mov rdx, rax
L002a: cmp [rcx], ecx
L002c: add rsp, 0x20
L0030: pop rsi
L0031: jmp Test+Test+LinearExpr.CompareTo(LinearExpr)

;Test+Test+LinearExpr.CompareTo(System.Object, System.Collections.IComparer)
L0000: push r14
L0002: push rdi
L0003: push rsi
L0004: push rbp
L0005: push rbx
L0006: sub rsp, 0x30
L000a: vzeroupper
L000d: xor eax, eax
L000f: mov [rsp+0x20], rax
L0014: mov [rsp+0x28], rax
L0019: mov rsi, rdx
L001c: mov rdi, r8
L001f: mov rbx, rcx
L0022: mov rbp, rsi
L0025: test rbp, rbp
L0028: je short L0045
L002a: mov rcx, 0x7ff953af0bf0
L0034: cmp [rbp], rcx
L0038: je short L0045
L003a: mov rdx, rsi
L003d: call System.Runtime.CompilerServices.CastHelpers.ChkCastClassSpecial(Void*, System.Object)
L0042: mov rbp, rax
L0045: test rbx, rbx
L0048: je L02d5
L004e: test rbp, rbp
L0051: je short L006b
L0053: mov rcx, 0x7ff953af0bf0
L005d: cmp [rbp], rcx
L0061: je short L006b
L0063: mov rdx, rbp
L0066: call System.Runtime.CompilerServices.CastHelpers.ChkCastClassSpecial(Void*, System.Object)
L006b: test rbp, rbp
L006e: je L02c5
L0074: mov r14d, [rbx+8]
L0078: mov eax, r14d
L007b: mov esi, [rbp+8]
L007e: cmp eax, esi
L0080: jne L02c1
L0086: cmp r14d, 3
L008a: ja L01ad
L0090: mov ecx, r14d
L0093: lea rdx, [Test+Test+LinearExpr.CompareTo(System.Object, System.Collections.IComparer)]
L009a: mov edx, [rdx+rcx*4]
L009d: lea rax, [L001f]
L00a4: add rdx, rax
L00a7: jmp rdx
L00a9: mov rsi, rbx
L00ac: mov rcx, 0x7ff953af1320
L00b6: cmp [rsi], rcx
L00b9: je short L00c6
L00bb: mov rdx, rbx
L00be: call System.Runtime.CompilerServices.CastHelpers.ChkCastClassSpecial(Void*, System.Object)
L00c3: mov rsi, rax
L00c6: mov rbx, rbp
L00c9: mov rcx, 0x7ff953af1320
L00d3: cmp [rbx], rcx
L00d6: je short L00e3
L00d8: mov rdx, rbp
L00db: call System.Runtime.CompilerServices.CastHelpers.ChkCastClassSpecial(Void*, System.Object)
L00e0: mov rbx, rax
L00e3: vmovsd xmm1, [rsi+0x18]
L00e8: vmovsd xmm2, [rbx+0x18]
L00ed: vucomisd xmm2, xmm1
L00f1: jbe short L00fa
L00f3: mov eax, 0xffffffff
L00f8: jmp short L011b
L00fa: vucomisd xmm1, xmm2
L00fe: jbe short L0107
L0100: mov eax, 1
L0105: jmp short L011b
L0107: vucomisd xmm1, xmm2
L010b: jp short L0113
L010d: jne short L0113
L010f: xor eax, eax
L0111: jmp short L011b
L0113: mov rcx, rdi
L0116: call Microsoft.FSharp.Core.LanguagePrimitives+HashCompare.GenericComparisonWithComparerIntrinsic[[System.Double, System.Private.CoreLib]](System.Collections.IComparer, Double, Double)
L011b: test eax, eax
L011d: jl L02b9
L0123: test eax, eax
L0125: jg L02bb
L012b: mov rcx, [rsi+0x10]
L012f: mov rdx, [rbx+0x10]
L0133: mov rsi, rdx
L0136: mov rbx, rcx
L0139: jmp L0022
L013e: mov rsi, rbx
L0141: mov rcx, 0x7ff953af14c8
L014b: cmp [rsi], rcx
L014e: je short L015b
L0150: mov rdx, rbx
L0153: call System.Runtime.CompilerServices.CastHelpers.ChkCastClassSpecial(Void*, System.Object)
L0158: mov rsi, rax
L015b: mov rbx, rbp
L015e: mov rcx, 0x7ff953af14c8
L0168: cmp [rbx], rcx
L016b: je short L0178
L016d: mov rdx, rbp
L0170: call System.Runtime.CompilerServices.CastHelpers.ChkCastClassSpecial(Void*, System.Object)
L0175: mov rbx, rax
L0178: mov rcx, [rsi+0x10]
L017c: mov rdx, [rbx+0x10]
L0180: mov r8, rdi
L0183: cmp [rcx], ecx
L0185: call Test+Test+LinearExpr.CompareTo(System.Object, System.Collections.IComparer)
L018a: test eax, eax
L018c: jl L02bd
L0192: test eax, eax
L0194: jg L02bf
L019a: mov rcx, [rsi+0x18]
L019e: mov rdx, [rbx+0x18]
L01a2: mov rsi, rdx
L01a5: mov rbx, rcx
L01a8: jmp L0022
L01ad: mov rsi, rbx
L01b0: mov rcx, 0x7ff953af0ff8
L01ba: cmp [rsi], rcx
L01bd: je short L01ca
L01bf: mov rdx, rbx
L01c2: call System.Runtime.CompilerServices.CastHelpers.ChkCastClassSpecial(Void*, System.Object)
L01c7: mov rsi, rax
L01ca: mov rax, rbp
L01cd: mov rcx, 0x7ff953af0ff8
L01d7: cmp [rax], rcx
L01da: je short L01e4
L01dc: mov rdx, rbp
L01df: call System.Runtime.CompilerServices.CastHelpers.ChkCastClassSpecial(Void*, System.Object)
L01e4: vmovsd xmm1, [rsi+0x10]
L01e9: vmovsd xmm2, [rax+0x10]
L01ee: vucomisd xmm2, xmm1
L01f2: ja L02f9
L01f8: vucomisd xmm1, xmm2
L01fc: ja L02c5
L0202: vucomisd xmm1, xmm2
L0206: jp short L020e
L0208: je L0309
L020e: mov rcx, rdi
L0211: add rsp, 0x30
L0215: pop rbx
L0216: pop rbp
L0217: pop rsi
L0218: pop rdi
L0219: pop r14
L021b: jmp Microsoft.FSharp.Core.LanguagePrimitives+HashCompare.GenericComparisonWithComparerIntrinsic[[System.Double, System.Private.CoreLib]](System.Collections.IComparer, Double, Double)
L0220: mov rsi, rbx
L0223: mov rcx, 0x7ff953af1178
L022d: cmp [rsi], rcx
L0230: je short L023d
L0232: mov rdx, rbx
L0235: call System.Runtime.CompilerServices.CastHelpers.ChkCastClassSpecial(Void*, System.Object)
L023a: mov rsi, rax
L023d: mov rbx, rbp
L0240: mov rcx, 0x7ff953af1178
L024a: cmp [rbx], rcx
L024d: je short L025a
L024f: mov rdx, rbp
L0252: call System.Runtime.CompilerServices.CastHelpers.ChkCastClassSpecial(Void*, System.Object)
L0257: mov rbx, rax
L025a: add rsi, 0x10
L025e: mov rcx, [rsi]
L0261: mov [rsp+0x20], rcx
L0266: mov rcx, [rsi+8]
L026a: mov [rsp+0x28], rcx
L026f: add rbx, 0x10
L0273: mov rsi, [rbx]
L0276: mov rbx, [rbx+8]
L027a: mov rcx, 0x7ff953af0538
L0284: call 0x00007ff9b182a470
L0289: mov rbp, rax
L028c: lea r14, [rbp+8]
L0290: mov rcx, r14
L0293: mov rdx, rsi
L0296: call 0x00007ff9b182a050
L029b: lea rcx, [r14+8]
L029f: mov rdx, rbx
L02a2: call 0x00007ff9b182a050
L02a7: mov rdx, rbp
L02aa: lea rcx, [rsp+0x20]
L02af: mov r8, rdi
L02b2: call Test+Test+Decision.CompareTo(System.Object, System.Collections.IComparer)
L02b7: jmp short L0316
L02b9: jmp short L0316
L02bb: jmp short L0316
L02bd: jmp short L0316
L02bf: jmp short L0316
L02c1: sub eax, esi
L02c3: jmp short L0316
L02c5: mov eax, 1
L02ca: add rsp, 0x30
L02ce: pop rbx
L02cf: pop rbp
L02d0: pop rsi
L02d1: pop rdi
L02d2: pop r14
L02d4: ret
L02d5: mov rax, rsi
L02d8: test rax, rax
L02db: je short L02f4
L02dd: mov rcx, 0x7ff953af0bf0
L02e7: cmp [rax], rcx
L02ea: je short L02f4
L02ec: mov rdx, rsi
L02ef: call System.Runtime.CompilerServices.CastHelpers.ChkCastClassSpecial(Void*, System.Object)
L02f4: test rax, rax
L02f7: je short L0309
L02f9: mov eax, 0xffffffff
L02fe: add rsp, 0x30
L0302: pop rbx
L0303: pop rbp
L0304: pop rsi
L0305: pop rdi
L0306: pop r14
L0308: ret
L0309: xor eax, eax
L030b: add rsp, 0x30
L030f: pop rbx
L0310: pop rbp
L0311: pop rsi
L0312: pop rdi
L0313: pop r14
L0315: ret
L0316: add rsp, 0x30
L031a: pop rbx
L031b: pop rbp
L031c: pop rsi
L031d: pop rdi
L031e: pop r14
L0320: ret

;Test+Test+LinearExpr.GetHashCode(System.Collections.IEqualityComparer)
L0000: push rdi
L0001: push rsi
L0002: push rbp
L0003: push rbx
L0004: sub rsp, 0x38
L0008: vzeroupper
L000b: xor eax, eax
L000d: mov [rsp+0x28], rax
L0012: mov [rsp+0x30], rax
L0017: mov rsi, rcx
L001a: mov rdi, rdx
L001d: test rsi, rsi
L0020: je L017b
L0026: mov ebx, [rsi+8]
L0029: cmp ebx, 3
L002c: ja short L0046
L002e: mov ecx, ebx
L0030: lea rdx, [Test+Test+LinearExpr.GetHashCode(System.Collections.IEqualityComparer)]
L0037: mov edx, [rdx+rcx*4]
L003a: lea rax, [L001d]
L0041: add rdx, rax
L0044: jmp rdx
L0046: mov rcx, rsi
L0049: mov rdx, 0x7ff953af0ff8
L0053: cmp [rcx], rdx
L0056: je short L0063
L0058: mov rcx, rdx
L005b: mov rdx, rsi
L005e: call System.Runtime.CompilerServices.CastHelpers.ChkCastClassSpecial(Void*, System.Object)
L0063: vmovsd xmm1, [rsi+0x10]
L0068: mov rcx, rdi
L006b: call Microsoft.FSharp.Core.LanguagePrimitives+HashCompare.GenericHashWithComparerIntrinsic[[System.Double, System.Private.CoreLib]](System.Collections.IEqualityComparer, Double)
L0070: lea ebp, [rax-0x61c88647]
L0076: mov eax, ebp
L0078: jmp L0172
L007d: mov rcx, rsi
L0080: mov rdx, 0x7ff953af1178
L008a: cmp [rcx], rdx
L008d: je short L009a
L008f: mov rcx, rdx
L0092: mov rdx, rsi
L0095: call System.Runtime.CompilerServices.CastHelpers.ChkCastClassSpecial(Void*, System.Object)
L009a: add rsi, 0x10
L009e: mov rcx, [rsi]
L00a1: mov [rsp+0x28], rcx
L00a6: mov rcx, [rsi+8]
L00aa: mov [rsp+0x30], rcx
L00af: lea rcx, [rsp+0x28]
L00b4: mov rdx, rdi
L00b7: call Test+Test+Decision.GetHashCode(System.Collections.IEqualityComparer)
L00bc: lea ebp, [rax-0x61c88607]
L00c2: mov eax, ebp
L00c4: jmp L0172
L00c9: mov rcx, rsi
L00cc: mov rdx, 0x7ff953af1320
L00d6: cmp [rcx], rdx
L00d9: je short L00e6
L00db: mov rcx, rdx
L00de: mov rdx, rsi
L00e1: call System.Runtime.CompilerServices.CastHelpers.ChkCastClassSpecial(Void*, System.Object)
L00e6: mov rcx, [rsi+0x10]
L00ea: mov rdx, rdi
L00ed: cmp [rcx], ecx
L00ef: call Test+Test+LinearExpr.GetHashCode(System.Collections.IEqualityComparer)
L00f4: lea ebp, [rax-0x61c885c7]
L00fa: vmovsd xmm1, [rsi+0x18]
L00ff: mov rcx, rdi
L0102: call Microsoft.FSharp.Core.LanguagePrimitives+HashCompare.GenericHashWithComparerIntrinsic[[System.Double, System.Private.CoreLib]](System.Collections.IEqualityComparer, Double)
L0107: mov ecx, ebp
L0109: shl ecx, 6
L010c: add ecx, eax
L010e: mov edx, ebp
L0110: sar edx, 2
L0113: lea ebp, [rcx+rdx-0x61c88647]
L011a: mov eax, ebp
L011c: jmp short L0172
L011e: mov rcx, rsi
L0121: mov rdx, 0x7ff953af14c8
L012b: cmp [rcx], rdx
L012e: je short L013b
L0130: mov rcx, rdx
L0133: mov rdx, rsi
L0136: call System.Runtime.CompilerServices.CastHelpers.ChkCastClassSpecial(Void*, System.Object)
L013b: mov rcx, [rsi+0x18]
L013f: mov rdx, rdi
L0142: cmp [rcx], ecx
L0144: call Test+Test+LinearExpr.GetHashCode(System.Collections.IEqualityComparer)
L0149: lea ebp, [rax-0x61c88587]
L014f: mov rcx, [rsi+0x10]
L0153: mov rdx, rdi
L0156: cmp [rcx], ecx
L0158: call Test+Test+LinearExpr.GetHashCode(System.Collections.IEqualityComparer)
L015d: mov edx, ebp
L015f: shl edx, 6
L0162: add eax, edx
L0164: mov edx, ebp
L0166: sar edx, 2
L0169: lea ebp, [rax+rdx-0x61c88647]
L0170: mov eax, ebp
L0172: add rsp, 0x38
L0176: pop rbx
L0177: pop rbp
L0178: pop rsi
L0179: pop rdi
L017a: ret
L017b: xor eax, eax
L017d: add rsp, 0x38
L0181: pop rbx
L0182: pop rbp
L0183: pop rsi
L0184: pop rdi
L0185: ret

;Test+Test+LinearExpr.GetHashCode()
L0000: mov rdx, 0x191f2a86fa8
L000a: mov rdx, [rdx]
L000d: cmp [rcx], ecx
L000f: jmp Test+Test+LinearExpr.GetHashCode(System.Collections.IEqualityComparer)

;Test+Test+LinearExpr.Equals(System.Object, System.Collections.IEqualityComparer)
L0000: push r14
L0002: push rdi
L0003: push rsi
L0004: push rbp
L0005: push rbx
L0006: sub rsp, 0x30
L000a: vzeroupper
L000d: xor eax, eax
L000f: mov [rsp+0x20], rax
L0014: mov [rsp+0x28], rax
L0019: mov rsi, r8
L001c: mov rdi, rcx
L001f: test rdi, rdi
L0022: je L023a
L0028: mov rcx, 0x7ff953af0bf0
L0032: call System.Runtime.CompilerServices.CastHelpers.IsInstanceOfClass(Void*, System.Object)
L0037: mov rbx, rax
L003a: test rbx, rbx
L003d: je L022d
L0043: mov ebp, [rdi+8]
L0046: mov ecx, ebp
L0048: mov edx, [rbx+8]
L004b: cmp ecx, edx
L004d: jne L022d
L0053: cmp ebp, 3
L0056: ja L013a
L005c: mov ecx, ebp
L005e: lea rdx, [Test+Test+LinearExpr.Equals(System.Object, System.Collections.IEqualityComparer)]
L0065: mov edx, [rdx+rcx*4]
L0068: lea rax, [L001c]
L006f: add rdx, rax
L0072: jmp rdx
L0074: mov r14, rdi
L0077: mov rcx, 0x7ff953af1320
L0081: cmp [r14], rcx
L0084: je short L0091
L0086: mov rdx, rdi
L0089: call System.Runtime.CompilerServices.CastHelpers.ChkCastClassSpecial(Void*, System.Object)
L008e: mov r14, rax
L0091: mov rdi, rbx
L0094: mov rcx, 0x7ff953af1320
L009e: cmp [rdi], rcx
L00a1: je short L00ae
L00a3: mov rdx, rbx
L00a6: call System.Runtime.CompilerServices.CastHelpers.ChkCastClassSpecial(Void*, System.Object)
L00ab: mov rdi, rax
L00ae: vmovsd xmm0, [r14+0x18]
L00b4: vucomisd xmm0, [rdi+0x18]
L00b9: jp L022d
L00bf: jne L022d
L00c5: mov rcx, [r14+0x10]
L00c9: mov rdx, [rdi+0x10]
L00cd: mov rdi, rcx
L00d0: jmp L001f
L00d5: mov rbp, rdi
L00d8: mov rcx, 0x7ff953af14c8
L00e2: cmp [rbp], rcx
L00e6: je short L00f3
L00e8: mov rdx, rdi
L00eb: call System.Runtime.CompilerServices.CastHelpers.ChkCastClassSpecial(Void*, System.Object)
L00f0: mov rbp, rax
L00f3: mov rdi, rbx
L00f6: mov rcx, 0x7ff953af14c8
L0100: cmp [rdi], rcx
L0103: je short L0110
L0105: mov rdx, rbx
L0108: call System.Runtime.CompilerServices.CastHelpers.ChkCastClassSpecial(Void*, System.Object)
L010d: mov rdi, rax
L0110: mov rcx, [rbp+0x10]
L0114: mov rdx, [rdi+0x10]
L0118: mov r8, rsi
L011b: cmp [rcx], ecx
L011d: call Test+Test+LinearExpr.Equals(System.Object, System.Collections.IEqualityComparer)
L0122: test eax, eax
L0124: je L022d
L012a: mov rcx, [rbp+0x18]
L012e: mov rdx, [rdi+0x18]
L0132: mov rdi, rcx
L0135: jmp L001f
L013a: mov rbp, rdi
L013d: mov rcx, 0x7ff953af0ff8
L0147: cmp [rbp], rcx
L014b: je short L0158
L014d: mov rdx, rdi
L0150: call System.Runtime.CompilerServices.CastHelpers.ChkCastClassSpecial(Void*, System.Object)
L0155: mov rbp, rax
L0158: mov rax, rbx
L015b: mov rcx, 0x7ff953af0ff8
L0165: cmp [rax], rcx
L0168: je short L0172
L016a: mov rdx, rbx
L016d: call System.Runtime.CompilerServices.CastHelpers.ChkCastClassSpecial(Void*, System.Object)
L0172: vmovsd xmm0, [rbp+0x10]
L0177: vucomisd xmm0, [rax+0x10]
L017c: setnp r14b
L0180: jp short L0186
L0182: sete r14b
L0186: movzx r14d, r14b
L018a: jmp L0245
L018f: mov rbp, rdi
L0192: mov rcx, 0x7ff953af1178
L019c: cmp [rbp], rcx
L01a0: je short L01ad
L01a2: mov rdx, rdi
L01a5: call System.Runtime.CompilerServices.CastHelpers.ChkCastClassSpecial(Void*, System.Object)
L01aa: mov rbp, rax
L01ad: mov rdi, rbx
L01b0: mov rcx, 0x7ff953af1178
L01ba: cmp [rdi], rcx
L01bd: je short L01ca
L01bf: mov rdx, rbx
L01c2: call System.Runtime.CompilerServices.CastHelpers.ChkCastClassSpecial(Void*, System.Object)
L01c7: mov rdi, rax
L01ca: add rbp, 0x10
L01ce: mov rcx, [rbp]
L01d2: mov [rsp+0x20], rcx
L01d7: mov rcx, [rbp+8]
L01db: mov [rsp+0x28], rcx
L01e0: add rdi, 0x10
L01e4: mov rbx, [rdi]
L01e7: mov rdi, [rdi+8]
L01eb: mov rcx, 0x7ff953af0538
L01f5: call 0x00007ff9b182a470
L01fa: mov rbp, rax
L01fd: lea r14, [rbp+8]
L0201: mov rcx, r14
L0204: mov rdx, rbx
L0207: call 0x00007ff9b182a050
L020c: lea rcx, [r14+8]
L0210: mov rdx, rdi
L0213: call 0x00007ff9b182a050
L0218: mov rdx, rbp
L021b: lea rcx, [rsp+0x20]
L0220: mov r8, rsi
L0223: call Test+Test+Decision.Equals(System.Object, System.Collections.IEqualityComparer)
L0228: mov r14d, eax
L022b: jmp short L0245
L022d: xor eax, eax
L022f: add rsp, 0x30
L0233: pop rbx
L0234: pop rbp
L0235: pop rsi
L0236: pop rdi
L0237: pop r14
L0239: ret
L023a: test rdx, rdx
L023d: sete r14b
L0241: movzx r14d, r14b
L0245: movzx eax, r14b
L0249: add rsp, 0x30
L024d: pop rbx
L024e: pop rbp
L024f: pop rsi
L0250: pop rdi
L0251: pop r14
L0253: ret

;Test+Test+LinearExpr.Equals(LinearExpr)
L0000: push rdi
L0001: push rsi
L0002: push rbx
L0003: sub rsp, 0x40
L0007: vzeroupper
L000a: vmovaps [rsp+0x30], xmm6
L0010: vmovaps [rsp+0x20], xmm7
L0016: mov rsi, rdx
L0019: mov rdx, rcx
L001c: test rdx, rdx
L001f: je L022c
L0025: test rsi, rsi
L0028: je L0216
L002e: mov edi, [rdx+8]
L0031: mov ecx, edi
L0033: mov eax, [rsi+8]
L0036: cmp ecx, eax
L0038: jne L0216
L003e: cmp edi, 3
L0041: ja L012e
L0047: mov ecx, edi
L0049: lea rax, [Test+Test+LinearExpr.Equals(LinearExpr)]
L0050: mov eax, [rax+rcx*4]
L0053: lea r8, [L0019]
L005a: add rax, r8
L005d: jmp rax
L005f: mov rbx, rdx
L0062: mov rcx, 0x7ff953af1320
L006c: cmp [rbx], rcx
L006f: je short L0079
L0071: call System.Runtime.CompilerServices.CastHelpers.ChkCastClassSpecial(Void*, System.Object)
L0076: mov rbx, rax
L0079: mov rdi, rsi
L007c: mov rcx, 0x7ff953af1320
L0086: cmp [rdi], rcx
L0089: je short L0096
L008b: mov rdx, rsi
L008e: call System.Runtime.CompilerServices.CastHelpers.ChkCastClassSpecial(Void*, System.Object)
L0093: mov rdi, rax
L0096: vmovsd xmm6, [rbx+0x18]
L009b: vmovsd xmm7, [rdi+0x18]
L00a0: vucomisd xmm6, xmm7
L00a4: jp short L00a8
L00a6: je short L00c6
L00a8: vucomisd xmm6, xmm6
L00ac: jp short L00b4
L00ae: je L0216
L00b4: vucomisd xmm7, xmm7
L00b8: setp cl
L00bb: movzx ecx, cl
L00be: test ecx, ecx
L00c0: je L0216
L00c6: mov rdx, [rbx+0x10]
L00ca: mov rsi, [rdi+0x10]
L00ce: jmp L001c
L00d3: mov rdi, rdx
L00d6: mov rcx, 0x7ff953af14c8
L00e0: cmp [rdi], rcx
L00e3: je short L00ed
L00e5: call System.Runtime.CompilerServices.CastHelpers.ChkCastClassSpecial(Void*, System.Object)
L00ea: mov rdi, rax
L00ed: mov rbx, rsi
L00f0: mov rcx, 0x7ff953af14c8
L00fa: cmp [rbx], rcx
L00fd: je short L010a
L00ff: mov rdx, rsi
L0102: call System.Runtime.CompilerServices.CastHelpers.ChkCastClassSpecial(Void*, System.Object)
L0107: mov rbx, rax
L010a: mov rcx, [rdi+0x10]
L010e: mov rdx, [rbx+0x10]
L0112: cmp [rcx], ecx
L0114: call Test+Test+LinearExpr.Equals(LinearExpr)
L0119: test eax, eax
L011b: je L0216
L0121: mov rdx, [rdi+0x18]
L0125: mov rsi, [rbx+0x18]
L0129: jmp L001c
L012e: mov rdi, rdx
L0131: mov rcx, 0x7ff953af0ff8
L013b: cmp [rdi], rcx
L013e: je short L0148
L0140: call System.Runtime.CompilerServices.CastHelpers.ChkCastClassSpecial(Void*, System.Object)
L0145: mov rdi, rax
L0148: mov rax, rsi
L014b: mov rcx, 0x7ff953af0ff8
L0155: cmp [rax], rcx
L0158: je short L0162
L015a: mov rdx, rsi
L015d: call System.Runtime.CompilerServices.CastHelpers.ChkCastClassSpecial(Void*, System.Object)
L0162: vmovsd xmm6, [rdi+0x10]
L0167: vmovsd xmm7, [rax+0x10]
L016c: vucomisd xmm6, xmm7
L0170: jp short L018d
L0172: jne short L018d
L0174: mov eax, 1
L0179: vmovaps xmm6, [rsp+0x30]
L017f: vmovaps xmm7, [rsp+0x20]
L0185: add rsp, 0x40
L0189: pop rbx
L018a: pop rsi
L018b: pop rdi
L018c: ret
L018d: vucomisd xmm6, xmm6
L0191: jp short L0199
L0193: je L0216
L0199: vucomisd xmm7, xmm7
L019d: setp bl
L01a0: movzx ebx, bl
L01a3: jmp L0235
L01a8: mov rdi, rdx
L01ab: mov rcx, 0x7ff953af1178
L01b5: cmp [rdi], rcx
L01b8: je short L01c2
L01ba: call System.Runtime.CompilerServices.CastHelpers.ChkCastClassSpecial(Void*, System.Object)
L01bf: mov rdi, rax
L01c2: mov rbx, rsi
L01c5: mov rcx, 0x7ff953af1178
L01cf: cmp [rbx], rcx
L01d2: je short L01df
L01d4: mov rdx, rsi
L01d7: call System.Runtime.CompilerServices.CastHelpers.ChkCastClassSpecial(Void*, System.Object)
L01dc: mov rbx, rax
L01df: add rdi, 0x10
L01e3: mov rcx, [rdi]
L01e6: mov rsi, [rdi+8]
L01ea: add rbx, 0x10
L01ee: mov rdx, [rbx]
L01f1: mov rdi, [rbx+8]
L01f5: cmp [rcx], ecx
L01f7: call Test+Test+DecisionName.Equals(DecisionName)
L01fc: test eax, eax
L01fe: je short L0212
L0200: mov rcx, rsi
L0203: mov rdx, rdi
L0206: cmp [rcx], ecx
L0208: call Test+Test+DecisionType.Equals(DecisionType)
L020d: movzx ebx, al
L0210: jmp short L0214
L0212: xor ebx, ebx
L0214: jmp short L0235
L0216: xor eax, eax
L0218: vmovaps xmm6, [rsp+0x30]
L021e: vmovaps xmm7, [rsp+0x20]
L0224: add rsp, 0x40
L0228: pop rbx
L0229: pop rsi
L022a: pop rdi
L022b: ret
L022c: test rsi, rsi
L022f: sete bl
L0232: movzx ebx, bl
L0235: movzx eax, bl
L0238: vmovaps xmm6, [rsp+0x30]
L023e: vmovaps xmm7, [rsp+0x20]
L0244: add rsp, 0x40
L0248: pop rbx
L0249: pop rsi
L024a: pop rdi
L024b: ret

;Test+Test+LinearExpr.Equals(System.Object)
L0000: push rsi
L0001: sub rsp, 0x20
L0005: mov rsi, rcx
L0008: mov rcx, 0x7ff953af0bf0
L0012: call System.Runtime.CompilerServices.CastHelpers.IsInstanceOfClass(Void*, System.Object)
L0017: test rax, rax
L001a: je short L002e
L001c: mov rcx, rsi
L001f: mov rdx, rax
L0022: cmp [rcx], ecx
L0024: add rsp, 0x20
L0028: pop rsi
L0029: jmp Test+Test+LinearExpr.Equals(LinearExpr)
L002e: xor eax, eax
L0030: add rsp, 0x20
L0034: pop rsi
L0035: ret

;Test+Test+LinearExpr+Scale.get_scale()
L0000: vzeroupper
L0003: vmovsd xmm0, [rcx+0x18]
L0008: ret

;Test+Test+LinearExpr+Scale.get_expr()
L0000: mov rax, [rcx+0x10]
L0004: ret

;Test+Test+LinearExpr.get_IsFloat()
L0000: cmp dword ptr [rcx+8], 0
L0004: sete al
L0007: movzx eax, al
L000a: ret

;Test+Test+LinearExpr.get_IsDecision()
L0000: cmp dword ptr [rcx+8], 1
L0004: sete al
L0007: movzx eax, al
L000a: ret

;Test+Test+LinearExpr.get_IsScale()
L0000: cmp dword ptr [rcx+8], 2
L0004: sete al
L0007: movzx eax, al
L000a: ret

;Test+Test+LinearExpr.get_IsAdd()
L0000: cmp dword ptr [rcx+8], 3
L0004: sete al
L0007: movzx eax, al
L000a: ret

;Test+Test+LinearExpr.get_Tag()
L0000: mov eax, [rcx+8]
L0003: ret

;Test+Test+LinearExpr.ToString()
L0000: push rdi
L0001: push rsi
L0002: sub rsp, 0x28
L0006: mov rsi, rcx
L0009: mov rcx, 0x7ff953af84a0
L0013: call 0x00007ff9b182a470
L0018: mov rdi, rax
L001b: mov rdx, 0x191f2a9b3f0
L0025: mov rdx, [rdx]
L0028: lea rcx, [rdi+8]
L002c: call 0x00007ff9b182a080
L0031: xor edx, edx
L0033: mov [rdi+0x10], rdx
L0037: mov [rdi+0x18], rdx
L003b: mov rdx, rdi
L003e: mov rcx, 0x7ff953af8780
L0048: call Microsoft.FSharp.Core.PrintfModule.PrintFormatToStringThen[[System.__Canon, System.Private.CoreLib]](Microsoft.FSharp.Core.PrintfFormat`4<System.__Canon,Microsoft.FSharp.Core.Unit,System.String,System.String>)
L004d: mov rcx, rax
L0050: mov rdx, rsi
L0053: mov rax, [rax]
L0056: mov rax, [rax+0x40]
L005a: mov rax, [rax+0x20]
L005e: add rsp, 0x28
L0062: pop rsi
L0063: pop rdi
L0064: jmp rax

;Test+Test+LinearExpr.CompareTo(LinearExpr)
L0000: push r14
L0002: push rdi
L0003: push rsi
L0004: push rbp
L0005: push rbx
L0006: sub rsp, 0x30
L000a: vzeroupper
L000d: xor eax, eax
L000f: mov [rsp+0x20], rax
L0014: mov [rsp+0x28], rax
L0019: mov rsi, rcx
L001c: mov rdi, rdx
L001f: test rsi, rsi
L0022: je L02e3
L0028: test rdi, rdi
L002b: je L02d3
L0031: mov ebx, [rsi+8]
L0034: mov eax, ebx
L0036: mov ebp, [rdi+8]
L0039: cmp eax, ebp
L003b: jne L02cf
L0041: cmp ebx, 3
L0044: ja short L005e
L0046: mov ecx, ebx
L0048: lea rdx, [Test+Test+LinearExpr.CompareTo(LinearExpr)]
L004f: mov edx, [rdx+rcx*4]
L0052: lea rax, [L001f]
L0059: add rdx, rax
L005c: jmp rdx
L005e: mov rcx, rsi
L0061: mov rdx, 0x7ff953af0ff8
L006b: cmp [rcx], rdx
L006e: je short L007b
L0070: mov rcx, rdx
L0073: mov rdx, rsi
L0076: call System.Runtime.CompilerServices.CastHelpers.ChkCastClassSpecial(Void*, System.Object)
L007b: mov rax, rdi
L007e: mov rcx, 0x7ff953af0ff8
L0088: cmp [rax], rcx
L008b: je short L0095
L008d: mov rdx, rdi
L0090: call System.Runtime.CompilerServices.CastHelpers.ChkCastClassSpecial(Void*, System.Object)
L0095: mov rcx, 0x191f2a86fa0
L009f: mov r14, [rcx]
L00a2: vmovsd xmm1, [rsi+0x10]
L00a7: vmovsd xmm2, [rax+0x10]
L00ac: vucomisd xmm2, xmm1
L00b0: ja L02e8
L00b6: vucomisd xmm1, xmm2
L00ba: ja L02d3
L00c0: vucomisd xmm1, xmm2
L00c4: jp short L00cc
L00c6: je L02f8
L00cc: mov rcx, r14
L00cf: add rsp, 0x30
L00d3: pop rbx
L00d4: pop rbp
L00d5: pop rsi
L00d6: pop rdi
L00d7: pop r14
L00d9: jmp Microsoft.FSharp.Core.LanguagePrimitives+HashCompare.GenericComparisonWithComparerIntrinsic[[System.Double, System.Private.CoreLib]](System.Collections.IComparer, Double, Double)
L00de: mov rcx, rsi
L00e1: mov rdx, 0x7ff953af1178
L00eb: cmp [rcx], rdx
L00ee: je short L00fb
L00f0: mov rcx, rdx
L00f3: mov rdx, rsi
L00f6: call System.Runtime.CompilerServices.CastHelpers.ChkCastClassSpecial(Void*, System.Object)
L00fb: mov rbx, rdi
L00fe: mov rcx, 0x7ff953af1178
L0108: cmp [rbx], rcx
L010b: je short L0118
L010d: mov rdx, rdi
L0110: call System.Runtime.CompilerServices.CastHelpers.ChkCastClassSpecial(Void*, System.Object)
L0115: mov rbx, rax
L0118: mov rcx, 0x191f2a86fa0
L0122: mov r14, [rcx]
L0125: add rsi, 0x10
L0129: mov rcx, [rsi]
L012c: mov [rsp+0x20], rcx
L0131: mov rcx, [rsi+8]
L0135: mov [rsp+0x28], rcx
L013a: add rbx, 0x10
L013e: mov rdi, [rbx]
L0141: mov rsi, [rbx+8]
L0145: mov rcx, 0x7ff953af0538
L014f: call 0x00007ff9b182a470
L0154: mov rbx, rax
L0157: lea rbp, [rbx+8]
L015b: mov rcx, rbp
L015e: mov rdx, rdi
L0161: call 0x00007ff9b182a050
L0166: lea rcx, [rbp+8]
L016a: mov rdx, rsi
L016d: call 0x00007ff9b182a050
L0172: mov rdx, rbx
L0175: lea rcx, [rsp+0x20]
L017a: mov r8, r14
L017d: call Test+Test+Decision.CompareTo(System.Object, System.Collections.IComparer)
L0182: jmp L0305
L0187: mov rcx, rsi
L018a: mov rdx, 0x7ff953af1320
L0194: cmp [rcx], rdx
L0197: je short L01a4
L0199: mov rcx, rdx
L019c: mov rdx, rsi
L019f: call System.Runtime.CompilerServices.CastHelpers.ChkCastClassSpecial(Void*, System.Object)
L01a4: mov rbx, rdi
L01a7: mov rcx, 0x7ff953af1320
L01b1: cmp [rbx], rcx
L01b4: je short L01c1
L01b6: mov rdx, rdi
L01b9: call System.Runtime.CompilerServices.CastHelpers.ChkCastClassSpecial(Void*, System.Object)
L01be: mov rbx, rax
L01c1: mov rcx, 0x191f2a86fa0
L01cb: mov r14, [rcx]
L01ce: vmovsd xmm1, [rsi+0x18]
L01d3: vmovsd xmm2, [rbx+0x18]
L01d8: vucomisd xmm2, xmm1
L01dc: jbe short L01e5
L01de: mov eax, 0xffffffff
L01e3: jmp short L0206
L01e5: vucomisd xmm1, xmm2
L01e9: jbe short L01f2
L01eb: mov eax, 1
L01f0: jmp short L0206
L01f2: vucomisd xmm1, xmm2
L01f6: jp short L01fe
L01f8: jne short L01fe
L01fa: xor eax, eax
L01fc: jmp short L0206
L01fe: mov rcx, r14
L0201: call Microsoft.FSharp.Core.LanguagePrimitives+HashCompare.GenericComparisonWithComparerIntrinsic[[System.Double, System.Private.CoreLib]](System.Collections.IComparer, Double, Double)
L0206: test eax, eax
L0208: jge short L020f
L020a: jmp L0305
L020f: test eax, eax
L0211: jle short L0218
L0213: jmp L0305
L0218: mov rcx, 0x191f2a86fa0
L0222: mov r14, [rcx]
L0225: mov rcx, [rsi+0x10]
L0229: mov rdx, [rbx+0x10]
L022d: mov r8, r14
L0230: cmp [rcx], ecx
L0232: add rsp, 0x30
L0236: pop rbx
L0237: pop rbp
L0238: pop rsi
L0239: pop rdi
L023a: pop r14
L023c: jmp Test+Test+LinearExpr.CompareTo(System.Object, System.Collections.IComparer)
L0241: mov rcx, rsi
L0244: mov rdx, 0x7ff953af14c8
L024e: cmp [rcx], rdx
L0251: je short L025e
L0253: mov rcx, rdx
L0256: mov rdx, rsi
L0259: call System.Runtime.CompilerServices.CastHelpers.ChkCastClassSpecial(Void*, System.Object)
L025e: mov rbx, rdi
L0261: mov rcx, 0x7ff953af14c8
L026b: cmp [rbx], rcx
L026e: je short L027b
L0270: mov rdx, rdi
L0273: call System.Runtime.CompilerServices.CastHelpers.ChkCastClassSpecial(Void*, System.Object)
L0278: mov rbx, rax
L027b: mov rcx, 0x191f2a86fa0
L0285: mov r14, [rcx]
L0288: mov rcx, [rsi+0x10]
L028c: mov rdx, [rbx+0x10]
L0290: mov r8, r14
L0293: cmp [rcx], ecx
L0295: call Test+Test+LinearExpr.CompareTo(System.Object, System.Collections.IComparer)
L029a: test eax, eax
L029c: jge short L02a0
L029e: jmp short L0305
L02a0: test eax, eax
L02a2: jle short L02a6
L02a4: jmp short L0305
L02a6: mov rcx, 0x191f2a86fa0
L02b0: mov r14, [rcx]
L02b3: mov rcx, [rsi+0x18]
L02b7: mov rdx, [rbx+0x18]
L02bb: mov r8, r14
L02be: cmp [rcx], ecx
L02c0: add rsp, 0x30
L02c4: pop rbx
L02c5: pop rbp
L02c6: pop rsi
L02c7: pop rdi
L02c8: pop r14
L02ca: jmp Test+Test+LinearExpr.CompareTo(System.Object, System.Collections.IComparer)
L02cf: sub eax, ebp
L02d1: jmp short L0305
L02d3: mov eax, 1
L02d8: add rsp, 0x30
L02dc: pop rbx
L02dd: pop rbp
L02de: pop rsi
L02df: pop rdi
L02e0: pop r14
L02e2: ret
L02e3: test rdi, rdi
L02e6: je short L02f8
L02e8: mov eax, 0xffffffff
L02ed: add rsp, 0x30
L02f1: pop rbx
L02f2: pop rbp
L02f3: pop rsi
L02f4: pop rdi
L02f5: pop r14
L02f7: ret
L02f8: xor eax, eax
L02fa: add rsp, 0x30
L02fe: pop rbx
L02ff: pop rbp
L0300: pop rsi
L0301: pop rdi
L0302: pop r14
L0304: ret
L0305: add rsp, 0x30
L0309: pop rbx
L030a: pop rbp
L030b: pop rsi
L030c: pop rdi
L030d: pop r14
L030f: ret

;Test+Test+LinearExpr.CompareTo(System.Object)
L0000: push rsi
L0001: sub rsp, 0x20
L0005: mov rsi, rcx
L0008: mov rax, rdx
L000b: test rax, rax
L000e: je short L0024
L0010: mov rcx, 0x7ff953af0bf0
L001a: cmp [rax], rcx
L001d: je short L0024
L001f: call System.Runtime.CompilerServices.CastHelpers.ChkCastClassSpecial(Void*, System.Object)
L0024: mov rcx, rsi
L0027: mov rdx, rax
L002a: cmp [rcx], ecx
L002c: add rsp, 0x20
L0030: pop rsi
L0031: jmp Test+Test+LinearExpr.CompareTo(LinearExpr)

;Test+Test+LinearExpr.CompareTo(System.Object, System.Collections.IComparer)
L0000: push r14
L0002: push rdi
L0003: push rsi
L0004: push rbp
L0005: push rbx
L0006: sub rsp, 0x30
L000a: vzeroupper
L000d: xor eax, eax
L000f: mov [rsp+0x20], rax
L0014: mov [rsp+0x28], rax
L0019: mov rsi, rdx
L001c: mov rdi, r8
L001f: mov rbx, rcx
L0022: mov rbp, rsi
L0025: test rbp, rbp
L0028: je short L0045
L002a: mov rcx, 0x7ff953af0bf0
L0034: cmp [rbp], rcx
L0038: je short L0045
L003a: mov rdx, rsi
L003d: call System.Runtime.CompilerServices.CastHelpers.ChkCastClassSpecial(Void*, System.Object)
L0042: mov rbp, rax
L0045: test rbx, rbx
L0048: je L02d5
L004e: test rbp, rbp
L0051: je short L006b
L0053: mov rcx, 0x7ff953af0bf0
L005d: cmp [rbp], rcx
L0061: je short L006b
L0063: mov rdx, rbp
L0066: call System.Runtime.CompilerServices.CastHelpers.ChkCastClassSpecial(Void*, System.Object)
L006b: test rbp, rbp
L006e: je L02c5
L0074: mov r14d, [rbx+8]
L0078: mov eax, r14d
L007b: mov esi, [rbp+8]
L007e: cmp eax, esi
L0080: jne L02c1
L0086: cmp r14d, 3
L008a: ja L01ad
L0090: mov ecx, r14d
L0093: lea rdx, [Test+Test+LinearExpr.CompareTo(System.Object, System.Collections.IComparer)]
L009a: mov edx, [rdx+rcx*4]
L009d: lea rax, [L001f]
L00a4: add rdx, rax
L00a7: jmp rdx
L00a9: mov rsi, rbx
L00ac: mov rcx, 0x7ff953af1320
L00b6: cmp [rsi], rcx
L00b9: je short L00c6
L00bb: mov rdx, rbx
L00be: call System.Runtime.CompilerServices.CastHelpers.ChkCastClassSpecial(Void*, System.Object)
L00c3: mov rsi, rax
L00c6: mov rbx, rbp
L00c9: mov rcx, 0x7ff953af1320
L00d3: cmp [rbx], rcx
L00d6: je short L00e3
L00d8: mov rdx, rbp
L00db: call System.Runtime.CompilerServices.CastHelpers.ChkCastClassSpecial(Void*, System.Object)
L00e0: mov rbx, rax
L00e3: vmovsd xmm1, [rsi+0x18]
L00e8: vmovsd xmm2, [rbx+0x18]
L00ed: vucomisd xmm2, xmm1
L00f1: jbe short L00fa
L00f3: mov eax, 0xffffffff
L00f8: jmp short L011b
L00fa: vucomisd xmm1, xmm2
L00fe: jbe short L0107
L0100: mov eax, 1
L0105: jmp short L011b
L0107: vucomisd xmm1, xmm2
L010b: jp short L0113
L010d: jne short L0113
L010f: xor eax, eax
L0111: jmp short L011b
L0113: mov rcx, rdi
L0116: call Microsoft.FSharp.Core.LanguagePrimitives+HashCompare.GenericComparisonWithComparerIntrinsic[[System.Double, System.Private.CoreLib]](System.Collections.IComparer, Double, Double)
L011b: test eax, eax
L011d: jl L02b9
L0123: test eax, eax
L0125: jg L02bb
L012b: mov rcx, [rsi+0x10]
L012f: mov rdx, [rbx+0x10]
L0133: mov rsi, rdx
L0136: mov rbx, rcx
L0139: jmp L0022
L013e: mov rsi, rbx
L0141: mov rcx, 0x7ff953af14c8
L014b: cmp [rsi], rcx
L014e: je short L015b
L0150: mov rdx, rbx
L0153: call System.Runtime.CompilerServices.CastHelpers.ChkCastClassSpecial(Void*, System.Object)
L0158: mov rsi, rax
L015b: mov rbx, rbp
L015e: mov rcx, 0x7ff953af14c8
L0168: cmp [rbx], rcx
L016b: je short L0178
L016d: mov rdx, rbp
L0170: call System.Runtime.CompilerServices.CastHelpers.ChkCastClassSpecial(Void*, System.Object)
L0175: mov rbx, rax
L0178: mov rcx, [rsi+0x10]
L017c: mov rdx, [rbx+0x10]
L0180: mov r8, rdi
L0183: cmp [rcx], ecx
L0185: call Test+Test+LinearExpr.CompareTo(System.Object, System.Collections.IComparer)
L018a: test eax, eax
L018c: jl L02bd
L0192: test eax, eax
L0194: jg L02bf
L019a: mov rcx, [rsi+0x18]
L019e: mov rdx, [rbx+0x18]
L01a2: mov rsi, rdx
L01a5: mov rbx, rcx
L01a8: jmp L0022
L01ad: mov rsi, rbx
L01b0: mov rcx, 0x7ff953af0ff8
L01ba: cmp [rsi], rcx
L01bd: je short L01ca
L01bf: mov rdx, rbx
L01c2: call System.Runtime.CompilerServices.CastHelpers.ChkCastClassSpecial(Void*, System.Object)
L01c7: mov rsi, rax
L01ca: mov rax, rbp
L01cd: mov rcx, 0x7ff953af0ff8
L01d7: cmp [rax], rcx
L01da: je short L01e4
L01dc: mov rdx, rbp
L01df: call System.Runtime.CompilerServices.CastHelpers.ChkCastClassSpecial(Void*, System.Object)
L01e4: vmovsd xmm1, [rsi+0x10]
L01e9: vmovsd xmm2, [rax+0x10]
L01ee: vucomisd xmm2, xmm1
L01f2: ja L02f9
L01f8: vucomisd xmm1, xmm2
L01fc: ja L02c5
L0202: vucomisd xmm1, xmm2
L0206: jp short L020e
L0208: je L0309
L020e: mov rcx, rdi
L0211: add rsp, 0x30
L0215: pop rbx
L0216: pop rbp
L0217: pop rsi
L0218: pop rdi
L0219: pop r14
L021b: jmp Microsoft.FSharp.Core.LanguagePrimitives+HashCompare.GenericComparisonWithComparerIntrinsic[[System.Double, System.Private.CoreLib]](System.Collections.IComparer, Double, Double)
L0220: mov rsi, rbx
L0223: mov rcx, 0x7ff953af1178
L022d: cmp [rsi], rcx
L0230: je short L023d
L0232: mov rdx, rbx
L0235: call System.Runtime.CompilerServices.CastHelpers.ChkCastClassSpecial(Void*, System.Object)
L023a: mov rsi, rax
L023d: mov rbx, rbp
L0240: mov rcx, 0x7ff953af1178
L024a: cmp [rbx], rcx
L024d: je short L025a
L024f: mov rdx, rbp
L0252: call System.Runtime.CompilerServices.CastHelpers.ChkCastClassSpecial(Void*, System.Object)
L0257: mov rbx, rax
L025a: add rsi, 0x10
L025e: mov rcx, [rsi]
L0261: mov [rsp+0x20], rcx
L0266: mov rcx, [rsi+8]
L026a: mov [rsp+0x28], rcx
L026f: add rbx, 0x10
L0273: mov rsi, [rbx]
L0276: mov rbx, [rbx+8]
L027a: mov rcx, 0x7ff953af0538
L0284: call 0x00007ff9b182a470
L0289: mov rbp, rax
L028c: lea r14, [rbp+8]
L0290: mov rcx, r14
L0293: mov rdx, rsi
L0296: call 0x00007ff9b182a050
L029b: lea rcx, [r14+8]
L029f: mov rdx, rbx
L02a2: call 0x00007ff9b182a050
L02a7: mov rdx, rbp
L02aa: lea rcx, [rsp+0x20]
L02af: mov r8, rdi
L02b2: call Test+Test+Decision.CompareTo(System.Object, System.Collections.IComparer)
L02b7: jmp short L0316
L02b9: jmp short L0316
L02bb: jmp short L0316
L02bd: jmp short L0316
L02bf: jmp short L0316
L02c1: sub eax, esi
L02c3: jmp short L0316
L02c5: mov eax, 1
L02ca: add rsp, 0x30
L02ce: pop rbx
L02cf: pop rbp
L02d0: pop rsi
L02d1: pop rdi
L02d2: pop r14
L02d4: ret
L02d5: mov rax, rsi
L02d8: test rax, rax
L02db: je short L02f4
L02dd: mov rcx, 0x7ff953af0bf0
L02e7: cmp [rax], rcx
L02ea: je short L02f4
L02ec: mov rdx, rsi
L02ef: call System.Runtime.CompilerServices.CastHelpers.ChkCastClassSpecial(Void*, System.Object)
L02f4: test rax, rax
L02f7: je short L0309
L02f9: mov eax, 0xffffffff
L02fe: add rsp, 0x30
L0302: pop rbx
L0303: pop rbp
L0304: pop rsi
L0305: pop rdi
L0306: pop r14
L0308: ret
L0309: xor eax, eax
L030b: add rsp, 0x30
L030f: pop rbx
L0310: pop rbp
L0311: pop rsi
L0312: pop rdi
L0313: pop r14
L0315: ret
L0316: add rsp, 0x30
L031a: pop rbx
L031b: pop rbp
L031c: pop rsi
L031d: pop rdi
L031e: pop r14
L0320: ret

;Test+Test+LinearExpr.GetHashCode(System.Collections.IEqualityComparer)
L0000: push rdi
L0001: push rsi
L0002: push rbp
L0003: push rbx
L0004: sub rsp, 0x38
L0008: vzeroupper
L000b: xor eax, eax
L000d: mov [rsp+0x28], rax
L0012: mov [rsp+0x30], rax
L0017: mov rsi, rcx
L001a: mov rdi, rdx
L001d: test rsi, rsi
L0020: je L017b
L0026: mov ebx, [rsi+8]
L0029: cmp ebx, 3
L002c: ja short L0046
L002e: mov ecx, ebx
L0030: lea rdx, [Test+Test+LinearExpr.GetHashCode(System.Collections.IEqualityComparer)]
L0037: mov edx, [rdx+rcx*4]
L003a: lea rax, [L001d]
L0041: add rdx, rax
L0044: jmp rdx
L0046: mov rcx, rsi
L0049: mov rdx, 0x7ff953af0ff8
L0053: cmp [rcx], rdx
L0056: je short L0063
L0058: mov rcx, rdx
L005b: mov rdx, rsi
L005e: call System.Runtime.CompilerServices.CastHelpers.ChkCastClassSpecial(Void*, System.Object)
L0063: vmovsd xmm1, [rsi+0x10]
L0068: mov rcx, rdi
L006b: call Microsoft.FSharp.Core.LanguagePrimitives+HashCompare.GenericHashWithComparerIntrinsic[[System.Double, System.Private.CoreLib]](System.Collections.IEqualityComparer, Double)
L0070: lea ebp, [rax-0x61c88647]
L0076: mov eax, ebp
L0078: jmp L0172
L007d: mov rcx, rsi
L0080: mov rdx, 0x7ff953af1178
L008a: cmp [rcx], rdx
L008d: je short L009a
L008f: mov rcx, rdx
L0092: mov rdx, rsi
L0095: call System.Runtime.CompilerServices.CastHelpers.ChkCastClassSpecial(Void*, System.Object)
L009a: add rsi, 0x10
L009e: mov rcx, [rsi]
L00a1: mov [rsp+0x28], rcx
L00a6: mov rcx, [rsi+8]
L00aa: mov [rsp+0x30], rcx
L00af: lea rcx, [rsp+0x28]
L00b4: mov rdx, rdi
L00b7: call Test+Test+Decision.GetHashCode(System.Collections.IEqualityComparer)
L00bc: lea ebp, [rax-0x61c88607]
L00c2: mov eax, ebp
L00c4: jmp L0172
L00c9: mov rcx, rsi
L00cc: mov rdx, 0x7ff953af1320
L00d6: cmp [rcx], rdx
L00d9: je short L00e6
L00db: mov rcx, rdx
L00de: mov rdx, rsi
L00e1: call System.Runtime.CompilerServices.CastHelpers.ChkCastClassSpecial(Void*, System.Object)
L00e6: mov rcx, [rsi+0x10]
L00ea: mov rdx, rdi
L00ed: cmp [rcx], ecx
L00ef: call Test+Test+LinearExpr.GetHashCode(System.Collections.IEqualityComparer)
L00f4: lea ebp, [rax-0x61c885c7]
L00fa: vmovsd xmm1, [rsi+0x18]
L00ff: mov rcx, rdi
L0102: call Microsoft.FSharp.Core.LanguagePrimitives+HashCompare.GenericHashWithComparerIntrinsic[[System.Double, System.Private.CoreLib]](System.Collections.IEqualityComparer, Double)
L0107: mov ecx, ebp
L0109: shl ecx, 6
L010c: add ecx, eax
L010e: mov edx, ebp
L0110: sar edx, 2
L0113: lea ebp, [rcx+rdx-0x61c88647]
L011a: mov eax, ebp
L011c: jmp short L0172
L011e: mov rcx, rsi
L0121: mov rdx, 0x7ff953af14c8
L012b: cmp [rcx], rdx
L012e: je short L013b
L0130: mov rcx, rdx
L0133: mov rdx, rsi
L0136: call System.Runtime.CompilerServices.CastHelpers.ChkCastClassSpecial(Void*, System.Object)
L013b: mov rcx, [rsi+0x18]
L013f: mov rdx, rdi
L0142: cmp [rcx], ecx
L0144: call Test+Test+LinearExpr.GetHashCode(System.Collections.IEqualityComparer)
L0149: lea ebp, [rax-0x61c88587]
L014f: mov rcx, [rsi+0x10]
L0153: mov rdx, rdi
L0156: cmp [rcx], ecx
L0158: call Test+Test+LinearExpr.GetHashCode(System.Collections.IEqualityComparer)
L015d: mov edx, ebp
L015f: shl edx, 6
L0162: add eax, edx
L0164: mov edx, ebp
L0166: sar edx, 2
L0169: lea ebp, [rax+rdx-0x61c88647]
L0170: mov eax, ebp
L0172: add rsp, 0x38
L0176: pop rbx
L0177: pop rbp
L0178: pop rsi
L0179: pop rdi
L017a: ret
L017b: xor eax, eax
L017d: add rsp, 0x38
L0181: pop rbx
L0182: pop rbp
L0183: pop rsi
L0184: pop rdi
L0185: ret

;Test+Test+LinearExpr.GetHashCode()
L0000: mov rdx, 0x191f2a86fa8
L000a: mov rdx, [rdx]
L000d: cmp [rcx], ecx
L000f: jmp Test+Test+LinearExpr.GetHashCode(System.Collections.IEqualityComparer)

;Test+Test+LinearExpr.Equals(System.Object, System.Collections.IEqualityComparer)
L0000: push r14
L0002: push rdi
L0003: push rsi
L0004: push rbp
L0005: push rbx
L0006: sub rsp, 0x30
L000a: vzeroupper
L000d: xor eax, eax
L000f: mov [rsp+0x20], rax
L0014: mov [rsp+0x28], rax
L0019: mov rsi, r8
L001c: mov rdi, rcx
L001f: test rdi, rdi
L0022: je L023a
L0028: mov rcx, 0x7ff953af0bf0
L0032: call System.Runtime.CompilerServices.CastHelpers.IsInstanceOfClass(Void*, System.Object)
L0037: mov rbx, rax
L003a: test rbx, rbx
L003d: je L022d
L0043: mov ebp, [rdi+8]
L0046: mov ecx, ebp
L0048: mov edx, [rbx+8]
L004b: cmp ecx, edx
L004d: jne L022d
L0053: cmp ebp, 3
L0056: ja L013a
L005c: mov ecx, ebp
L005e: lea rdx, [Test+Test+LinearExpr.Equals(System.Object, System.Collections.IEqualityComparer)]
L0065: mov edx, [rdx+rcx*4]
L0068: lea rax, [L001c]
L006f: add rdx, rax
L0072: jmp rdx
L0074: mov r14, rdi
L0077: mov rcx, 0x7ff953af1320
L0081: cmp [r14], rcx
L0084: je short L0091
L0086: mov rdx, rdi
L0089: call System.Runtime.CompilerServices.CastHelpers.ChkCastClassSpecial(Void*, System.Object)
L008e: mov r14, rax
L0091: mov rdi, rbx
L0094: mov rcx, 0x7ff953af1320
L009e: cmp [rdi], rcx
L00a1: je short L00ae
L00a3: mov rdx, rbx
L00a6: call System.Runtime.CompilerServices.CastHelpers.ChkCastClassSpecial(Void*, System.Object)
L00ab: mov rdi, rax
L00ae: vmovsd xmm0, [r14+0x18]
L00b4: vucomisd xmm0, [rdi+0x18]
L00b9: jp L022d
L00bf: jne L022d
L00c5: mov rcx, [r14+0x10]
L00c9: mov rdx, [rdi+0x10]
L00cd: mov rdi, rcx
L00d0: jmp L001f
L00d5: mov rbp, rdi
L00d8: mov rcx, 0x7ff953af14c8
L00e2: cmp [rbp], rcx
L00e6: je short L00f3
L00e8: mov rdx, rdi
L00eb: call System.Runtime.CompilerServices.CastHelpers.ChkCastClassSpecial(Void*, System.Object)
L00f0: mov rbp, rax
L00f3: mov rdi, rbx
L00f6: mov rcx, 0x7ff953af14c8
L0100: cmp [rdi], rcx
L0103: je short L0110
L0105: mov rdx, rbx
L0108: call System.Runtime.CompilerServices.CastHelpers.ChkCastClassSpecial(Void*, System.Object)
L010d: mov rdi, rax
L0110: mov rcx, [rbp+0x10]
L0114: mov rdx, [rdi+0x10]
L0118: mov r8, rsi
L011b: cmp [rcx], ecx
L011d: call Test+Test+LinearExpr.Equals(System.Object, System.Collections.IEqualityComparer)
L0122: test eax, eax
L0124: je L022d
L012a: mov rcx, [rbp+0x18]
L012e: mov rdx, [rdi+0x18]
L0132: mov rdi, rcx
L0135: jmp L001f
L013a: mov rbp, rdi
L013d: mov rcx, 0x7ff953af0ff8
L0147: cmp [rbp], rcx
L014b: je short L0158
L014d: mov rdx, rdi
L0150: call System.Runtime.CompilerServices.CastHelpers.ChkCastClassSpecial(Void*, System.Object)
L0155: mov rbp, rax
L0158: mov rax, rbx
L015b: mov rcx, 0x7ff953af0ff8
L0165: cmp [rax], rcx
L0168: je short L0172
L016a: mov rdx, rbx
L016d: call System.Runtime.CompilerServices.CastHelpers.ChkCastClassSpecial(Void*, System.Object)
L0172: vmovsd xmm0, [rbp+0x10]
L0177: vucomisd xmm0, [rax+0x10]
L017c: setnp r14b
L0180: jp short L0186
L0182: sete r14b
L0186: movzx r14d, r14b
L018a: jmp L0245
L018f: mov rbp, rdi
L0192: mov rcx, 0x7ff953af1178
L019c: cmp [rbp], rcx
L01a0: je short L01ad
L01a2: mov rdx, rdi
L01a5: call System.Runtime.CompilerServices.CastHelpers.ChkCastClassSpecial(Void*, System.Object)
L01aa: mov rbp, rax
L01ad: mov rdi, rbx
L01b0: mov rcx, 0x7ff953af1178
L01ba: cmp [rdi], rcx
L01bd: je short L01ca
L01bf: mov rdx, rbx
L01c2: call System.Runtime.CompilerServices.CastHelpers.ChkCastClassSpecial(Void*, System.Object)
L01c7: mov rdi, rax
L01ca: add rbp, 0x10
L01ce: mov rcx, [rbp]
L01d2: mov [rsp+0x20], rcx
L01d7: mov rcx, [rbp+8]
L01db: mov [rsp+0x28], rcx
L01e0: add rdi, 0x10
L01e4: mov rbx, [rdi]
L01e7: mov rdi, [rdi+8]
L01eb: mov rcx, 0x7ff953af0538
L01f5: call 0x00007ff9b182a470
L01fa: mov rbp, rax
L01fd: lea r14, [rbp+8]
L0201: mov rcx, r14
L0204: mov rdx, rbx
L0207: call 0x00007ff9b182a050
L020c: lea rcx, [r14+8]
L0210: mov rdx, rdi
L0213: call 0x00007ff9b182a050
L0218: mov rdx, rbp
L021b: lea rcx, [rsp+0x20]
L0220: mov r8, rsi
L0223: call Test+Test+Decision.Equals(System.Object, System.Collections.IEqualityComparer)
L0228: mov r14d, eax
L022b: jmp short L0245
L022d: xor eax, eax
L022f: add rsp, 0x30
L0233: pop rbx
L0234: pop rbp
L0235: pop rsi
L0236: pop rdi
L0237: pop r14
L0239: ret
L023a: test rdx, rdx
L023d: sete r14b
L0241: movzx r14d, r14b
L0245: movzx eax, r14b
L0249: add rsp, 0x30
L024d: pop rbx
L024e: pop rbp
L024f: pop rsi
L0250: pop rdi
L0251: pop r14
L0253: ret

;Test+Test+LinearExpr.Equals(LinearExpr)
L0000: push rdi
L0001: push rsi
L0002: push rbx
L0003: sub rsp, 0x40
L0007: vzeroupper
L000a: vmovaps [rsp+0x30], xmm6
L0010: vmovaps [rsp+0x20], xmm7
L0016: mov rsi, rdx
L0019: mov rdx, rcx
L001c: test rdx, rdx
L001f: je L022c
L0025: test rsi, rsi
L0028: je L0216
L002e: mov edi, [rdx+8]
L0031: mov ecx, edi
L0033: mov eax, [rsi+8]
L0036: cmp ecx, eax
L0038: jne L0216
L003e: cmp edi, 3
L0041: ja L012e
L0047: mov ecx, edi
L0049: lea rax, [Test+Test+LinearExpr.Equals(LinearExpr)]
L0050: mov eax, [rax+rcx*4]
L0053: lea r8, [L0019]
L005a: add rax, r8
L005d: jmp rax
L005f: mov rbx, rdx
L0062: mov rcx, 0x7ff953af1320
L006c: cmp [rbx], rcx
L006f: je short L0079
L0071: call System.Runtime.CompilerServices.CastHelpers.ChkCastClassSpecial(Void*, System.Object)
L0076: mov rbx, rax
L0079: mov rdi, rsi
L007c: mov rcx, 0x7ff953af1320
L0086: cmp [rdi], rcx
L0089: je short L0096
L008b: mov rdx, rsi
L008e: call System.Runtime.CompilerServices.CastHelpers.ChkCastClassSpecial(Void*, System.Object)
L0093: mov rdi, rax
L0096: vmovsd xmm6, [rbx+0x18]
L009b: vmovsd xmm7, [rdi+0x18]
L00a0: vucomisd xmm6, xmm7
L00a4: jp short L00a8
L00a6: je short L00c6
L00a8: vucomisd xmm6, xmm6
L00ac: jp short L00b4
L00ae: je L0216
L00b4: vucomisd xmm7, xmm7
L00b8: setp cl
L00bb: movzx ecx, cl
L00be: test ecx, ecx
L00c0: je L0216
L00c6: mov rdx, [rbx+0x10]
L00ca: mov rsi, [rdi+0x10]
L00ce: jmp L001c
L00d3: mov rdi, rdx
L00d6: mov rcx, 0x7ff953af14c8
L00e0: cmp [rdi], rcx
L00e3: je short L00ed
L00e5: call System.Runtime.CompilerServices.CastHelpers.ChkCastClassSpecial(Void*, System.Object)
L00ea: mov rdi, rax
L00ed: mov rbx, rsi
L00f0: mov rcx, 0x7ff953af14c8
L00fa: cmp [rbx], rcx
L00fd: je short L010a
L00ff: mov rdx, rsi
L0102: call System.Runtime.CompilerServices.CastHelpers.ChkCastClassSpecial(Void*, System.Object)
L0107: mov rbx, rax
L010a: mov rcx, [rdi+0x10]
L010e: mov rdx, [rbx+0x10]
L0112: cmp [rcx], ecx
L0114: call Test+Test+LinearExpr.Equals(LinearExpr)
L0119: test eax, eax
L011b: je L0216
L0121: mov rdx, [rdi+0x18]
L0125: mov rsi, [rbx+0x18]
L0129: jmp L001c
L012e: mov rdi, rdx
L0131: mov rcx, 0x7ff953af0ff8
L013b: cmp [rdi], rcx
L013e: je short L0148
L0140: call System.Runtime.CompilerServices.CastHelpers.ChkCastClassSpecial(Void*, System.Object)
L0145: mov rdi, rax
L0148: mov rax, rsi
L014b: mov rcx, 0x7ff953af0ff8
L0155: cmp [rax], rcx
L0158: je short L0162
L015a: mov rdx, rsi
L015d: call System.Runtime.CompilerServices.CastHelpers.ChkCastClassSpecial(Void*, System.Object)
L0162: vmovsd xmm6, [rdi+0x10]
L0167: vmovsd xmm7, [rax+0x10]
L016c: vucomisd xmm6, xmm7
L0170: jp short L018d
L0172: jne short L018d
L0174: mov eax, 1
L0179: vmovaps xmm6, [rsp+0x30]
L017f: vmovaps xmm7, [rsp+0x20]
L0185: add rsp, 0x40
L0189: pop rbx
L018a: pop rsi
L018b: pop rdi
L018c: ret
L018d: vucomisd xmm6, xmm6
L0191: jp short L0199
L0193: je L0216
L0199: vucomisd xmm7, xmm7
L019d: setp bl
L01a0: movzx ebx, bl
L01a3: jmp L0235
L01a8: mov rdi, rdx
L01ab: mov rcx, 0x7ff953af1178
L01b5: cmp [rdi], rcx
L01b8: je short L01c2
L01ba: call System.Runtime.CompilerServices.CastHelpers.ChkCastClassSpecial(Void*, System.Object)
L01bf: mov rdi, rax
L01c2: mov rbx, rsi
L01c5: mov rcx, 0x7ff953af1178
L01cf: cmp [rbx], rcx
L01d2: je short L01df
L01d4: mov rdx, rsi
L01d7: call System.Runtime.CompilerServices.CastHelpers.ChkCastClassSpecial(Void*, System.Object)
L01dc: mov rbx, rax
L01df: add rdi, 0x10
L01e3: mov rcx, [rdi]
L01e6: mov rsi, [rdi+8]
L01ea: add rbx, 0x10
L01ee: mov rdx, [rbx]
L01f1: mov rdi, [rbx+8]
L01f5: cmp [rcx], ecx
L01f7: call Test+Test+DecisionName.Equals(DecisionName)
L01fc: test eax, eax
L01fe: je short L0212
L0200: mov rcx, rsi
L0203: mov rdx, rdi
L0206: cmp [rcx], ecx
L0208: call Test+Test+DecisionType.Equals(DecisionType)
L020d: movzx ebx, al
L0210: jmp short L0214
L0212: xor ebx, ebx
L0214: jmp short L0235
L0216: xor eax, eax
L0218: vmovaps xmm6, [rsp+0x30]
L021e: vmovaps xmm7, [rsp+0x20]
L0224: add rsp, 0x40
L0228: pop rbx
L0229: pop rsi
L022a: pop rdi
L022b: ret
L022c: test rsi, rsi
L022f: sete bl
L0232: movzx ebx, bl
L0235: movzx eax, bl
L0238: vmovaps xmm6, [rsp+0x30]
L023e: vmovaps xmm7, [rsp+0x20]
L0244: add rsp, 0x40
L0248: pop rbx
L0249: pop rsi
L024a: pop rdi
L024b: ret

;Test+Test+LinearExpr.Equals(System.Object)
L0000: push rsi
L0001: sub rsp, 0x20
L0005: mov rsi, rcx
L0008: mov rcx, 0x7ff953af0bf0
L0012: call System.Runtime.CompilerServices.CastHelpers.IsInstanceOfClass(Void*, System.Object)
L0017: test rax, rax
L001a: je short L002e
L001c: mov rcx, rsi
L001f: mov rdx, rax
L0022: cmp [rcx], ecx
L0024: add rsp, 0x20
L0028: pop rsi
L0029: jmp Test+Test+LinearExpr.Equals(LinearExpr)
L002e: xor eax, eax
L0030: add rsp, 0x20
L0034: pop rsi
L0035: ret

;Test+Test+LinearExpr+Add.get_lExpr()
L0000: mov rax, [rcx+0x10]
L0004: ret

;Test+Test+LinearExpr+Add.get_rExpr()
L0000: mov rax, [rcx+0x18]
L0004: ret

;Test+Test+LinearExpr.get_IsFloat()
L0000: cmp dword ptr [rcx+8], 0
L0004: sete al
L0007: movzx eax, al
L000a: ret

;Test+Test+LinearExpr.get_IsDecision()
L0000: cmp dword ptr [rcx+8], 1
L0004: sete al
L0007: movzx eax, al
L000a: ret

;Test+Test+LinearExpr.get_IsScale()
L0000: cmp dword ptr [rcx+8], 2
L0004: sete al
L0007: movzx eax, al
L000a: ret

;Test+Test+LinearExpr.get_IsAdd()
L0000: cmp dword ptr [rcx+8], 3
L0004: sete al
L0007: movzx eax, al
L000a: ret

;Test+Test+LinearExpr.get_Tag()
L0000: mov eax, [rcx+8]
L0003: ret

;Test+Test+LinearExpr.ToString()
L0000: push rdi
L0001: push rsi
L0002: sub rsp, 0x28
L0006: mov rsi, rcx
L0009: mov rcx, 0x7ff953af84a0
L0013: call 0x00007ff9b182a470
L0018: mov rdi, rax
L001b: mov rdx, 0x191f2a9b3f0
L0025: mov rdx, [rdx]
L0028: lea rcx, [rdi+8]
L002c: call 0x00007ff9b182a080
L0031: xor edx, edx
L0033: mov [rdi+0x10], rdx
L0037: mov [rdi+0x18], rdx
L003b: mov rdx, rdi
L003e: mov rcx, 0x7ff953af8780
L0048: call Microsoft.FSharp.Core.PrintfModule.PrintFormatToStringThen[[System.__Canon, System.Private.CoreLib]](Microsoft.FSharp.Core.PrintfFormat`4<System.__Canon,Microsoft.FSharp.Core.Unit,System.String,System.String>)
L004d: mov rcx, rax
L0050: mov rdx, rsi
L0053: mov rax, [rax]
L0056: mov rax, [rax+0x40]
L005a: mov rax, [rax+0x20]
L005e: add rsp, 0x28
L0062: pop rsi
L0063: pop rdi
L0064: jmp rax

;Test+Test+LinearExpr.CompareTo(LinearExpr)
L0000: push r14
L0002: push rdi
L0003: push rsi
L0004: push rbp
L0005: push rbx
L0006: sub rsp, 0x30
L000a: vzeroupper
L000d: xor eax, eax
L000f: mov [rsp+0x20], rax
L0014: mov [rsp+0x28], rax
L0019: mov rsi, rcx
L001c: mov rdi, rdx
L001f: test rsi, rsi
L0022: je L02e3
L0028: test rdi, rdi
L002b: je L02d3
L0031: mov ebx, [rsi+8]
L0034: mov eax, ebx
L0036: mov ebp, [rdi+8]
L0039: cmp eax, ebp
L003b: jne L02cf
L0041: cmp ebx, 3
L0044: ja short L005e
L0046: mov ecx, ebx
L0048: lea rdx, [Test+Test+LinearExpr.CompareTo(LinearExpr)]
L004f: mov edx, [rdx+rcx*4]
L0052: lea rax, [L001f]
L0059: add rdx, rax
L005c: jmp rdx
L005e: mov rcx, rsi
L0061: mov rdx, 0x7ff953af0ff8
L006b: cmp [rcx], rdx
L006e: je short L007b
L0070: mov rcx, rdx
L0073: mov rdx, rsi
L0076: call System.Runtime.CompilerServices.CastHelpers.ChkCastClassSpecial(Void*, System.Object)
L007b: mov rax, rdi
L007e: mov rcx, 0x7ff953af0ff8
L0088: cmp [rax], rcx
L008b: je short L0095
L008d: mov rdx, rdi
L0090: call System.Runtime.CompilerServices.CastHelpers.ChkCastClassSpecial(Void*, System.Object)
L0095: mov rcx, 0x191f2a86fa0
L009f: mov r14, [rcx]
L00a2: vmovsd xmm1, [rsi+0x10]
L00a7: vmovsd xmm2, [rax+0x10]
L00ac: vucomisd xmm2, xmm1
L00b0: ja L02e8
L00b6: vucomisd xmm1, xmm2
L00ba: ja L02d3
L00c0: vucomisd xmm1, xmm2
L00c4: jp short L00cc
L00c6: je L02f8
L00cc: mov rcx, r14
L00cf: add rsp, 0x30
L00d3: pop rbx
L00d4: pop rbp
L00d5: pop rsi
L00d6: pop rdi
L00d7: pop r14
L00d9: jmp Microsoft.FSharp.Core.LanguagePrimitives+HashCompare.GenericComparisonWithComparerIntrinsic[[System.Double, System.Private.CoreLib]](System.Collections.IComparer, Double, Double)
L00de: mov rcx, rsi
L00e1: mov rdx, 0x7ff953af1178
L00eb: cmp [rcx], rdx
L00ee: je short L00fb
L00f0: mov rcx, rdx
L00f3: mov rdx, rsi
L00f6: call System.Runtime.CompilerServices.CastHelpers.ChkCastClassSpecial(Void*, System.Object)
L00fb: mov rbx, rdi
L00fe: mov rcx, 0x7ff953af1178
L0108: cmp [rbx], rcx
L010b: je short L0118
L010d: mov rdx, rdi
L0110: call System.Runtime.CompilerServices.CastHelpers.ChkCastClassSpecial(Void*, System.Object)
L0115: mov rbx, rax
L0118: mov rcx, 0x191f2a86fa0
L0122: mov r14, [rcx]
L0125: add rsi, 0x10
L0129: mov rcx, [rsi]
L012c: mov [rsp+0x20], rcx
L0131: mov rcx, [rsi+8]
L0135: mov [rsp+0x28], rcx
L013a: add rbx, 0x10
L013e: mov rdi, [rbx]
L0141: mov rsi, [rbx+8]
L0145: mov rcx, 0x7ff953af0538
L014f: call 0x00007ff9b182a470
L0154: mov rbx, rax
L0157: lea rbp, [rbx+8]
L015b: mov rcx, rbp
L015e: mov rdx, rdi
L0161: call 0x00007ff9b182a050
L0166: lea rcx, [rbp+8]
L016a: mov rdx, rsi
L016d: call 0x00007ff9b182a050
L0172: mov rdx, rbx
L0175: lea rcx, [rsp+0x20]
L017a: mov r8, r14
L017d: call Test+Test+Decision.CompareTo(System.Object, System.Collections.IComparer)
L0182: jmp L0305
L0187: mov rcx, rsi
L018a: mov rdx, 0x7ff953af1320
L0194: cmp [rcx], rdx
L0197: je short L01a4
L0199: mov rcx, rdx
L019c: mov rdx, rsi
L019f: call System.Runtime.CompilerServices.CastHelpers.ChkCastClassSpecial(Void*, System.Object)
L01a4: mov rbx, rdi
L01a7: mov rcx, 0x7ff953af1320
L01b1: cmp [rbx], rcx
L01b4: je short L01c1
L01b6: mov rdx, rdi
L01b9: call System.Runtime.CompilerServices.CastHelpers.ChkCastClassSpecial(Void*, System.Object)
L01be: mov rbx, rax
L01c1: mov rcx, 0x191f2a86fa0
L01cb: mov r14, [rcx]
L01ce: vmovsd xmm1, [rsi+0x18]
L01d3: vmovsd xmm2, [rbx+0x18]
L01d8: vucomisd xmm2, xmm1
L01dc: jbe short L01e5
L01de: mov eax, 0xffffffff
L01e3: jmp short L0206
L01e5: vucomisd xmm1, xmm2
L01e9: jbe short L01f2
L01eb: mov eax, 1
L01f0: jmp short L0206
L01f2: vucomisd xmm1, xmm2
L01f6: jp short L01fe
L01f8: jne short L01fe
L01fa: xor eax, eax
L01fc: jmp short L0206
L01fe: mov rcx, r14
L0201: call Microsoft.FSharp.Core.LanguagePrimitives+HashCompare.GenericComparisonWithComparerIntrinsic[[System.Double, System.Private.CoreLib]](System.Collections.IComparer, Double, Double)
L0206: test eax, eax
L0208: jge short L020f
L020a: jmp L0305
L020f: test eax, eax
L0211: jle short L0218
L0213: jmp L0305
L0218: mov rcx, 0x191f2a86fa0
L0222: mov r14, [rcx]
L0225: mov rcx, [rsi+0x10]
L0229: mov rdx, [rbx+0x10]
L022d: mov r8, r14
L0230: cmp [rcx], ecx
L0232: add rsp, 0x30
L0236: pop rbx
L0237: pop rbp
L0238: pop rsi
L0239: pop rdi
L023a: pop r14
L023c: jmp Test+Test+LinearExpr.CompareTo(System.Object, System.Collections.IComparer)
L0241: mov rcx, rsi
L0244: mov rdx, 0x7ff953af14c8
L024e: cmp [rcx], rdx
L0251: je short L025e
L0253: mov rcx, rdx
L0256: mov rdx, rsi
L0259: call System.Runtime.CompilerServices.CastHelpers.ChkCastClassSpecial(Void*, System.Object)
L025e: mov rbx, rdi
L0261: mov rcx, 0x7ff953af14c8
L026b: cmp [rbx], rcx
L026e: je short L027b
L0270: mov rdx, rdi
L0273: call System.Runtime.CompilerServices.CastHelpers.ChkCastClassSpecial(Void*, System.Object)
L0278: mov rbx, rax
L027b: mov rcx, 0x191f2a86fa0
L0285: mov r14, [rcx]
L0288: mov rcx, [rsi+0x10]
L028c: mov rdx, [rbx+0x10]
L0290: mov r8, r14
L0293: cmp [rcx], ecx
L0295: call Test+Test+LinearExpr.CompareTo(System.Object, System.Collections.IComparer)
L029a: test eax, eax
L029c: jge short L02a0
L029e: jmp short L0305
L02a0: test eax, eax
L02a2: jle short L02a6
L02a4: jmp short L0305
L02a6: mov rcx, 0x191f2a86fa0
L02b0: mov r14, [rcx]
L02b3: mov rcx, [rsi+0x18]
L02b7: mov rdx, [rbx+0x18]
L02bb: mov r8, r14
L02be: cmp [rcx], ecx
L02c0: add rsp, 0x30
L02c4: pop rbx
L02c5: pop rbp
L02c6: pop rsi
L02c7: pop rdi
L02c8: pop r14
L02ca: jmp Test+Test+LinearExpr.CompareTo(System.Object, System.Collections.IComparer)
L02cf: sub eax, ebp
L02d1: jmp short L0305
L02d3: mov eax, 1
L02d8: add rsp, 0x30
L02dc: pop rbx
L02dd: pop rbp
L02de: pop rsi
L02df: pop rdi
L02e0: pop r14
L02e2: ret
L02e3: test rdi, rdi
L02e6: je short L02f8
L02e8: mov eax, 0xffffffff
L02ed: add rsp, 0x30
L02f1: pop rbx
L02f2: pop rbp
L02f3: pop rsi
L02f4: pop rdi
L02f5: pop r14
L02f7: ret
L02f8: xor eax, eax
L02fa: add rsp, 0x30
L02fe: pop rbx
L02ff: pop rbp
L0300: pop rsi
L0301: pop rdi
L0302: pop r14
L0304: ret
L0305: add rsp, 0x30
L0309: pop rbx
L030a: pop rbp
L030b: pop rsi
L030c: pop rdi
L030d: pop r14
L030f: ret

;Test+Test+LinearExpr.CompareTo(System.Object)
L0000: push rsi
L0001: sub rsp, 0x20
L0005: mov rsi, rcx
L0008: mov rax, rdx
L000b: test rax, rax
L000e: je short L0024
L0010: mov rcx, 0x7ff953af0bf0
L001a: cmp [rax], rcx
L001d: je short L0024
L001f: call System.Runtime.CompilerServices.CastHelpers.ChkCastClassSpecial(Void*, System.Object)
L0024: mov rcx, rsi
L0027: mov rdx, rax
L002a: cmp [rcx], ecx
L002c: add rsp, 0x20
L0030: pop rsi
L0031: jmp Test+Test+LinearExpr.CompareTo(LinearExpr)

;Test+Test+LinearExpr.CompareTo(System.Object, System.Collections.IComparer)
L0000: push r14
L0002: push rdi
L0003: push rsi
L0004: push rbp
L0005: push rbx
L0006: sub rsp, 0x30
L000a: vzeroupper
L000d: xor eax, eax
L000f: mov [rsp+0x20], rax
L0014: mov [rsp+0x28], rax
L0019: mov rsi, rdx
L001c: mov rdi, r8
L001f: mov rbx, rcx
L0022: mov rbp, rsi
L0025: test rbp, rbp
L0028: je short L0045
L002a: mov rcx, 0x7ff953af0bf0
L0034: cmp [rbp], rcx
L0038: je short L0045
L003a: mov rdx, rsi
L003d: call System.Runtime.CompilerServices.CastHelpers.ChkCastClassSpecial(Void*, System.Object)
L0042: mov rbp, rax
L0045: test rbx, rbx
L0048: je L02d5
L004e: test rbp, rbp
L0051: je short L006b
L0053: mov rcx, 0x7ff953af0bf0
L005d: cmp [rbp], rcx
L0061: je short L006b
L0063: mov rdx, rbp
L0066: call System.Runtime.CompilerServices.CastHelpers.ChkCastClassSpecial(Void*, System.Object)
L006b: test rbp, rbp
L006e: je L02c5
L0074: mov r14d, [rbx+8]
L0078: mov eax, r14d
L007b: mov esi, [rbp+8]
L007e: cmp eax, esi
L0080: jne L02c1
L0086: cmp r14d, 3
L008a: ja L01ad
L0090: mov ecx, r14d
L0093: lea rdx, [Test+Test+LinearExpr.CompareTo(System.Object, System.Collections.IComparer)]
L009a: mov edx, [rdx+rcx*4]
L009d: lea rax, [L001f]
L00a4: add rdx, rax
L00a7: jmp rdx
L00a9: mov rsi, rbx
L00ac: mov rcx, 0x7ff953af1320
L00b6: cmp [rsi], rcx
L00b9: je short L00c6
L00bb: mov rdx, rbx
L00be: call System.Runtime.CompilerServices.CastHelpers.ChkCastClassSpecial(Void*, System.Object)
L00c3: mov rsi, rax
L00c6: mov rbx, rbp
L00c9: mov rcx, 0x7ff953af1320
L00d3: cmp [rbx], rcx
L00d6: je short L00e3
L00d8: mov rdx, rbp
L00db: call System.Runtime.CompilerServices.CastHelpers.ChkCastClassSpecial(Void*, System.Object)
L00e0: mov rbx, rax
L00e3: vmovsd xmm1, [rsi+0x18]
L00e8: vmovsd xmm2, [rbx+0x18]
L00ed: vucomisd xmm2, xmm1
L00f1: jbe short L00fa
L00f3: mov eax, 0xffffffff
L00f8: jmp short L011b
L00fa: vucomisd xmm1, xmm2
L00fe: jbe short L0107
L0100: mov eax, 1
L0105: jmp short L011b
L0107: vucomisd xmm1, xmm2
L010b: jp short L0113
L010d: jne short L0113
L010f: xor eax, eax
L0111: jmp short L011b
L0113: mov rcx, rdi
L0116: call Microsoft.FSharp.Core.LanguagePrimitives+HashCompare.GenericComparisonWithComparerIntrinsic[[System.Double, System.Private.CoreLib]](System.Collections.IComparer, Double, Double)
L011b: test eax, eax
L011d: jl L02b9
L0123: test eax, eax
L0125: jg L02bb
L012b: mov rcx, [rsi+0x10]
L012f: mov rdx, [rbx+0x10]
L0133: mov rsi, rdx
L0136: mov rbx, rcx
L0139: jmp L0022
L013e: mov rsi, rbx
L0141: mov rcx, 0x7ff953af14c8
L014b: cmp [rsi], rcx
L014e: je short L015b
L0150: mov rdx, rbx
L0153: call System.Runtime.CompilerServices.CastHelpers.ChkCastClassSpecial(Void*, System.Object)
L0158: mov rsi, rax
L015b: mov rbx, rbp
L015e: mov rcx, 0x7ff953af14c8
L0168: cmp [rbx], rcx
L016b: je short L0178
L016d: mov rdx, rbp
L0170: call System.Runtime.CompilerServices.CastHelpers.ChkCastClassSpecial(Void*, System.Object)
L0175: mov rbx, rax
L0178: mov rcx, [rsi+0x10]
L017c: mov rdx, [rbx+0x10]
L0180: mov r8, rdi
L0183: cmp [rcx], ecx
L0185: call Test+Test+LinearExpr.CompareTo(System.Object, System.Collections.IComparer)
L018a: test eax, eax
L018c: jl L02bd
L0192: test eax, eax
L0194: jg L02bf
L019a: mov rcx, [rsi+0x18]
L019e: mov rdx, [rbx+0x18]
L01a2: mov rsi, rdx
L01a5: mov rbx, rcx
L01a8: jmp L0022
L01ad: mov rsi, rbx
L01b0: mov rcx, 0x7ff953af0ff8
L01ba: cmp [rsi], rcx
L01bd: je short L01ca
L01bf: mov rdx, rbx
L01c2: call System.Runtime.CompilerServices.CastHelpers.ChkCastClassSpecial(Void*, System.Object)
L01c7: mov rsi, rax
L01ca: mov rax, rbp
L01cd: mov rcx, 0x7ff953af0ff8
L01d7: cmp [rax], rcx
L01da: je short L01e4
L01dc: mov rdx, rbp
L01df: call System.Runtime.CompilerServices.CastHelpers.ChkCastClassSpecial(Void*, System.Object)
L01e4: vmovsd xmm1, [rsi+0x10]
L01e9: vmovsd xmm2, [rax+0x10]
L01ee: vucomisd xmm2, xmm1
L01f2: ja L02f9
L01f8: vucomisd xmm1, xmm2
L01fc: ja L02c5
L0202: vucomisd xmm1, xmm2
L0206: jp short L020e
L0208: je L0309
L020e: mov rcx, rdi
L0211: add rsp, 0x30
L0215: pop rbx
L0216: pop rbp
L0217: pop rsi
L0218: pop rdi
L0219: pop r14
L021b: jmp Microsoft.FSharp.Core.LanguagePrimitives+HashCompare.GenericComparisonWithComparerIntrinsic[[System.Double, System.Private.CoreLib]](System.Collections.IComparer, Double, Double)
L0220: mov rsi, rbx
L0223: mov rcx, 0x7ff953af1178
L022d: cmp [rsi], rcx
L0230: je short L023d
L0232: mov rdx, rbx
L0235: call System.Runtime.CompilerServices.CastHelpers.ChkCastClassSpecial(Void*, System.Object)
L023a: mov rsi, rax
L023d: mov rbx, rbp
L0240: mov rcx, 0x7ff953af1178
L024a: cmp [rbx], rcx
L024d: je short L025a
L024f: mov rdx, rbp
L0252: call System.Runtime.CompilerServices.CastHelpers.ChkCastClassSpecial(Void*, System.Object)
L0257: mov rbx, rax
L025a: add rsi, 0x10
L025e: mov rcx, [rsi]
L0261: mov [rsp+0x20], rcx
L0266: mov rcx, [rsi+8]
L026a: mov [rsp+0x28], rcx
L026f: add rbx, 0x10
L0273: mov rsi, [rbx]
L0276: mov rbx, [rbx+8]
L027a: mov rcx, 0x7ff953af0538
L0284: call 0x00007ff9b182a470
L0289: mov rbp, rax
L028c: lea r14, [rbp+8]
L0290: mov rcx, r14
L0293: mov rdx, rsi
L0296: call 0x00007ff9b182a050
L029b: lea rcx, [r14+8]
L029f: mov rdx, rbx
L02a2: call 0x00007ff9b182a050
L02a7: mov rdx, rbp
L02aa: lea rcx, [rsp+0x20]
L02af: mov r8, rdi
L02b2: call Test+Test+Decision.CompareTo(System.Object, System.Collections.IComparer)
L02b7: jmp short L0316
L02b9: jmp short L0316
L02bb: jmp short L0316
L02bd: jmp short L0316
L02bf: jmp short L0316
L02c1: sub eax, esi
L02c3: jmp short L0316
L02c5: mov eax, 1
L02ca: add rsp, 0x30
L02ce: pop rbx
L02cf: pop rbp
L02d0: pop rsi
L02d1: pop rdi
L02d2: pop r14
L02d4: ret
L02d5: mov rax, rsi
L02d8: test rax, rax
L02db: je short L02f4
L02dd: mov rcx, 0x7ff953af0bf0
L02e7: cmp [rax], rcx
L02ea: je short L02f4
L02ec: mov rdx, rsi
L02ef: call System.Runtime.CompilerServices.CastHelpers.ChkCastClassSpecial(Void*, System.Object)
L02f4: test rax, rax
L02f7: je short L0309
L02f9: mov eax, 0xffffffff
L02fe: add rsp, 0x30
L0302: pop rbx
L0303: pop rbp
L0304: pop rsi
L0305: pop rdi
L0306: pop r14
L0308: ret
L0309: xor eax, eax
L030b: add rsp, 0x30
L030f: pop rbx
L0310: pop rbp
L0311: pop rsi
L0312: pop rdi
L0313: pop r14
L0315: ret
L0316: add rsp, 0x30
L031a: pop rbx
L031b: pop rbp
L031c: pop rsi
L031d: pop rdi
L031e: pop r14
L0320: ret

;Test+Test+LinearExpr.GetHashCode(System.Collections.IEqualityComparer)
L0000: push rdi
L0001: push rsi
L0002: push rbp
L0003: push rbx
L0004: sub rsp, 0x38
L0008: vzeroupper
L000b: xor eax, eax
L000d: mov [rsp+0x28], rax
L0012: mov [rsp+0x30], rax
L0017: mov rsi, rcx
L001a: mov rdi, rdx
L001d: test rsi, rsi
L0020: je L017b
L0026: mov ebx, [rsi+8]
L0029: cmp ebx, 3
L002c: ja short L0046
L002e: mov ecx, ebx
L0030: lea rdx, [Test+Test+LinearExpr.GetHashCode(System.Collections.IEqualityComparer)]
L0037: mov edx, [rdx+rcx*4]
L003a: lea rax, [L001d]
L0041: add rdx, rax
L0044: jmp rdx
L0046: mov rcx, rsi
L0049: mov rdx, 0x7ff953af0ff8
L0053: cmp [rcx], rdx
L0056: je short L0063
L0058: mov rcx, rdx
L005b: mov rdx, rsi
L005e: call System.Runtime.CompilerServices.CastHelpers.ChkCastClassSpecial(Void*, System.Object)
L0063: vmovsd xmm1, [rsi+0x10]
L0068: mov rcx, rdi
L006b: call Microsoft.FSharp.Core.LanguagePrimitives+HashCompare.GenericHashWithComparerIntrinsic[[System.Double, System.Private.CoreLib]](System.Collections.IEqualityComparer, Double)
L0070: lea ebp, [rax-0x61c88647]
L0076: mov eax, ebp
L0078: jmp L0172
L007d: mov rcx, rsi
L0080: mov rdx, 0x7ff953af1178
L008a: cmp [rcx], rdx
L008d: je short L009a
L008f: mov rcx, rdx
L0092: mov rdx, rsi
L0095: call System.Runtime.CompilerServices.CastHelpers.ChkCastClassSpecial(Void*, System.Object)
L009a: add rsi, 0x10
L009e: mov rcx, [rsi]
L00a1: mov [rsp+0x28], rcx
L00a6: mov rcx, [rsi+8]
L00aa: mov [rsp+0x30], rcx
L00af: lea rcx, [rsp+0x28]
L00b4: mov rdx, rdi
L00b7: call Test+Test+Decision.GetHashCode(System.Collections.IEqualityComparer)
L00bc: lea ebp, [rax-0x61c88607]
L00c2: mov eax, ebp
L00c4: jmp L0172
L00c9: mov rcx, rsi
L00cc: mov rdx, 0x7ff953af1320
L00d6: cmp [rcx], rdx
L00d9: je short L00e6
L00db: mov rcx, rdx
L00de: mov rdx, rsi
L00e1: call System.Runtime.CompilerServices.CastHelpers.ChkCastClassSpecial(Void*, System.Object)
L00e6: mov rcx, [rsi+0x10]
L00ea: mov rdx, rdi
L00ed: cmp [rcx], ecx
L00ef: call Test+Test+LinearExpr.GetHashCode(System.Collections.IEqualityComparer)
L00f4: lea ebp, [rax-0x61c885c7]
L00fa: vmovsd xmm1, [rsi+0x18]
L00ff: mov rcx, rdi
L0102: call Microsoft.FSharp.Core.LanguagePrimitives+HashCompare.GenericHashWithComparerIntrinsic[[System.Double, System.Private.CoreLib]](System.Collections.IEqualityComparer, Double)
L0107: mov ecx, ebp
L0109: shl ecx, 6
L010c: add ecx, eax
L010e: mov edx, ebp
L0110: sar edx, 2
L0113: lea ebp, [rcx+rdx-0x61c88647]
L011a: mov eax, ebp
L011c: jmp short L0172
L011e: mov rcx, rsi
L0121: mov rdx, 0x7ff953af14c8
L012b: cmp [rcx], rdx
L012e: je short L013b
L0130: mov rcx, rdx
L0133: mov rdx, rsi
L0136: call System.Runtime.CompilerServices.CastHelpers.ChkCastClassSpecial(Void*, System.Object)
L013b: mov rcx, [rsi+0x18]
L013f: mov rdx, rdi
L0142: cmp [rcx], ecx
L0144: call Test+Test+LinearExpr.GetHashCode(System.Collections.IEqualityComparer)
L0149: lea ebp, [rax-0x61c88587]
L014f: mov rcx, [rsi+0x10]
L0153: mov rdx, rdi
L0156: cmp [rcx], ecx
L0158: call Test+Test+LinearExpr.GetHashCode(System.Collections.IEqualityComparer)
L015d: mov edx, ebp
L015f: shl edx, 6
L0162: add eax, edx
L0164: mov edx, ebp
L0166: sar edx, 2
L0169: lea ebp, [rax+rdx-0x61c88647]
L0170: mov eax, ebp
L0172: add rsp, 0x38
L0176: pop rbx
L0177: pop rbp
L0178: pop rsi
L0179: pop rdi
L017a: ret
L017b: xor eax, eax
L017d: add rsp, 0x38
L0181: pop rbx
L0182: pop rbp
L0183: pop rsi
L0184: pop rdi
L0185: ret

;Test+Test+LinearExpr.GetHashCode()
L0000: mov rdx, 0x191f2a86fa8
L000a: mov rdx, [rdx]
L000d: cmp [rcx], ecx
L000f: jmp Test+Test+LinearExpr.GetHashCode(System.Collections.IEqualityComparer)

;Test+Test+LinearExpr.Equals(System.Object, System.Collections.IEqualityComparer)
L0000: push r14
L0002: push rdi
L0003: push rsi
L0004: push rbp
L0005: push rbx
L0006: sub rsp, 0x30
L000a: vzeroupper
L000d: xor eax, eax
L000f: mov [rsp+0x20], rax
L0014: mov [rsp+0x28], rax
L0019: mov rsi, r8
L001c: mov rdi, rcx
L001f: test rdi, rdi
L0022: je L023a
L0028: mov rcx, 0x7ff953af0bf0
L0032: call System.Runtime.CompilerServices.CastHelpers.IsInstanceOfClass(Void*, System.Object)
L0037: mov rbx, rax
L003a: test rbx, rbx
L003d: je L022d
L0043: mov ebp, [rdi+8]
L0046: mov ecx, ebp
L0048: mov edx, [rbx+8]
L004b: cmp ecx, edx
L004d: jne L022d
L0053: cmp ebp, 3
L0056: ja L013a
L005c: mov ecx, ebp
L005e: lea rdx, [Test+Test+LinearExpr.Equals(System.Object, System.Collections.IEqualityComparer)]
L0065: mov edx, [rdx+rcx*4]
L0068: lea rax, [L001c]
L006f: add rdx, rax
L0072: jmp rdx
L0074: mov r14, rdi
L0077: mov rcx, 0x7ff953af1320
L0081: cmp [r14], rcx
L0084: je short L0091
L0086: mov rdx, rdi
L0089: call System.Runtime.CompilerServices.CastHelpers.ChkCastClassSpecial(Void*, System.Object)
L008e: mov r14, rax
L0091: mov rdi, rbx
L0094: mov rcx, 0x7ff953af1320
L009e: cmp [rdi], rcx
L00a1: je short L00ae
L00a3: mov rdx, rbx
L00a6: call System.Runtime.CompilerServices.CastHelpers.ChkCastClassSpecial(Void*, System.Object)
L00ab: mov rdi, rax
L00ae: vmovsd xmm0, [r14+0x18]
L00b4: vucomisd xmm0, [rdi+0x18]
L00b9: jp L022d
L00bf: jne L022d
L00c5: mov rcx, [r14+0x10]
L00c9: mov rdx, [rdi+0x10]
L00cd: mov rdi, rcx
L00d0: jmp L001f
L00d5: mov rbp, rdi
L00d8: mov rcx, 0x7ff953af14c8
L00e2: cmp [rbp], rcx
L00e6: je short L00f3
L00e8: mov rdx, rdi
L00eb: call System.Runtime.CompilerServices.CastHelpers.ChkCastClassSpecial(Void*, System.Object)
L00f0: mov rbp, rax
L00f3: mov rdi, rbx
L00f6: mov rcx, 0x7ff953af14c8
L0100: cmp [rdi], rcx
L0103: je short L0110
L0105: mov rdx, rbx
L0108: call System.Runtime.CompilerServices.CastHelpers.ChkCastClassSpecial(Void*, System.Object)
L010d: mov rdi, rax
L0110: mov rcx, [rbp+0x10]
L0114: mov rdx, [rdi+0x10]
L0118: mov r8, rsi
L011b: cmp [rcx], ecx
L011d: call Test+Test+LinearExpr.Equals(System.Object, System.Collections.IEqualityComparer)
L0122: test eax, eax
L0124: je L022d
L012a: mov rcx, [rbp+0x18]
L012e: mov rdx, [rdi+0x18]
L0132: mov rdi, rcx
L0135: jmp L001f
L013a: mov rbp, rdi
L013d: mov rcx, 0x7ff953af0ff8
L0147: cmp [rbp], rcx
L014b: je short L0158
L014d: mov rdx, rdi
L0150: call System.Runtime.CompilerServices.CastHelpers.ChkCastClassSpecial(Void*, System.Object)
L0155: mov rbp, rax
L0158: mov rax, rbx
L015b: mov rcx, 0x7ff953af0ff8
L0165: cmp [rax], rcx
L0168: je short L0172
L016a: mov rdx, rbx
L016d: call System.Runtime.CompilerServices.CastHelpers.ChkCastClassSpecial(Void*, System.Object)
L0172: vmovsd xmm0, [rbp+0x10]
L0177: vucomisd xmm0, [rax+0x10]
L017c: setnp r14b
L0180: jp short L0186
L0182: sete r14b
L0186: movzx r14d, r14b
L018a: jmp L0245
L018f: mov rbp, rdi
L0192: mov rcx, 0x7ff953af1178
L019c: cmp [rbp], rcx
L01a0: je short L01ad
L01a2: mov rdx, rdi
L01a5: call System.Runtime.CompilerServices.CastHelpers.ChkCastClassSpecial(Void*, System.Object)
L01aa: mov rbp, rax
L01ad: mov rdi, rbx
L01b0: mov rcx, 0x7ff953af1178
L01ba: cmp [rdi], rcx
L01bd: je short L01ca
L01bf: mov rdx, rbx
L01c2: call System.Runtime.CompilerServices.CastHelpers.ChkCastClassSpecial(Void*, System.Object)
L01c7: mov rdi, rax
L01ca: add rbp, 0x10
L01ce: mov rcx, [rbp]
L01d2: mov [rsp+0x20], rcx
L01d7: mov rcx, [rbp+8]
L01db: mov [rsp+0x28], rcx
L01e0: add rdi, 0x10
L01e4: mov rbx, [rdi]
L01e7: mov rdi, [rdi+8]
L01eb: mov rcx, 0x7ff953af0538
L01f5: call 0x00007ff9b182a470
L01fa: mov rbp, rax
L01fd: lea r14, [rbp+8]
L0201: mov rcx, r14
L0204: mov rdx, rbx
L0207: call 0x00007ff9b182a050
L020c: lea rcx, [r14+8]
L0210: mov rdx, rdi
L0213: call 0x00007ff9b182a050
L0218: mov rdx, rbp
L021b: lea rcx, [rsp+0x20]
L0220: mov r8, rsi
L0223: call Test+Test+Decision.Equals(System.Object, System.Collections.IEqualityComparer)
L0228: mov r14d, eax
L022b: jmp short L0245
L022d: xor eax, eax
L022f: add rsp, 0x30
L0233: pop rbx
L0234: pop rbp
L0235: pop rsi
L0236: pop rdi
L0237: pop r14
L0239: ret
L023a: test rdx, rdx
L023d: sete r14b
L0241: movzx r14d, r14b
L0245: movzx eax, r14b
L0249: add rsp, 0x30
L024d: pop rbx
L024e: pop rbp
L024f: pop rsi
L0250: pop rdi
L0251: pop r14
L0253: ret

;Test+Test+LinearExpr.Equals(LinearExpr)
L0000: push rdi
L0001: push rsi
L0002: push rbx
L0003: sub rsp, 0x40
L0007: vzeroupper
L000a: vmovaps [rsp+0x30], xmm6
L0010: vmovaps [rsp+0x20], xmm7
L0016: mov rsi, rdx
L0019: mov rdx, rcx
L001c: test rdx, rdx
L001f: je L022c
L0025: test rsi, rsi
L0028: je L0216
L002e: mov edi, [rdx+8]
L0031: mov ecx, edi
L0033: mov eax, [rsi+8]
L0036: cmp ecx, eax
L0038: jne L0216
L003e: cmp edi, 3
L0041: ja L012e
L0047: mov ecx, edi
L0049: lea rax, [Test+Test+LinearExpr.Equals(LinearExpr)]
L0050: mov eax, [rax+rcx*4]
L0053: lea r8, [L0019]
L005a: add rax, r8
L005d: jmp rax
L005f: mov rbx, rdx
L0062: mov rcx, 0x7ff953af1320
L006c: cmp [rbx], rcx
L006f: je short L0079
L0071: call System.Runtime.CompilerServices.CastHelpers.ChkCastClassSpecial(Void*, System.Object)
L0076: mov rbx, rax
L0079: mov rdi, rsi
L007c: mov rcx, 0x7ff953af1320
L0086: cmp [rdi], rcx
L0089: je short L0096
L008b: mov rdx, rsi
L008e: call System.Runtime.CompilerServices.CastHelpers.ChkCastClassSpecial(Void*, System.Object)
L0093: mov rdi, rax
L0096: vmovsd xmm6, [rbx+0x18]
L009b: vmovsd xmm7, [rdi+0x18]
L00a0: vucomisd xmm6, xmm7
L00a4: jp short L00a8
L00a6: je short L00c6
L00a8: vucomisd xmm6, xmm6
L00ac: jp short L00b4
L00ae: je L0216
L00b4: vucomisd xmm7, xmm7
L00b8: setp cl
L00bb: movzx ecx, cl
L00be: test ecx, ecx
L00c0: je L0216
L00c6: mov rdx, [rbx+0x10]
L00ca: mov rsi, [rdi+0x10]
L00ce: jmp L001c
L00d3: mov rdi, rdx
L00d6: mov rcx, 0x7ff953af14c8
L00e0: cmp [rdi], rcx
L00e3: je short L00ed
L00e5: call System.Runtime.CompilerServices.CastHelpers.ChkCastClassSpecial(Void*, System.Object)
L00ea: mov rdi, rax
L00ed: mov rbx, rsi
L00f0: mov rcx, 0x7ff953af14c8
L00fa: cmp [rbx], rcx
L00fd: je short L010a
L00ff: mov rdx, rsi
L0102: call System.Runtime.CompilerServices.CastHelpers.ChkCastClassSpecial(Void*, System.Object)
L0107: mov rbx, rax
L010a: mov rcx, [rdi+0x10]
L010e: mov rdx, [rbx+0x10]
L0112: cmp [rcx], ecx
L0114: call Test+Test+LinearExpr.Equals(LinearExpr)
L0119: test eax, eax
L011b: je L0216
L0121: mov rdx, [rdi+0x18]
L0125: mov rsi, [rbx+0x18]
L0129: jmp L001c
L012e: mov rdi, rdx
L0131: mov rcx, 0x7ff953af0ff8
L013b: cmp [rdi], rcx
L013e: je short L0148
L0140: call System.Runtime.CompilerServices.CastHelpers.ChkCastClassSpecial(Void*, System.Object)
L0145: mov rdi, rax
L0148: mov rax, rsi
L014b: mov rcx, 0x7ff953af0ff8
L0155: cmp [rax], rcx
L0158: je short L0162
L015a: mov rdx, rsi
L015d: call System.Runtime.CompilerServices.CastHelpers.ChkCastClassSpecial(Void*, System.Object)
L0162: vmovsd xmm6, [rdi+0x10]
L0167: vmovsd xmm7, [rax+0x10]
L016c: vucomisd xmm6, xmm7
L0170: jp short L018d
L0172: jne short L018d
L0174: mov eax, 1
L0179: vmovaps xmm6, [rsp+0x30]
L017f: vmovaps xmm7, [rsp+0x20]
L0185: add rsp, 0x40
L0189: pop rbx
L018a: pop rsi
L018b: pop rdi
L018c: ret
L018d: vucomisd xmm6, xmm6
L0191: jp short L0199
L0193: je L0216
L0199: vucomisd xmm7, xmm7
L019d: setp bl
L01a0: movzx ebx, bl
L01a3: jmp L0235
L01a8: mov rdi, rdx
L01ab: mov rcx, 0x7ff953af1178
L01b5: cmp [rdi], rcx
L01b8: je short L01c2
L01ba: call System.Runtime.CompilerServices.CastHelpers.ChkCastClassSpecial(Void*, System.Object)
L01bf: mov rdi, rax
L01c2: mov rbx, rsi
L01c5: mov rcx, 0x7ff953af1178
L01cf: cmp [rbx], rcx
L01d2: je short L01df
L01d4: mov rdx, rsi
L01d7: call System.Runtime.CompilerServices.CastHelpers.ChkCastClassSpecial(Void*, System.Object)
L01dc: mov rbx, rax
L01df: add rdi, 0x10
L01e3: mov rcx, [rdi]
L01e6: mov rsi, [rdi+8]
L01ea: add rbx, 0x10
L01ee: mov rdx, [rbx]
L01f1: mov rdi, [rbx+8]
L01f5: cmp [rcx], ecx
L01f7: call Test+Test+DecisionName.Equals(DecisionName)
L01fc: test eax, eax
L01fe: je short L0212
L0200: mov rcx, rsi
L0203: mov rdx, rdi
L0206: cmp [rcx], ecx
L0208: call Test+Test+DecisionType.Equals(DecisionType)
L020d: movzx ebx, al
L0210: jmp short L0214
L0212: xor ebx, ebx
L0214: jmp short L0235
L0216: xor eax, eax
L0218: vmovaps xmm6, [rsp+0x30]
L021e: vmovaps xmm7, [rsp+0x20]
L0224: add rsp, 0x40
L0228: pop rbx
L0229: pop rsi
L022a: pop rdi
L022b: ret
L022c: test rsi, rsi
L022f: sete bl
L0232: movzx ebx, bl
L0235: movzx eax, bl
L0238: vmovaps xmm6, [rsp+0x30]
L023e: vmovaps xmm7, [rsp+0x20]
L0244: add rsp, 0x40
L0248: pop rbx
L0249: pop rsi
L024a: pop rdi
L024b: ret

;Test+Test+LinearExpr.Equals(System.Object)
L0000: push rsi
L0001: sub rsp, 0x20
L0005: mov rsi, rcx
L0008: mov rcx, 0x7ff953af0bf0
L0012: call System.Runtime.CompilerServices.CastHelpers.IsInstanceOfClass(Void*, System.Object)
L0017: test rax, rax
L001a: je short L002e
L001c: mov rcx, rsi
L001f: mov rdx, rax
L0022: cmp [rcx], ecx
L0024: add rsp, 0x20
L0028: pop rsi
L0029: jmp Test+Test+LinearExpr.Equals(LinearExpr)
L002e: xor eax, eax
L0030: add rsp, 0x20
L0034: pop rsi
L0035: ret

;Test+Test+LinearExpr+Float@DebugTypeProxy..ctor(Float)
L0000: lea rcx, [rcx+8]
L0004: call 0x00007ff9b182a080
L0009: nop
L000a: ret

;Test+Test+LinearExpr+Float@DebugTypeProxy.get_Item()
L0000: vzeroupper
L0003: mov rax, [rcx+8]
L0007: vmovsd xmm0, [rax+0x10]
L000c: ret

;Test+Test+LinearExpr+Decision@DebugTypeProxy..ctor(Decision)
L0000: lea rcx, [rcx+8]
L0004: call 0x00007ff9b182a080
L0009: nop
L000a: ret

;Test+Test+LinearExpr+Decision@DebugTypeProxy.get_Item()
L0000: push rdi
L0001: push rsi
L0002: push rbx
L0003: mov rbx, rdx
L0006: mov rsi, [rcx+8]
L000a: add rsi, 0x10
L000e: mov rdi, rbx
L0011: call 0x00007ff9b182a130
L0016: call 0x00007ff9b182a130
L001b: mov rax, rbx
L001e: pop rbx
L001f: pop rsi
L0020: pop rdi
L0021: ret

;Test+Test+LinearExpr+Scale@DebugTypeProxy..ctor(Scale)
L0000: lea rcx, [rcx+8]
L0004: call 0x00007ff9b182a080
L0009: nop
L000a: ret

;Test+Test+LinearExpr+Scale@DebugTypeProxy.get_scale()
L0000: vzeroupper
L0003: mov rax, [rcx+8]
L0007: vmovsd xmm0, [rax+0x18]
L000c: ret

;Test+Test+LinearExpr+Scale@DebugTypeProxy.get_expr()
L0000: mov rax, [rcx+8]
L0004: mov rax, [rax+0x10]
L0008: ret

;Test+Test+LinearExpr+Add@DebugTypeProxy..ctor(Add)
L0000: lea rcx, [rcx+8]
L0004: call 0x00007ff9b182a080
L0009: nop
L000a: ret

;Test+Test+LinearExpr+Add@DebugTypeProxy.get_lExpr()
L0000: mov rax, [rcx+8]
L0004: mov rax, [rax+0x10]
L0008: ret

;Test+Test+LinearExpr+Add@DebugTypeProxy.get_rExpr()
L0000: mov rax, [rcx+8]
L0004: mov rax, [rax+0x18]
L0008: ret

Test+Test+SliceMap`2..ctor(System.Collections.Generic.IComparer`1<!0>, System.ReadOnlyMemory`1<!0>, System.ReadOnlyMemory`1<!1>)
; generic method cannot be jitted. provide explicit types

Test+Test+SliceMap`2..ctor(System.Collections.Generic.IEnumerable`1<System.Tuple`2<!0,!1>>)
; generic method cannot be jitted. provide explicit types

Test+Test+SliceMap`2.get_Keys()
; generic method cannot be jitted. provide explicit types

Test+Test+SliceMap`2.get_Values()
; generic method cannot be jitted. provide explicit types

Test+Test+SliceMap`2.get_Comparer()
; generic method cannot be jitted. provide explicit types

Test+Test+data@98.Invoke(System.Tuple`2<!0,!1>)
; generic method cannot be jitted. provide explicit types
