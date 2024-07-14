using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace giadinhthoxinh.Entities;

public partial class GiadinhthoxinhContext : DbContext
{
    public GiadinhthoxinhContext()
    {
    }

    public GiadinhthoxinhContext(DbContextOptions<GiadinhthoxinhContext> options)
        : base(options)
    {
    }

    public virtual DbSet<TblCategory> TblCategories { get; set; }

    public virtual DbSet<TblCheckinDetail> TblCheckinDetails { get; set; }

    public virtual DbSet<TblCheckoutDetail> TblCheckoutDetails { get; set; }

    public virtual DbSet<TblImage> TblImages { get; set; }

    public virtual DbSet<TblImportMaterial> TblImportMaterials { get; set; }

    public virtual DbSet<TblImportOrder> TblImportOrders { get; set; }

    public virtual DbSet<TblMaterColor> TblMaterColors { get; set; }

    public virtual DbSet<TblMaterPriceImport> TblMaterPriceImports { get; set; }

    public virtual DbSet<TblMaterSize> TblMaterSizes { get; set; }

    public virtual DbSet<TblMaterial> TblMaterials { get; set; }

    public virtual DbSet<TblOrder> TblOrders { get; set; }

    public virtual DbSet<TblPermission> TblPermissions { get; set; }

    public virtual DbSet<TblProduct> TblProducts { get; set; }

    public virtual DbSet<TblProductColor> TblProductColors { get; set; }

    public virtual DbSet<TblProductPrice> TblProductPrices { get; set; }

    public virtual DbSet<TblProductSize> TblProductSizes { get; set; }

    public virtual DbSet<TblPromote> TblPromotes { get; set; }

    public virtual DbSet<TblReview> TblReviews { get; set; }

    public virtual DbSet<TblSupplier> TblSuppliers { get; set; }

    public virtual DbSet<TblUser> TblUsers { get; set; }

