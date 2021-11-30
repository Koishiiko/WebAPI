using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebAPI.entity;
using WebAPI.dto;
using WebAPI.pagination;

namespace WebAPI.sql {
    public interface IReportSQL {

        List<Report> GetAll();

        List<ReportDTO> GetByPage(ReportPagination pagination);

        int GetCount(ReportPagination pagination);

        /// <summary>
        /// 获取当前产品的所有记录信息
        /// </summary>
        /// <param name="productId"></param>
        /// <returns></returns>
        List<ReportDTO> GetByProductId(string productId);

        /// <summary>
        /// 只获取当前产品下所有记录的工序id和guid
        /// 
        /// 主要用于查询detail表
        /// </summary>
        /// <param name="productId"></param>
        /// <returns></returns>
        List<ReportDTO> GetAllByProductId(string productId);

        Report GetLastByProductId(int stepId, string productId);

        Report GetByGuid(string guid);

        long Save(Report report);
    }
}
