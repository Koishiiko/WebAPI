using System.Collections.Generic;
using System.Linq;
using WebAPI.dto;
using WebAPI.entity;
using WebAPI.po;
using WebAPI.sql;
using WebAPI.utils;

namespace WebAPI.service.impl {
    public class StepService : IStepService {

        private readonly IStepSQL stepSQL;
        private readonly IModuleSQL moduleSQL;
        private readonly IItemSQL itemSQL;
        private readonly IRuleSQL ruleSQL;
        private readonly ISuggestionSQL suggestionSQL;
        private readonly IRoleStepSQL roleStepSQL;

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
            List<StepData> rows = stepSQL.GetStepDatas(payload.Roles.ToArray());

            List<StepDTO> datas = new List<StepDTO>();

            StepDTO curr = null;
            rows.ForEach((row) => {
                if (curr == null || curr.Id != row.SId) {
                    curr = new StepDTO() { Id = row.SId, StepId = row.StepId, Name = row.StepName, Modules = new List<Module>() };
                }
                if (row.MId != 0) {
                    Module module = new Module() { Id = row.MId, StepId = row.StepId, ModuleId = row.ModuleId, Name = row.ModuleName };
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
