Imports System.IO
Imports SldWorks
Imports SwConst

Class Util

    Shared Sub SetPropriedade(swproMgr As CustomPropertyManager, nomeDaProp As String, valorDaProp As String)

        swCustProp.Add3(nomeDaProp, swCustomInfoType_e.swCustomInfoText, valorDaProp, swCustomPropertyAddOption_e.swCustomPropertyReplaceValue)

    End Sub

End Class
