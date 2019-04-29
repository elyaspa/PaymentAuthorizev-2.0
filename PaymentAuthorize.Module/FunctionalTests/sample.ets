;#DropDB PaymentAuthorizeEasyTest

#Application PaymentAuthorize.Win
#Application PaymentAuthorize.Web
#IfDef PaymentAuthorize.Win

*Action About...
 
*Action OK

#EndIf

#IfDef PaymentAuthorize.Web
!Action Navigation(My Details)
#EndIf
