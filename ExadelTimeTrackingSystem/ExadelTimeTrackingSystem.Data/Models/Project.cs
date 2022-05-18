﻿// This file is used by Code Analysis to maintain SuppressMessage
// attributes that are applied to this project.
// Project-level suppressions either have no target or are given
// a specific target and scoped to a namespace, type, member, etc.
namespace ExadelTimeTrackingSystem.Data.Models
{
    using System;
    using ExadelTimeTrackingSystem.Data.Models.Abstract;

    public class Project : IDocument
    {
        public Guid Id { get; set; }

        public string[] Activities { get; set; }

        public string Name { get; set; }

        public Guid ApproverId { get; set; }

        public Guid[] EmployeeIds { get; set; }
    }
}