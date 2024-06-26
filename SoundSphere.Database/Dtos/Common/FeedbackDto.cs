﻿using SoundSphere.Database.Entities;
using System.ComponentModel.DataAnnotations;

namespace SoundSphere.Database.Dtos.Common
{
    public class FeedbackDto : BaseEntity
    {
        [Required(ErrorMessage = "Id is required")]
        public Guid Id { get; set; }

        [Required(ErrorMessage = "UserId is required")]
        public Guid UserId { get; set; }

        [Required(ErrorMessage = "Type is required")]
        public FeedbackType Type { get; set; }

        [Required(ErrorMessage = "Message is required")]
        [StringLength(500, ErrorMessage = "Message can't be longer than 500 characters")]
        public string Message { get; set; } = null!;

        public override bool Equals(object? obj) => obj is FeedbackDto feedbackDto &&
            Id.Equals(feedbackDto.Id) &&
            UserId.Equals(feedbackDto.UserId) &&
            Type == feedbackDto.Type &&
            Message.Equals(feedbackDto.Message) &&
            CreatedAt.Equals(feedbackDto.CreatedAt) &&
            UpdatedAt.Equals(feedbackDto.UpdatedAt) &&
            DeletedAt.Equals(feedbackDto.DeletedAt);

        public override int GetHashCode() => HashCode.Combine(Id, UserId, Type, Message, HashCode.Combine(CreatedAt, UpdatedAt, DeletedAt));
    }
}