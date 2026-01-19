using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using QuickCode.Demo.EmailManagerModule.Domain.Entities;
using QuickCode.Demo.EmailManagerModule.Domain.Enums;

namespace QuickCode.Demo.EmailManagerModule.Persistence.Contexts;

public partial class ReadDbContext : DbContext
{
	public ReadDbContext(DbContextOptions<ReadDbContext> options) : base(options) { }


	public virtual DbSet<Message> Message { get; set; }

	public virtual DbSet<Campaign> Campaign { get; set; }

	public virtual DbSet<MessageLog> MessageLog { get; set; }

	public virtual DbSet<OtpMessageLog> OtpMessageLog { get; set; }

	public virtual DbSet<OtpMessage> OtpMessage { get; set; }

	public virtual DbSet<MessageQueue> MessageQueue { get; set; }

	public virtual DbSet<OtpMessageQueue> OtpMessageQueue { get; set; }

	public virtual DbSet<Sender> Sender { get; set; }

	public virtual DbSet<MessageTemplate> MessageTemplate { get; set; }

	public virtual DbSet<BlackList> BlackList { get; set; }

	public virtual DbSet<AuditLog> AuditLog { get; set; }


	protected override void OnModelCreating(ModelBuilder modelBuilder)
	{
		modelBuilder.Entity<Message>()
		.Property(b => b.AttemptCount)
		.IsRequired()
		.HasDefaultValueSql("0");


		var converterMessageStatus = new ValueConverter<MessageStatus, string>(
		v => v.ToString(),
		v => (MessageStatus)Enum.Parse(typeof(MessageStatus), v));

		modelBuilder.Entity<Message>()
		.Property(b => b.Status)
		.HasConversion(converterMessageStatus);

		modelBuilder.Entity<Campaign>()
		.Property(b => b.IsActive)
		.IsRequired()
		.HasDefaultValue(true);

		modelBuilder.Entity<Campaign>()
		.Property(b => b.Priority)
		.IsRequired()
		.HasDefaultValueSql("1");

		modelBuilder.Entity<MessageLog>()
		.Property(b => b.AttemptCount)
		.IsRequired()
		.HasDefaultValueSql("0");


		var converterMessageLogStatus = new ValueConverter<MessageStatus, string>(
		v => v.ToString(),
		v => (MessageStatus)Enum.Parse(typeof(MessageStatus), v));

		modelBuilder.Entity<MessageLog>()
		.Property(b => b.Status)
		.HasConversion(converterMessageLogStatus);


		var converterOtpMessageLogStatus = new ValueConverter<MessageStatus, string>(
		v => v.ToString(),
		v => (MessageStatus)Enum.Parse(typeof(MessageStatus), v));

		modelBuilder.Entity<OtpMessageLog>()
		.Property(b => b.Status)
		.HasConversion(converterOtpMessageLogStatus);

		modelBuilder.Entity<OtpMessage>()
		.Property(b => b.AttemptCount)
		.IsRequired()
		.HasDefaultValueSql("0");


		var converterOtpMessageStatus = new ValueConverter<MessageStatus, string>(
		v => v.ToString(),
		v => (MessageStatus)Enum.Parse(typeof(MessageStatus), v));

		modelBuilder.Entity<OtpMessage>()
		.Property(b => b.Status)
		.HasConversion(converterOtpMessageStatus);

		modelBuilder.Entity<MessageQueue>()
		.Property(b => b.Priority)
		.IsRequired()
		.HasDefaultValueSql("1");


		var converterMessageQueueStatus = new ValueConverter<MessageStatus, string>(
		v => v.ToString(),
		v => (MessageStatus)Enum.Parse(typeof(MessageStatus), v));

		modelBuilder.Entity<MessageQueue>()
		.Property(b => b.Status)
		.HasConversion(converterMessageQueueStatus);

		modelBuilder.Entity<OtpMessageQueue>()
		.Property(b => b.Priority)
		.IsRequired()
		.HasDefaultValueSql("1");


		var converterOtpMessageQueueStatus = new ValueConverter<MessageStatus, string>(
		v => v.ToString(),
		v => (MessageStatus)Enum.Parse(typeof(MessageStatus), v));

		modelBuilder.Entity<OtpMessageQueue>()
		.Property(b => b.Status)
		.HasConversion(converterOtpMessageQueueStatus);

		modelBuilder.Entity<Sender>()
		.Property(b => b.IsActive)
		.IsRequired()
		.HasDefaultValue(true);

		modelBuilder.Entity<Sender>()
		.Property(b => b.Priority)
		.IsRequired()
		.HasDefaultValueSql("1");

		modelBuilder.Entity<Sender>()
		.Property(b => b.DailyLimit)
		.IsRequired()
		.HasDefaultValueSql("1000");


		var converterMessageTemplateType = new ValueConverter<TemplateTypes, string>(
		v => v.ToString(),
		v => (TemplateTypes)Enum.Parse(typeof(TemplateTypes), v));

		modelBuilder.Entity<MessageTemplate>()
		.Property(b => b.Type)
		.HasConversion(converterMessageTemplateType);


		var converterBlackListReasonType = new ValueConverter<BlacklistReasonType, string>(
		v => v.ToString(),
		v => (BlacklistReasonType)Enum.Parse(typeof(BlacklistReasonType), v));

		modelBuilder.Entity<BlackList>()
		.Property(b => b.ReasonType)
		.HasConversion(converterBlackListReasonType);

		modelBuilder.Entity<AuditLog>()
		.Property(b => b.IsChanged)
		.IsRequired()
		.HasDefaultValue(true);

		modelBuilder.Entity<AuditLog>()
		.Property(b => b.IsSuccess)
		.IsRequired()
		.HasDefaultValue(true);

		modelBuilder.Entity<Message>().Property(b => b.IsDeleted).IsRequired().HasDefaultValue(false);
		modelBuilder.Entity<Message>().HasQueryFilter(r => !r.IsDeleted);

		modelBuilder.Entity<Campaign>().Property(b => b.IsDeleted).IsRequired().HasDefaultValue(false);
		modelBuilder.Entity<Campaign>().HasQueryFilter(r => !r.IsDeleted);

		modelBuilder.Entity<MessageLog>().Property(b => b.IsDeleted).IsRequired().HasDefaultValue(false);
		modelBuilder.Entity<MessageLog>().HasQueryFilter(r => !r.IsDeleted);

		modelBuilder.Entity<OtpMessageLog>().Property(b => b.IsDeleted).IsRequired().HasDefaultValue(false);
		modelBuilder.Entity<OtpMessageLog>().HasQueryFilter(r => !r.IsDeleted);

		modelBuilder.Entity<OtpMessage>().Property(b => b.IsDeleted).IsRequired().HasDefaultValue(false);
		modelBuilder.Entity<OtpMessage>().HasQueryFilter(r => !r.IsDeleted);

		modelBuilder.Entity<MessageQueue>().Property(b => b.IsDeleted).IsRequired().HasDefaultValue(false);
		modelBuilder.Entity<MessageQueue>().HasQueryFilter(r => !r.IsDeleted);

		modelBuilder.Entity<OtpMessageQueue>().Property(b => b.IsDeleted).IsRequired().HasDefaultValue(false);
		modelBuilder.Entity<OtpMessageQueue>().HasQueryFilter(r => !r.IsDeleted);

		modelBuilder.Entity<Sender>().Property(b => b.IsDeleted).IsRequired().HasDefaultValue(false);
		modelBuilder.Entity<Sender>().HasQueryFilter(r => !r.IsDeleted);

		modelBuilder.Entity<BlackList>().Property(b => b.IsDeleted).IsRequired().HasDefaultValue(false);
		modelBuilder.Entity<BlackList>().HasQueryFilter(r => !r.IsDeleted);


		modelBuilder.Entity<Message>().HasIndex(r => r.IsDeleted).HasFilter("IsDeleted = 0");
		modelBuilder.Entity<Campaign>().HasIndex(r => r.IsDeleted).HasFilter("IsDeleted = 0");
		modelBuilder.Entity<MessageLog>().HasIndex(r => r.IsDeleted).HasFilter("IsDeleted = 0");
		modelBuilder.Entity<OtpMessageLog>().HasIndex(r => r.IsDeleted).HasFilter("IsDeleted = 0");
		modelBuilder.Entity<OtpMessage>().HasIndex(r => r.IsDeleted).HasFilter("IsDeleted = 0");
		modelBuilder.Entity<MessageQueue>().HasIndex(r => r.IsDeleted).HasFilter("IsDeleted = 0");
		modelBuilder.Entity<OtpMessageQueue>().HasIndex(r => r.IsDeleted).HasFilter("IsDeleted = 0");
		modelBuilder.Entity<Sender>().HasIndex(r => r.IsDeleted).HasFilter("IsDeleted = 0");
		modelBuilder.Entity<BlackList>().HasIndex(r => r.IsDeleted).HasFilter("IsDeleted = 0");


		modelBuilder.Entity<Message>()
			.HasOne(e => e.Campaign)
			.WithMany(p => p.Messages)
			.HasForeignKey(e => e.CampaignId)
			.OnDelete(DeleteBehavior.Restrict);

		modelBuilder.Entity<Message>()
			.HasOne(e => e.Template)
			.WithMany(p => p.Messages)
			.HasForeignKey(e => e.TemplateName)
			.OnDelete(DeleteBehavior.Restrict);

		modelBuilder.Entity<Campaign>()
			.HasOne(e => e.Template)
			.WithMany(p => p.Campaigns)
			.HasForeignKey(e => e.TemplateName)
			.OnDelete(DeleteBehavior.Restrict);

		modelBuilder.Entity<MessageLog>()
			.HasOne(e => e.Template)
			.WithMany(p => p.MessageLogs)
			.HasForeignKey(e => e.TemplateName)
			.OnDelete(DeleteBehavior.Restrict);

		modelBuilder.Entity<MessageLog>()
			.HasOne(e => e.Sender)
			.WithMany(p => p.MessageLogs)
			.HasForeignKey(e => e.SenderId)
			.OnDelete(DeleteBehavior.Restrict);

		modelBuilder.Entity<MessageLog>()
			.HasOne(e => e.Message)
			.WithMany(p => p.MessageLogs)
			.HasForeignKey(e => e.MessageId)
			.OnDelete(DeleteBehavior.Restrict);

		modelBuilder.Entity<OtpMessageLog>()
			.HasOne(e => e.Template)
			.WithMany(p => p.OtpMessageLogs)
			.HasForeignKey(e => e.TemplateName)
			.OnDelete(DeleteBehavior.Restrict);

		modelBuilder.Entity<OtpMessageLog>()
			.HasOne(e => e.Sender)
			.WithMany(p => p.OtpMessageLogs)
			.HasForeignKey(e => e.SenderId)
			.OnDelete(DeleteBehavior.Restrict);

		modelBuilder.Entity<OtpMessageLog>()
			.HasOne(e => e.OtpMessage)
			.WithMany(p => p.OtpMessageLogs)
			.HasForeignKey(e => e.OtpMessageId)
			.OnDelete(DeleteBehavior.Restrict);

		modelBuilder.Entity<OtpMessage>()
			.HasOne(e => e.Template)
			.WithMany(p => p.OtpMessages)
			.HasForeignKey(e => e.TemplateName)
			.OnDelete(DeleteBehavior.Restrict);

		modelBuilder.Entity<MessageQueue>()
			.HasOne(e => e.Campaign)
			.WithMany(p => p.MessageQueues)
			.HasForeignKey(e => e.CampaignId)
			.OnDelete(DeleteBehavior.Restrict);

		modelBuilder.Entity<MessageQueue>()
			.HasOne(e => e.Message)
			.WithMany(p => p.MessageQueues)
			.HasForeignKey(e => e.MessageId)
			.OnDelete(DeleteBehavior.Restrict);

		modelBuilder.Entity<MessageQueue>()
			.HasOne(e => e.Sender)
			.WithMany(p => p.MessageQueues)
			.HasForeignKey(e => e.SenderId)
			.OnDelete(DeleteBehavior.Restrict);

		modelBuilder.Entity<OtpMessageQueue>()
			.HasOne(e => e.OtpMessage)
			.WithMany(p => p.OtpMessageQueues)
			.HasForeignKey(e => e.OtpMessageId)
			.OnDelete(DeleteBehavior.Restrict);

		modelBuilder.Entity<OtpMessageQueue>()
			.HasOne(e => e.Sender)
			.WithMany(p => p.OtpMessageQueues)
			.HasForeignKey(e => e.SenderId)
			.OnDelete(DeleteBehavior.Restrict);

		OnModelCreatingPartial(modelBuilder);
	}

	partial void OnModelCreatingPartial(ModelBuilder modelBuilder);


    public override int SaveChanges()
    {
        throw new InvalidOperationException("ReadDbContext is read-only.");
    }

    public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        throw new InvalidOperationException("ReadDbContext is read-only.");
    }

}
