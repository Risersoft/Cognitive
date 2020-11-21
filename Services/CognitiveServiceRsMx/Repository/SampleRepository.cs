using risersoft.shared.portable.Models.HR;
using System.Data;
namespace CognitiveServiceRsMx
{

    /// <summary>
    /// ESS Repository
    /// </summary>
    /// <remarks></remarks>
    public class SampleRepository : ServerRepositoryBase<EmployeeInfo, int>, ISampleRepository
    {

        protected internal DataRow GetEmployeeRow()
        {
            string sql = string.Format("select * from hrpListAllEmp() where userid='{0}' and onrolls=1", this.User.UserId().ToString());
            DataTable dt1 = this.WebController.DataProvider.objSQLHelper.ExecuteDataset(System.Data.CommandType.Text, sql).Tables[0];
            if (dt1.Rows.Count > 0)
                return dt1.Rows[0];
            else
                return null;
        }
        protected internal DataTable GetSubordinates(int EmployeeID)
        {
            string sql = string.Format("select * from hrpListAllEmp() where onrolls=1 and (leaveauth1id={0} or leaveauth2id={0})", EmployeeID);
            DataTable dt1 = this.WebController.DataProvider.objSQLHelper.ExecuteDataset(System.Data.CommandType.Text, sql).Tables[0];
            return dt1;
        }

    }
}
