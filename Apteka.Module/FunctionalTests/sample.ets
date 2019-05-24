;#DropDB AptekaEasyTest

#Application Apteka.Win
#Application Apteka.Web

*FillForm
 User Name = Admin
*Action Log On

#IfDef Apteka.Win

*Action About...
 
*Action OK

#EndIf

#IfDef Apteka.Web
*Action Navigation(My Details)
#EndIf
