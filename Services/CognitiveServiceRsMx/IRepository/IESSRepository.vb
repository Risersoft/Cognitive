Imports System.IO
Imports System.Net
Imports risersoft.shared.portable.Model
Imports risersoft.shared.portable.Models
Imports risersoft.shared.portable.Models.HR

Public Interface IESSRepository
    Inherits IRepositoryBase(Of EmployeeInfo, Integer, Boolean, RSCallerInfo, HttpStatusCode)

End Interface
