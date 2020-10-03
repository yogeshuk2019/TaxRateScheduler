﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using TaxRateScheduler.Context;

namespace TaxRateScheduler.Migrations
{
    [DbContext(typeof(MunicipalityTaxRateContext))]
    partial class MunicipalityTaxRateContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.1.8")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("TaxRateScheduler.Model.TaxRateModel", b =>
                {
                    b.Property<string>("MunicipalityName")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ScheduleType")
                        .HasColumnType("nvarchar(450)");

                    b.Property<DateTime>("StartDate")
                        .HasColumnType("date");

                    b.Property<DateTime>("EndDate")
                        .HasColumnType("date");

                    b.Property<decimal>("TaxRate")
                        .HasColumnType("decimal(4,2)");

                    b.Property<int>("Year")
                        .HasColumnType("int");

                    b.HasKey("MunicipalityName", "ScheduleType", "StartDate");

                    b.ToTable("tblTaxRates");
                });
#pragma warning restore 612, 618
        }
    }
}
