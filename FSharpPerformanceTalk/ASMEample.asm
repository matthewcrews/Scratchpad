mov         qword ptr [r11+38h],r14  
add         r14,2  
jmp         _PyEval_EvalFrameDefault+5872h (07FFDFD04F462h)  
mov         rdi,qword ptr [r12-8]  
lea         rax,[__ImageBase (07FFDFCE20000h)]  
mov         rsi,qword ptr [r12-10h]  
sub         r12,8  
mov         rdx,rdi  
mov         rcx,rsi  
call        qword ptr [rax+r13*8+3E7A70h]  
mov         qword ptr [rsp+8],rbx  
push        rdi  
sub         rsp,30h  
xor         r8d,r8d  
mov         rdi,rdx  
mov         rbx,rcx  
call        binary_op1 (07FFDFCF06C70h)  
mov         qword ptr [rsp+10h],rbp  
mov         qword ptr [rsp+18h],rsi  
mov         qword ptr [rsp+20h],rdi  
push        r14  
sub         rsp,20h  
mov         rsi,rdx  
movsxd      r9,r8d  
mov         rdx,qword ptr [rcx+8]  
mov         rbp,rcx  
mov         rdi,qword ptr [rdx+60h]  
test        rdi,rdi  
je          binary_op1+2Fh (07FFDFCF06C9Fh)  
mov         rdi,qword ptr [rdi+r9]  
mov         rcx,qword ptr [rsi+8]  
mov         qword ptr [rsp+30h],rbx  
cmp         rcx,rdx  
je          binary_op1+55h (07FFDFCF06CC5h)  
xor         ebx,ebx  
lea         r14,[_Py_NotImplementedStruct (07FFDFD2E6AF0h)]  
test        rdi,rdi  
je          binary_op1+0ADh (07FFDFCF06D1Dh)  
test        rbx,rbx  
je          binary_op1+90h (07FFDFCF06D00h)  
mov         rdx,rsi  
mov         rcx,rbp  
call        rdi  
mov         rax,qword ptr [rcx+8]  
test        dword ptr [rax+0A8h],1000000h  
je          long_add+24h (07FFDFCF60434h)  
mov         rax,qword ptr [rdx+8]  
test        dword ptr [rax+0A8h],1000000h  
jne         _PyLong_Add (07FFDFCF60380h)  
sub         rsp,28h  
mov         r8,rdx  
mov         rdx,qword ptr [rcx+10h]  
lea         rax,[rdx+1]  
cmp         rax,3  
jae         _PyLong_Add+3Eh (07FFDFCF603BEh)  
mov         r9,qword ptr [r8+10h]  
lea         rax,[r9+1]  
cmp         rax,3  
jae         _PyLong_Add+3Eh (07FFDFCF603BEh)  
mov         ecx,dword ptr [rcx+18h]  
mov         eax,dword ptr [r8+18h]  
imul        rcx,rdx  
imul        rax,r9  
add         rcx,rax  
add         rsp,28h  
jmp         _PyLong_FromSTwoDigits (07FFDFCF5AF10h)  
push        rdi  
sub         rsp,20h  
lea         rax,[rcx+5]  
mov         rdi,rcx  
cmp         rax,105h  
ja          _PyLong_FromSTwoDigits+2Fh (07FFDFCF5AF3Fh)  
lea         rax,[rcx+3FFFFFFFh]  
cmp         rax,7FFFFFFFh  
jae         _PyLong_FromSTwoDigits+48h (07FFDFCF5AF58h)  
add         rsp,20h  
pop         rdi  
jmp         _PyLong_FromMedium (07FFDFCF5AE50h)  
mov         qword ptr [rsp+10h],rbx  
push        rsi  
sub         rsp,20h  
movsxd      rsi,ecx  
mov         edx,20h  
mov         rcx,qword ptr [_PyObject (07FFDFD3B8CA8h)]  
call        qword ptr [_PyObject+8h (07FFDFD3B8CB0h)]  
push        rbx  
sub         rsp,20h  
mov         rbx,rdx  
call        pymalloc_alloc (07FFDFCF73C80h)  
sub         rsp,28h  
lea         rax,[rdx-1]  
cmp         rax,1FFh  
jbe         pymalloc_alloc+17h (07FFDFCF73C97h)  
mov         qword ptr [rsp+30h],rbx  
mov         qword ptr [rsp+38h],rsi  
mov         qword ptr [rsp+40h],rdi  
lea         edi,[rdx-1]  
shr         edi,4  
mov         qword ptr [rsp+20h],r14  
lea         r14,[__ImageBase (07FFDFCE20000h)]  
lea         eax,[rdi+rdi]  
lea         rsi,[rax*8+4C7120h]  
mov         rdx,qword ptr [rsi+r14]  
mov         rcx,qword ptr [rdx+10h]  
cmp         rdx,rcx  
je          pymalloc_alloc+9Fh (07FFDFCF73D1Fh)  
mov         r9,qword ptr [rdx+8]  
inc         dword ptr [rdx]  
mov         rax,qword ptr [r9]  
mov         qword ptr [rdx+8],rax  
test        rax,rax  
jne         pymalloc_alloc+1C1h (07FFDFCF73E41h)  
mov         r14,qword ptr [rsp+20h]  
mov         rax,r9  
mov         rdi,qword ptr [rsp+40h]  
mov         rsi,qword ptr [rsp+38h]  
mov         rbx,qword ptr [rsp+30h]  
add         rsp,28h  
ret  
test        rax,rax  
jne         _PyObject_Malloc+46h (07FFDFCF73EA6h)  
add         rsp,20h  
pop         rbx  
ret  
mov         rbx,rax  
test        rax,rax  
jne         _PyLong_FromMedium+4Ah (07FFDFCF5AE9Ah)  
mov         qword ptr [rsp+30h],rdi  
mov         eax,esi  
cdq  
mov         rcx,rsi  
sar         rcx,3Fh  
mov         edi,eax  
and         rcx,0FFFFFFFFFFFFFFFEh  
lea         rax,[PyLong_Type (07FFDFD2E52E0h)]  
xor         edi,edx  
mov         qword ptr [rbx+8],rax  
inc         rcx  
sub         edi,edx  
mov         qword ptr [rbx+10h],rcx  
test        dword ptr [PyLong_Type+0A8h (07FFDFD2E5388h)],200h  
je          _PyLong_FromMedium+88h (07FFDFCF5AED8h)  
cmp         dword ptr [_Py_tracemalloc_config+4h (07FFDFD2E7324h)],0  
je          _PyLong_FromMedium+99h (07FFDFCF5AEE9h)  
mov         dword ptr [rbx+18h],edi  
mov         rax,rbx  
mov         rdi,qword ptr [rsp+30h]  
mov         qword ptr [rbx],1  
mov         rbx,qword ptr [rsp+38h]  
add         rsp,20h  
pop         rsi  
ret  
mov         rcx,rax  
cmp         rax,r14  
jne         binary_op1+0D9h (07FFDFCF06D49h)  
mov         rbx,qword ptr [rsp+30h]  
mov         rbp,qword ptr [rsp+38h]  
mov         rsi,qword ptr [rsp+40h]  
mov         rdi,qword ptr [rsp+48h]  
add         rsp,20h  
pop         r14  
ret  
mov         rcx,rax  
lea         rax,[_Py_NotImplementedStruct (07FFDFD2E6AF0h)]  
cmp         rcx,rax  
je          PyNumber_Add+35h (07FFDFCF073B5h)  
mov         rax,rcx  
mov         rbx,qword ptr [rsp+40h]  
add         rsp,30h  
pop         rdi  
ret  
sub         qword ptr [rsi],1  
mov         r15,rax  
jne         _PyEval_EvalFrameDefault+58AFh (07FFDFD04F49Fh)  
sub         qword ptr [rdi],1  
jne         _PyEval_EvalFrameDefault+58C6h (07FFDFD04F4B6h)  
mov         qword ptr [r12-8],r15  
test        r15,r15  
mov         r15,qword ptr [rsp+30h]  
je          _PyEval_EvalFrameDefault+161h (07FFDFD049D51h)  
add         r14,2  
jmp         _PyEval_EvalFrameDefault+59FCh (07FFDFD04F5ECh)  