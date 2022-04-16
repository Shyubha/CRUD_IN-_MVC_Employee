using System.Data.SqlClient;
using System;
using System.Collections.Generic;
namespace CRUD_IN__MVC_Employee.Models
{
    public class EmployeeDAL
    {
        SqlConnection con;
        SqlCommand cmd;
        SqlDataReader dr;
        public EmployeeDAL()
        {
            con = new SqlConnection(Startup.ConnectionString);
        }
        public List<Employee> GetAllDetails()
        {

            List<Employee> list = new List<Employee>();
            cmd = new SqlCommand("select * from Employee", con);
            con.Open();
            dr = cmd.ExecuteReader();
            list = ArrageList(dr);
            con.Close();
            return list;
        }
        public int Save(Employee e)
        {
            cmd = new SqlCommand("insert into Employee values(@empName,@empDept)", con);
            cmd.Parameters.AddWithValue("@empName", e.empName);
            cmd.Parameters.AddWithValue("@empDept", e.empDept);
            con.Open();
            int res = cmd.ExecuteNonQuery();
            con.Close();
            return res;
        }
        public List<Employee> ArrageList(SqlDataReader dr)
        {
            List<Employee> list = new List<Employee>();
            if (dr.HasRows)
            {
                while (dr.Read())
                {
                    Employee e = new Employee();
                    e.empId = Convert.ToInt32(dr["empId"]);
                    e.empName = dr["empName"].ToString();
                    e.empDept = dr["empDept"].ToString();
                    list.Add(e);
                }
                return list;
            }
            else
            {
                return null;
            }

        }
        public Employee GetEmployeeByid(int empId)
        {
            Employee e = new Employee();
            cmd = new SqlCommand("select * from Employee where empId=@empId", con);
            cmd.Parameters.AddWithValue("@empId", empId);
            con.Open();
            dr = cmd.ExecuteReader();
            if (dr.HasRows)
            {
                while (dr.Read())
                {
                   e.empId  = Convert.ToInt32(dr["empId"]);
                   e.empName = dr["empName"].ToString();
                   e.empDept = dr["empDept"].ToString();
                }

            }
            con.Close();
            return e;
        }

        public int Upate(Employee e)
        {
            cmd = new SqlCommand("update Employee set empName=@empName,empDept=@empDept where empId=@empId", con);
            cmd.Parameters.AddWithValue("@empName", e.empName);
            cmd.Parameters.AddWithValue("@empDept", e.empDept);
            cmd.Parameters.AddWithValue("@empId", e.empId);
            con.Open();
            int res = cmd.ExecuteNonQuery();
            con.Close();
            return res;
        }
        public int Delete(int empId)
        {
            cmd = new SqlCommand("delete from Employee where empId=@empId", con);
            cmd.Parameters.AddWithValue("@empId", empId);
            con.Open();
            int res = cmd.ExecuteNonQuery();
            con.Close();
            return res;
        }
    }
}
