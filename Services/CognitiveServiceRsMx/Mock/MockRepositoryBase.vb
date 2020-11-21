Imports CloudSync.Common
Imports CloudSync.Common.IRepository
Imports CloudSync.Common.Model
Imports risersoft.shared.portable
Imports risersoft.shared.portable.Enums
Imports risersoft.shared.portable.Model

Namespace MockRepository
    Public Class MockRepositoryBase(Of T As BaseInfo, TId)
        Implements IRepositoryBase(Of T, TId, RSCallerInfo)

        Public Property User As RSCallerInfo Implements IRepositoryBase(Of T, TId, RSCallerInfo).User

        Public Property WebController As clsWebControllerBase Implements IRepositoryBase(Of T, TId, RSCallerInfo).WebController

        Public Overridable Function GetAll() As ResultInfo(Of List(Of T), Status) Implements IRepositoryBase(Of T, TId, RSCallerInfo).GetAll
            Throw New NotImplementedException()
        End Function

        Public Overridable Function [Get](id As TId) As ResultInfo(Of T, Status) Implements IRepositoryBase(Of T, TId, RSCallerInfo).Get
            Throw New NotImplementedException()
        End Function

        Public Overridable Function Add(data As T) As ResultInfo(Of Boolean, Status) Implements IRepositoryBase(Of T, TId, RSCallerInfo).Add
            Throw New NotImplementedException()
        End Function

        Public Overridable Function Update(di As TId, data As T) As ResultInfo(Of Boolean, Status) Implements IRepositoryBase(Of T, TId, RSCallerInfo).Update
            Throw New NotImplementedException()
        End Function

        Public Overridable Function Patch(data As T) As ResultInfo(Of Boolean, Status) Implements IRepositoryBase(Of T, TId, RSCallerInfo).Patch
            Throw New NotImplementedException()
        End Function

        Public Overridable Function Delete(id As TId) As ResultInfo(Of Boolean, Status) Implements IRepositoryBase(Of T, TId, RSCallerInfo).Delete
            Throw New NotImplementedException()
        End Function

    End Class
End Namespace