using risersoft.shared.portable.Model;
using risersoft.shared.portable.Models.HR;
using risersoft.shared.web.IRepository;
using System.Net;
namespace CognitiveServiceRsMx
{

    public interface ISampleRepository : IRepositoryBase<EmployeeInfo, int, EmployeeInfo,bool, RSCallerInfo, HttpStatusCode>
	{

	}
}
