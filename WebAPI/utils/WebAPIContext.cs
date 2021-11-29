using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using WebAPI.entity;


namespace WebAPI.utils {
    public class WebAPIContext : DbContext {

        public WebAPIContext(DbContextOptions<WebAPIContext> options) : base(options) {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder) {
            base.OnModelCreating(modelBuilder);

            Func<string, string> CamelToSnake = str => Regex.Replace(str, @"(?<!^|_)[A-Z]", "_$0").ToLower();

            foreach (var entity in modelBuilder.Model.GetEntityTypes()) {
                entity.SetTableName(CamelToSnake(entity.GetTableName()));

                foreach (var property in entity.GetProperties()) {
                    property.SetColumnName(CamelToSnake(property.GetDefaultColumnBaseName()));
                }
            }
        }

        public DbSet<Step> Step;
        public DbSet<Module> Module;
        public DbSet<Item> Item;
        public DbSet<ItemRule> ItemRule;
        public DbSet<Suggestion> Suggestion;
        public DbSet<Valid> Valid;
        public DbSet<Record> Record;
        public DbSet<Report> Report;
        public DbSet<Detail> Detail;
        public DbSet<ReportTemplate> ReportTemplate;
        public DbSet<Account> Account;
        public DbSet<AccountRole> AccountRole;
        public DbSet<RoleStep> RoleStep;
    }
}
