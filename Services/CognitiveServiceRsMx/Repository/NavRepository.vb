Imports risersoft.shared.portable.Model
Imports risersoft.shared.portable.Enums
Imports risersoft.shared.portable
Imports risersoft.shared.portable.Models.HR
Imports risersoft.shared.portable.Models
Imports System.IO
Imports risersoft.shared
Imports risersoft.app.mxent
Imports risersoft.app.mxform
Imports risersoft.shared.portable.Models.Nav
Imports MobileServiceRsMx
Imports risersoft.shared.Extensions
Imports System.Net

''' <summary>
''' ESS Repository
''' </summary>
''' <remarks></remarks>
Public Class NavRepository
    Inherits ServerRepositoryBase(Of clsMobileViewModel, String)
    Implements INavRepository

    Protected Friend Function GetEmployeeRow() As DataRow
        Dim sql As String = String.Format("select * from hrpListAllEmp() where userid='{0}' and onrolls=1", Me.User.UserId.ToString)
        Dim dt1 As DataTable = Me.WebController.DataProvider.objSQLHelper.ExecuteDataset(Data.CommandType.Text, sql).Tables(0)
        If dt1.Rows.Count > 0 Then Return dt1.Rows(0) Else Return Nothing
    End Function



    Public Function GenerateView(Params As clsMobileNavParams) As ResultInfo(Of clsMobileViewModel, HttpStatusCode) Implements INavRepository.GenerateView
        Dim vwState As New clsViewState(Me.WebController)
        If Params.State IsNot Nothing Then vwState = Params.State.ToViewState(Me.WebController)
        Dim info = WebController.DataProvider.GenerateViewModel(Params.Info, Params.vwKey, vwState, Params.strIDXML, Params.vCall)
        Dim info2 = info.ToMobileView()
        If info2 Is Nothing Then Return BuildResponse(info2, HttpStatusCode.NotFound, "Not found") Else Return BuildResponse(info2)
    End Function


End Class
