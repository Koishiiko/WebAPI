using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebAPI.dto;
using WebAPI.entity;
using WebAPI.sql;
using WebAPI.utils;

namespace WebAPI.service.impl {
    public class StepService : IStepService {

        private IStepSQL stepSQL { get; }
        private IModuleSQL moduleSQL { get; }
        private IItemSQL itemSQL { get; }
        private IRuleSQL ruleSQL { get; }
        private ISuggestionSQL suggestionSQL { get; }
        private IRoleStepSQL roleStepSQL { get; }

        public StepService(IStepSQL stepSQL, IModuleSQL moduleSQL, IItemSQL itemSQL, IRuleSQL ruleSQL, ISuggestionSQL suggestionSQL, IRoleStepSQL roleStepSQL) {
            this.stepSQL = stepSQL;
            this.moduleSQL = moduleSQL;
            this.itemSQL = itemSQL;
            this.ruleSQL = ruleSQL;
            this.suggestionSQL = suggestionSQL;
            this.roleStepSQL = roleStepSQL;
        }

        public List<Step> GetAll() {
            return stepSQL.GetAll();
        }

        public Step GetById(int id) {
            return stepSQL.GetById(id);
        }

        public List<StepDTO> GetStepDatas(AccountJWTPayload payload) {
            List<dynamic> rows = stepSQL.GetStepDatas(payload.Roles.ToArray());

            List<StepDTO> datas = new List<StepDTO>();

            StepDTO curr = null;
            rows.ForEach((row) => {
                if (curr == null || curr.Id != row.s_id) {
                    curr = new StepDTO(row.s_id, row.step_id, row.step_name, new List<Module>());
                }
                if (row.m_id != null) {
                    Module module = new Module(row.m_id, row.step_id, row.module_id, row.module_name);
                    curr.Modules.Add(module);
                }
                if (!datas.Any() || datas.Last().Id != curr.Id) {
                    datas.Add(curr);
                }
            });
            return datas;
        }

        public List<Step> GetSteps(AccountJWTPayload payload) {
            return stepSQL.GetSteps(payload.Roles.ToArray());
        }

        public long Save(Step step) {
            return stepSQL.Save(step);
        }

        public int Update(Step step) {
            return stepSQL.Update(step) ? 1 : 0;
        }

        public int Delete(int id) {
            int res = stepSQL.Delete(id);
            moduleSQL.DeleteByStepId(id);
            itemSQL.DeleteByStepId(id);
            ruleSQL.DeleteByStepId(id);
            suggestionSQL.DeleteByStepId(id);
            roleStepSQL.DeleteByStepId(id);
            return res;
        }
    }
}
