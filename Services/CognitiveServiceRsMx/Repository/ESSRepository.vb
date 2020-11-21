Imports risersoft.shared.portable.Model
Imports risersoft.shared.portable.Enums
Imports risersoft.shared.portable
Imports risersoft.shared.portable.Models.HR
Imports risersoft.shared.portable.Models
Imports System.IO
Imports risersoft.shared
Imports risersoft.app.mxent
Imports risersoft.app.mxform
Imports System.Reflection
Imports risersoft.shared.cloud
Imports System.Net
Imports risersoft.app.reports.erp
Imports GeoTimeZone
Imports TimeZoneConverter

''' <summary>
''' ESS Repository
''' </summary>
''' <remarks></remarks>
Public Class ESSRepository
    Inherits ServerRepositoryBase(Of EmployeeInfo, Integer)
    Implements IESSRepository

    Protected Friend Function GetEmployeeRow() As DataRow
        Dim sql As String = String.Format("select * from hrpListAllEmp() where userid='{0}' and onrolls=1", Me.User.UserId.ToString)
        Dim dt1 As DataTable = Me.WebController.DataProvider.objSQLHelper.ExecuteDataset(Data.CommandType.Text, sql).Tables(0)
        If dt1.Rows.Count > 0 Then Return dt1.Rows(0) Else Return Nothing
    End Function
    Protected Friend Function GetSubordinates(EmployeeID As Integer) As DataTable
        Dim sql As String = String.Format("select * from hrpListAllEmp() where onrolls=1 and (leaveauth1id={0} or leaveauth2id={0})", EmployeeID)
        Dim dt1 As DataTable = Me.WebController.DataProvider.objSQLHelper.ExecuteDataset(Data.CommandType.Text, sql).Tables(0)
        Return dt1
    End Function

End Class