    public DbSet<PatientInfor> PatientInfors { get; set; }
    public DbSet<Diagnose> Diagnoses { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder.UseSqlServer("Name=MyShop");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Diagnose>(entity =>
        {
            entity.ToTable("diagnose");
            entity.HasKey(e => e.DiagnoseId).HasName("diagnose_id");
            entity.Property(e => e.PatientId).HasColumnName("patient_id");
            entity.Property(e => e.MainDisease).HasColumnName("main_disease");
            entity.Property(e => e.IncludingDiseases).HasColumnName("including_diseases");
            entity.Property(e => e.ICDCode).HasColumnName("ICD_code");
        });

        modelBuilder.Entity<PatientInfor>(entity =>
        {
            entity.ToTable("patient");
            entity.HasKey(e => e.PatientId).HasName("patient_id");
            entity.Property(e => e.SocialInsurancePeriod).HasColumnName("social_insurance_period");
            entity.Property(e => e.InsuranceCardNumber).HasColumnName("insurance_card_number");
            entity.Property(e => e.FamilyInformation).HasColumnName("family_information");
            entity.Property(e => e.FamilyInformationPhone).HasColumnName("family_information_phone");
            entity.Property(e => e.TimeComeExamination).HasColumnName("time_come_examination");
            entity.Property(e => e.TimeStartExamination).HasColumnName("time_start_examination");
            entity.Property(e => e.DiagnosisOfReferralSite).HasColumnName("diagnosis_of_referral_site");
            entity.Property(e => e.OrderNumber).HasColumnName("order_number");
            //entity.HasMany(d => d.Diagnoses).WithOne(p => p.Patient);

        });



        //modelBuilder.Entity<PatientInfor>(entity =>
        //{
        //    entity.ToTable("patient");
        //    entity.HasKey(e => e.PatientId).HasName("patient_id");
        //    //entity.Property(e => e.Username).HasColumnName("username");
        //    //entity.Property(e => e.Birthday).HasColumnName("birthday");
        //    //entity.Property(e => e.Age).HasColumnName("age");
        //    //entity.Property(e => e.Gender).HasColumnName("gender");
        //    //entity.Property(e => e.Job).HasColumnName("job");
        //    //entity.Property(e => e.Ethnic).HasColumnName("ethnic");
        //    //entity.Property(e => e.Nationality).HasColumnName("nationality");
        //    //entity.Property(e => e.Address).HasColumnName("address");
        //    //entity.Property(e => e.Workplace).HasColumnName("workplace");
        //    //entity.Property(e => e.Phone).HasColumnName("phone");
        //    //entity.Property(e => e.Type).HasColumnName("type");
        //    //entity.Property(e => e.Subject).HasColumnName("subject");
        //    entity.Property(e => e.SocialInsurancePeriod).HasColumnName("social_insurance_period");
        //    entity.Property(e => e.InsuranceCardNumber).HasColumnName("insurance_card_number");
        //    entity.Property(e => e.FamilyInformation).HasColumnName("family_information");
        //    entity.Property(e => e.FamilyInformationPhone).HasColumnName("family_information_phone");
        //    entity.Property(e => e.TimeComeExamination).HasColumnName("time_come_examination");
        //    entity.Property(e => e.TimeStartExamination).HasColumnName("time_start_examination");
        //    entity.Property(e => e.DiagnosisOfReferralSite).HasColumnName("diagnosis_of_referral_site");
        //});

        modelBuilder.Entity<TblCategory>(entity =>
        {
            entity.HasKey(e => e.PkICategoryId).HasName("PK__tblCateg__1CEB1EFB7725771C");
        });

        modelBuilder.Entity<TblCheckinDetail>(entity =>
        {
            entity.HasKey(e => e.PkICheckinDetailId).HasName("PK__tblCheck__74ECEDDEF3DEC773");

            entity.HasOne(d => d.FkIImportOrder).WithMany(p => p.TblCheckinDetails).HasConstraintName("fk_checkinDetail_importOrder");

            entity.HasOne(d => d.FkIProduct).WithMany(p => p.TblCheckinDetails)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("fk_checkinDetail_product");
        });

        modelBuilder.Entity<TblCheckoutDetail>(entity =>
        {
            entity.HasKey(e => e.PkICheckoutDetailId).HasName("PK__tblCheck__AA0F9D73CFC6D13F");

            entity.HasOne(d => d.FkIOrder).WithMany(p => p.TblCheckoutDetails).HasConstraintName("fk_checkoutDetail_order");

            entity.HasOne(d => d.FkIProduct).WithMany(p => p.TblCheckoutDetails)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("fk_checkoutDetail_product");
        });

        modelBuilder.Entity<TblImage>(entity =>
        {
            entity.HasKey(e => e.PkIImageId).HasName("PK__tblImage__2309338E88158E3D");

            entity.HasOne(d => d.FkIProduct).WithMany(p => p.TblImages).HasConstraintName("fk_img_product");
        });

        modelBuilder.Entity<TblImportMaterial>(entity =>
        {
            entity.HasOne(d => d.FkIImportOrder).WithMany(p => p.TblImportMaterials).HasConstraintName("FK_tblImportMaterial_tblImportOrder");

            entity.HasOne(d => d.FkIMaterial).WithMany(p => p.TblImportMaterials).HasConstraintName("FK_tblImportMaterial_tblMaterial");
        });

        modelBuilder.Entity<TblImportOrder>(entity =>
        {
            entity.HasKey(e => e.PkIImportOrderId).HasName("PK__tblImpor__804C7A8B512FB969");

            entity.HasOne(d => d.FkIAccount).WithMany(p => p.TblImportOrders).HasConstraintName("fk_importOrder_account");

            entity.HasOne(d => d.FkISupplier).WithMany(p => p.TblImportOrders).HasConstraintName("fk_importOrder_supplier");
        });

        modelBuilder.Entity<TblMaterColor>(entity =>
        {
            entity.HasOne(d => d.FkIMaterial).WithMany(p => p.TblMaterColors)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_tblMaterColor_tblMaterial");
        });

        modelBuilder.Entity<TblMaterPriceImport>(entity =>
        {
            entity.HasOne(d => d.FkIMaterial).WithMany(p => p.TblMaterPriceImports).HasConstraintName("FK_tblMaterPriceImport_tblMaterial");
        });

        modelBuilder.Entity<TblMaterSize>(entity =>
        {
            entity.HasOne(d => d.FkIMaterial).WithMany(p => p.TblMaterSizes).HasConstraintName("FK_tblMaterSize_tblMaterial");
        });

        modelBuilder.Entity<TblOrder>(entity =>
        {
            entity.HasKey(e => e.PkIOrderId).HasName("PK__tblOrder__3261310A653333D7");

            entity.HasOne(d => d.FkIAccount).WithMany(p => p.TblOrders).HasConstraintName("fk_order_account");
        });

        modelBuilder.Entity<TblPermission>(entity =>
        {
            entity.HasKey(e => e.PkIPermissionId).HasName("PK__tblPermi__B6AAD449DEF37E33");
        });

        modelBuilder.Entity<TblProduct>(entity =>
        {
            entity.HasKey(e => e.PkIProductId).HasName("PK__tblProdu__2980F3B84306DD52");

            entity.HasOne(d => d.FkICategory).WithMany(p => p.TblProducts).HasConstraintName("fk_product_category");

            entity.HasOne(d => d.FkIPromote).WithMany(p => p.TblProducts).HasConstraintName("fk_product_promote");
        });

        modelBuilder.Entity<TblProductColor>(entity =>
        {
            entity.HasOne(d => d.FkIProduct).WithMany(p => p.TblProductColors).HasConstraintName("FK_tblProductColor_tblProduct");
        });

        modelBuilder.Entity<TblProductPrice>(entity =>
        {
            entity.HasOne(d => d.FkIProduct).WithMany(p => p.TblProductPrices).HasConstraintName("FK_tblProductPrice_tblProduct");
        });

        modelBuilder.Entity<TblProductSize>(entity =>
        {
            entity.HasOne(d => d.FkIProduct).WithMany(p => p.TblProductSizes).HasConstraintName("FK_tblProductSize_tblProduct");
        });

        modelBuilder.Entity<TblPromote>(entity =>
        {
            entity.HasKey(e => e.PkIPromoteId).HasName("PK__tblPromo__498845A71A5830E3");
        });

        modelBuilder.Entity<TblReview>(entity =>
        {
            entity.HasKey(e => e.PkIReviewId).HasName("PK__tblRevie__7180705F11601E83");

            entity.HasOne(d => d.FkIAccount).WithMany(p => p.TblReviews).HasConstraintName("fk_review_account");

            entity.HasOne(d => d.FkIProduct).WithMany(p => p.TblReviews).HasConstraintName("fk_review_product");
        });

        modelBuilder.Entity<TblSupplier>(entity =>
        {
            entity.HasKey(e => e.PkISupplierId).HasName("PK__tblSuppl__C051541594A08A37");
        });

        modelBuilder.Entity<TblUser>(entity =>
        {
            entity.HasKey(e => e.PkIAccountId).HasName("PK__tblUser__A8653C89B0130405");

            entity.HasOne(d => d.FkIPermission).WithMany(p => p.TblUsers).HasConstraintName("fk_user_permission");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
