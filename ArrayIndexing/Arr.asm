
;Arr.test1(Double[])
L0000: vzeroupper
L0003: vxorps xmm0, xmm0, xmm0
L0007: xor eax, eax
L0009: mov edx, [rcx+8]
L000c: cmp eax, edx
L000e: jge short L001e
L0010: movsxd r8, eax
L0013: vaddsd xmm0, xmm0, [rcx+r8*8+0x10]
L001a: inc eax
L001c: jmp short L000c
L001e: ret

;Arr.test2(StructWrapper`1<Double>)
L0000: vzeroupper
L0003: vxorps xmm0, xmm0, xmm0
L0007: xor eax, eax
L0009: mov edx, [rcx+8]
L000c: cmp eax, edx
L000e: jge short L001e
L0010: movsxd r8, eax
L0013: vaddsd xmm0, xmm0, [rcx+r8*8+0x10]
L001a: inc eax
L001c: jmp short L000c
L001e: ret

;Arr.get_numberCount()
L0000: mov eax, 0xf4240
L0005: ret

;Arr.get_rawArray()
L0000: sub rsp, 0x28
L0004: mov rcx, 0x7ffa37f1cdd8
L000e: xor edx, edx
L0010: call 0x00007ffa9534d110
L0015: mov rax, [rax+8]
L0019: add rsp, 0x28
L001d: ret

;Arr.get_structApproach()
L0000: sub rsp, 0x28
L0004: mov rcx, 0x7ffa37f1cdd8
L000e: xor edx, edx
L0010: call 0x00007ffa9534d110
L0015: mov rax, [rax]
L0018: mov rax, [rax+8]
L001c: add rsp, 0x28
L0020: ret

;Arr.get_r1()
L0000: sub rsp, 0x28
L0004: vzeroupper
L0007: mov rcx, 0x7ffa37f1cdd8
L0011: xor edx, edx
L0013: call 0x00007ffa9534d2e0
L0018: vmovsd xmm0, [rax+8]
L001d: add rsp, 0x28
L0021: ret

;Arr.get_r2()
L0000: sub rsp, 0x28
L0004: vzeroupper
L0007: mov rcx, 0x7ffa37f1cdd8
L0011: xor edx, edx
L0013: call 0x00007ffa9534d2e0
L0018: vmovsd xmm0, [rax+0x10]
L001d: add rsp, 0x28
L0021: ret

Arr+StructWrapper`1..ctor(!0[])
; generic method cannot be jitted. provide explicit types

Arr+StructWrapper`1.get_Values()
; generic method cannot be jitted. provide explicit types

Arr+StructWrapper`1.CompareTo(StructWrapper`1<!0>)
; generic method cannot be jitted. provide explicit types

Arr+StructWrapper`1.CompareTo(System.Object)
; generic method cannot be jitted. provide explicit types

Arr+StructWrapper`1.CompareTo(System.Object, System.Collections.IComparer)
; generic method cannot be jitted. provide explicit types

Arr+StructWrapper`1.GetHashCode(System.Collections.IEqualityComparer)
; generic method cannot be jitted. provide explicit types

Arr+StructWrapper`1.GetHashCode()
; generic method cannot be jitted. provide explicit types

Arr+StructWrapper`1.Equals(System.Object, System.Collections.IEqualityComparer)
; generic method cannot be jitted. provide explicit types

Arr+StructWrapper`1.get_Item(Int32)
; generic method cannot be jitted. provide explicit types

Arr+StructWrapper`1.get_Length()
; generic method cannot be jitted. provide explicit types

Arr+StructWrapper`1.Equals(StructWrapper`1<!0>)
; generic method cannot be jitted. provide explicit types

Arr+StructWrapper`1.Equals(System.Object)
; generic method cannot be jitted. provide explicit types

Arr+StructWrapper`1..ctor(!0[])
; generic method cannot be jitted. provide explicit types

Arr+StructWrapper`1.get_Values()
; generic method cannot be jitted. provide explicit types

Arr+StructWrapper`1.CompareTo(StructWrapper`1<!0>)
; generic method cannot be jitted. provide explicit types

Arr+StructWrapper`1.CompareTo(System.Object)
; generic method cannot be jitted. provide explicit types

Arr+StructWrapper`1.CompareTo(System.Object, System.Collections.IComparer)
; generic method cannot be jitted. provide explicit types

Arr+StructWrapper`1.GetHashCode(System.Collections.IEqualityComparer)
; generic method cannot be jitted. provide explicit types

Arr+StructWrapper`1.GetHashCode()
; generic method cannot be jitted. provide explicit types

Arr+StructWrapper`1.Equals(System.Object, System.Collections.IEqualityComparer)
; generic method cannot be jitted. provide explicit types

Arr+StructWrapper`1.get_Item(Int32)
; generic method cannot be jitted. provide explicit types

Arr+StructWrapper`1.get_Length()
; generic method cannot be jitted. provide explicit types

Arr+StructWrapper`1.Equals(StructWrapper`1<!0>)
; generic method cannot be jitted. provide explicit types

Arr+StructWrapper`1.Equals(System.Object)
; generic method cannot be jitted. provide explicit types
