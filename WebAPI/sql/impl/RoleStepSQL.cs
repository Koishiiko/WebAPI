using System.Collections.Generic;
using WebAPI.entity;
using WebAPI.utils;

namespace WebAPI.sql.impl {
    public class RoleStepSQL : IRoleStepSQL {

        public int SaveSteps(long roleId, List<int> ids) {
            List<RoleStep> roleSteps = new List<RoleStep>();

            ids.ForEach((stepId) => {
                roleSteps.Add(new RoleStep() { RoleId = (int)roleId, StepId = stepId });
            });

            return DataSource.Save(roleSteps);
        }

        public int DeleteByRoleId(int id) {
            return DataSource.DB.Deleteable<RoleStep>().Where(rs => rs.RoleId == id).ExecuteCommand();
        }

        public int DeleteByStepId(int id) {
            return DataSource.DB.Deleteable<RoleStep>().Where(rs => rs.StepId == id).ExecuteCommand();
        }
    }
}
