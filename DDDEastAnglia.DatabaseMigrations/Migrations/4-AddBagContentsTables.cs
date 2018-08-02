using FluentMigrator;

namespace DDDEastAnglia.DatabaseMigrations.Migrations
{
    public class AddBagContentsTables : ForwardOnlyMigration
    {
        public override void Up()
        {
            Create.Table("BagContents")
                .WithColumn("ContentId").AsInt32().NotNullable().Identity().PrimaryKey()
                .WithColumn("SponsorId").AsInt32().NotNullable()
                .WithColumn("ContentText").AsString(int.MaxValue);

            Create.Table("BagFiles")
                .WithColumn("BagFileId").AsInt32().NotNullable().Identity().PrimaryKey()
                .WithColumn("ContentId").AsInt32().NotNullable()
                .WithColumn("Filename").AsString().NotNullable()
                .WithColumn("FileBytes").AsBinary(int.MaxValue).NotNullable()
                .WithColumn("FileMIMEType").AsString(int.MaxValue).NotNullable()
                .WithColumn("FileLength").AsInt32().NotNullable();
        }
    }
}
