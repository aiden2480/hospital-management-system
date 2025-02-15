﻿using HospitalManagementSystem.Entity;
using Spectre.Console;

namespace HospitalManagementSystem.Extension;

static internal class PluralExtensions
{
    public static Table ToTable(this IEnumerable<AbstractUserWithAppointments> users)
    {
        if (!users.Any())
        {
            return new Table().AddColumn("There are no users");
        }

        var table = new Table()
            .AddColumn("Full Name")
            .AddColumn("Email")
            .AddColumn("Phone")
            .AddColumn("Address");

        foreach (var user in users)
        {
            table.AddRow(user.FullName, user.Email, user.PhoneNumber, user.FullAddr);
        }

        return table;
    }

    public static Table ToTable(this IEnumerable<Appointment> appointments)
    {
        if (!appointments.Any())
        {
            return new Table().AddColumn("There are no appointments");
        }

        var table = new Table()
            .AddColumn("Appointment ID")
            .AddColumn("Doctor")
            .AddColumn("Patient")
            .AddColumn("Time")
            .AddColumn("Description");

        foreach (var a in appointments)
        {
            table.AddRow(a.Id.ToString(), a.Doctor.FullName, a.Patient.FullName, a.ScheduledTime.ToString(), a.Description);
        }

        return table;
    }
}
