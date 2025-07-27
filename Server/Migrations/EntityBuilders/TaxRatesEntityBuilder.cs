using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Migrations.Operations;
using Microsoft.EntityFrameworkCore.Migrations.Operations.Builders;
using Oqtane.Databases.Interfaces;
using Oqtane.Migrations;
using Oqtane.Migrations.EntityBuilders;

namespace GIBS.Module.TaxRates.Migrations.EntityBuilders
{
    public class TaxRatesEntityBuilder : AuditableBaseEntityBuilder<TaxRatesEntityBuilder>
    {
        private const string _entityTableName = "GIBSTaxRates";
        private readonly PrimaryKey<TaxRatesEntityBuilder> _primaryKey = new("PK_GIBSTaxRates", x => x.TaxRatesId);
        private readonly ForeignKey<TaxRatesEntityBuilder> _moduleForeignKey = new("FK_GIBSTaxRates_Module", x => x.ModuleId, "Module", "ModuleId", ReferentialAction.Cascade);

        public TaxRatesEntityBuilder(MigrationBuilder migrationBuilder, IDatabase database) : base(migrationBuilder, database)
        {
            EntityTableName = _entityTableName;
            PrimaryKey = _primaryKey;
            ForeignKeys.Add(_moduleForeignKey);
        }

        protected override TaxRatesEntityBuilder BuildTable(ColumnsBuilder table)
        {
            TaxRatesId = AddAutoIncrementColumn(table,"TaxRatesId");
            ModuleId = AddIntegerColumn(table,"ModuleId");
            Municipality = AddStringColumn(table, "Municipality", 255, false, true);
            Fiscal_Year = AddStringColumn(table, "Fiscal_Year", 4, false, true);
            DOR_Code = AddStringColumn(table, "DOR_Code", 4, true, true);
            Residential = AddDecimalColumn(table,"Residential", 7, 2, false, 0);
            Open_Space = AddDecimalColumn(table, "Open_Space", 7, 2, false, 0);
            Commercial = AddDecimalColumn(table,"Commercial", 7, 2, false, 0);
            Industrial = AddDecimalColumn(table,"Industrial", 7, 2, false, 0);
            Personal_Property = AddDecimalColumn(table, "Personal_Property", 7, 2, false);

            AddAuditableColumns(table);
            return this;
        }

        public OperationBuilder<AddColumnOperation> TaxRatesId { get; set; }
        public OperationBuilder<AddColumnOperation> ModuleId { get; set; }
        public OperationBuilder<AddColumnOperation> Municipality { get; set; }
        public OperationBuilder<AddColumnOperation> Fiscal_Year { get; set; }
        public OperationBuilder<AddColumnOperation> DOR_Code { get; set; }
        public OperationBuilder<AddColumnOperation> Residential { get; set; }
        public OperationBuilder<AddColumnOperation> Open_Space { get; set; }
        public OperationBuilder<AddColumnOperation> Commercial { get; set; }
        public OperationBuilder<AddColumnOperation> Industrial { get; set; }
        public OperationBuilder<AddColumnOperation> Personal_Property { get; set; }

    }
}
