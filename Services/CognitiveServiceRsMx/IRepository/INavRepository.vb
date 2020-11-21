Imports System.IO
Imports System.Net
Imports risersoft.shared.portable.Model
Imports risersoft.shared.portable.Models
Imports risersoft.shared.portable.Models.HR
Imports risersoft.shared.portable.Models.Nav

Public Interface INavRepository
    Inherits IRepositoryBase(Of clsMobileViewModel, String, Boolean, RSCallerInfo, HttpStatusCode)

    Function GenerateView(Params As clsMobileNavParams) As ResultInfo(Of clsMobileViewModel, HttpStatusCode)


End Interface
