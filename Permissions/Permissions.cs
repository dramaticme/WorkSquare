namespace worksquare.Permissions
{
    /// <summary>
    /// All permission keys in the system, organised by module.
    /// Format: {Module}.{Action}[.{Scope}]
    /// Scope: Own | Team | Company
    /// </summary>
    public static class Permissions
    {
        // ── Dashboard ─────────────────────────────────────────────────────
        public static class Dashboard
        {
            public const string View                       = "Dashboard.View";
            public const string ViewStatsOwn               = "Dashboard.ViewStats.Own";
            public const string ViewStatsTeam              = "Dashboard.ViewStats.Team";
            public const string ViewStatsCompany           = "Dashboard.ViewStats.Company";
            public const string ViewHappinessTrackerOwn    = "Dashboard.ViewHappinessTracker.Own";
            public const string ViewLearningTrackerOwn     = "Dashboard.ViewLearningTracker.Own";
            public const string ViewPerformanceTrackerOwn  = "Dashboard.ViewPerformanceTracker.Own";
            public const string ViewTargetsPreview         = "Dashboard.ViewTargetsPreview";
            public const string ViewTeamHappiness          = "Dashboard.ViewTeamHappiness";
            public const string ViewTeamPerformance        = "Dashboard.ViewTeamPerformance";
            public const string ViewCompanyMetrics         = "Dashboard.ViewCompanyMetrics";
        }

        // ── Targets (Kanban) ──────────────────────────────────────────────
        public static class Targets
        {
            public const string ViewOwn              = "Targets.View.Own";
            public const string ViewTeam             = "Targets.View.Team";
            public const string ViewCompany          = "Targets.View.Company";
            public const string MoveCardOwn          = "Targets.MoveCard.Own";
            public const string MoveCardTeam         = "Targets.MoveCard.Team";
            public const string MoveCardCompany      = "Targets.MoveCard.Company";
            public const string FilterByProject      = "Targets.FilterByProject";
            public const string FilterByEmployee     = "Targets.FilterByEmployee";
        }

        // ── Projects ─────────────────────────────────────────────────────
        public static class Projects
        {
            public const string ViewOwn              = "Projects.View.Own";
            public const string ViewCompany          = "Projects.View.Company";
            public const string Create               = "Projects.Create";
            public const string EditOwn              = "Projects.Edit.Own";
            public const string EditCompany          = "Projects.Edit.Company";
            public const string Delete               = "Projects.Delete";
            public const string Archive              = "Projects.Archive";
            public const string Restore              = "Projects.Restore";
            public const string AssignEmployee       = "Projects.AssignEmployee";
            public const string ChangeManager        = "Projects.ChangeManager";
            public const string ViewAnalytics        = "Projects.ViewAnalytics";
            public const string ViewClientInfo       = "Projects.ViewClientInfo";
        }

        // ── Tasks ─────────────────────────────────────────────────────────
        public static class Tasks
        {
            public const string ViewOwn              = "Tasks.View.Own";
            public const string ViewTeam             = "Tasks.View.Team";
            public const string ViewCompany          = "Tasks.View.Company";
            public const string Create               = "Tasks.Create";
            public const string EditOwn              = "Tasks.Edit.Own";
            public const string EditTeam             = "Tasks.Edit.Team";
            public const string EditCompany          = "Tasks.Edit.Company";
            public const string UpdateStatusOwn      = "Tasks.UpdateStatus.Own";
            public const string UpdateStatusTeam     = "Tasks.UpdateStatus.Team";
            public const string Assign               = "Tasks.Assign";
            public const string DeleteOwn            = "Tasks.Delete.Own";
            public const string DeleteCompany        = "Tasks.Delete.Company";
            public const string Bookmark             = "Tasks.Bookmark";
            public const string SetPriority          = "Tasks.SetPriority";
            public const string SetDueDate           = "Tasks.SetDueDate";
        }

        // ── Issues ────────────────────────────────────────────────────────
        public static class Issues
        {
            public const string ViewOwn              = "Issues.View.Own";
            public const string ViewTeam             = "Issues.View.Team";
            public const string ViewCompany          = "Issues.View.Company";
            public const string Create               = "Issues.Create";
            public const string EditOwn              = "Issues.Edit.Own";
            public const string EditCompany          = "Issues.Edit.Company";
            public const string UpdateStatusOwn      = "Issues.UpdateStatus.Own";
            public const string UpdateStatusTeam     = "Issues.UpdateStatus.Team";
            public const string Assign               = "Issues.Assign";
            public const string DeleteOwn            = "Issues.Delete.Own";
            public const string DeleteCompany        = "Issues.Delete.Company";
            public const string Bookmark             = "Issues.Bookmark";
            public const string SetPriority          = "Issues.SetPriority";
        }

        // ── Tickets ───────────────────────────────────────────────────────
        public static class Tickets
        {
            public const string ViewOwn              = "Tickets.View.Own";
            public const string ViewTeam             = "Tickets.View.Team";
            public const string ViewCompany          = "Tickets.View.Company";
            public const string Create               = "Tickets.Create";
            public const string EditOwn              = "Tickets.Edit.Own";
            public const string EditCompany          = "Tickets.Edit.Company";
            public const string UpdateStatusOwn      = "Tickets.UpdateStatus.Own";
            public const string UpdateStatusTeam     = "Tickets.UpdateStatus.Team";
            public const string Assign               = "Tickets.Assign";
            public const string DeleteOwn            = "Tickets.Delete.Own";
            public const string DeleteCompany        = "Tickets.Delete.Company";
            public const string Bookmark             = "Tickets.Bookmark";
        }

        // ── Employees ─────────────────────────────────────────────────────
        public static class Employees
        {
            public const string ViewOwn                   = "Employees.View.Own";
            public const string ViewTeam                  = "Employees.View.Team";
            public const string ViewCompany               = "Employees.View.Company";
            public const string ViewSensitive             = "Employees.ViewSensitive";
            public const string Create                    = "Employees.Create";
            public const string EditOwn                   = "Employees.Edit.Own";
            public const string EditTeam                  = "Employees.Edit.Team";
            public const string EditCompany               = "Employees.Edit.Company";
            public const string Activate                  = "Employees.Activate";
            public const string Delete                    = "Employees.Delete";
            public const string AssignRole                = "Employees.AssignRole";
            public const string SetManager                = "Employees.SetManager";
            public const string ViewAttendanceOwn         = "Employees.ViewAttendance.Own";
            public const string ViewAttendanceTeam        = "Employees.ViewAttendance.Team";
            public const string ViewAttendanceCompany     = "Employees.ViewAttendance.Company";
        }

        // ── Clients ───────────────────────────────────────────────────────
        public static class Clients
        {
            public const string View                = "Clients.View";
            public const string ViewDetails         = "Clients.ViewDetails";
            public const string Create              = "Clients.Create";
            public const string Edit                = "Clients.Edit";
            public const string Delete              = "Clients.Delete";
            public const string Archive             = "Clients.Archive";
        }

        // ── Leave ─────────────────────────────────────────────────────────
        public static class Leave
        {
            public const string ApplyOwn            = "Leave.Apply.Own";
            public const string ViewOwn             = "Leave.View.Own";
            public const string ViewTeam            = "Leave.View.Team";
            public const string ViewCompany         = "Leave.View.Company";
            public const string ApproveTeam         = "Leave.Approve.Team";
            public const string ApproveCompany      = "Leave.Approve.Company";
            public const string CancelOwn           = "Leave.Cancel.Own";
            public const string CancelTeam          = "Leave.Cancel.Team";
            public const string ConfigurePolicy     = "Leave.ConfigurePolicy";
            public const string ViewBalanceOwn      = "Leave.ViewBalance.Own";
            public const string ViewBalanceTeam     = "Leave.ViewBalance.Team";
            public const string AdjustBalance       = "Leave.AdjustBalance";
        }

        // ── Notes ─────────────────────────────────────────────────────────
        public static class Notes
        {
            public const string ViewOwn             = "Notes.View.Own";
            public const string ViewShared          = "Notes.View.Shared";
            public const string Create              = "Notes.Create";
            public const string EditOwn             = "Notes.Edit.Own";
            public const string DeleteOwn           = "Notes.Delete.Own";
            public const string ChangeColor         = "Notes.ChangeColor";
            public const string ShareTeam           = "Notes.Share.Team";
            public const string ShareCompany        = "Notes.Share.Company";
            public const string Unshare             = "Notes.Unshare";
            public const string DeleteCompany       = "Notes.Delete.Company";
        }

        // ── My Day ────────────────────────────────────────────────────────
        public static class MyDay
        {
            public const string SubmitOwn               = "MyDay.Submit.Own";
            public const string ViewOwn                 = "MyDay.View.Own";
            public const string EditOwn                 = "MyDay.Edit.Own";
            public const string ViewTeam                = "MyDay.View.Team";
            public const string ViewCompany             = "MyDay.View.Company";
            public const string ViewFeedbackTeam        = "MyDay.ViewFeedback.Team";
            public const string ViewFeedbackCompany     = "MyDay.ViewFeedback.Company";
            public const string ExportHistoryOwn        = "MyDay.ExportHistory.Own";
            public const string ExportHistoryTeam       = "MyDay.ExportHistory.Team";
            public const string ExportHistoryCompany    = "MyDay.ExportHistory.Company";
            public const string FilterByDate            = "MyDay.FilterByDate";
        }

        // ── Community (Coffee Community) ──────────────────────────────────
        public static class Community
        {
            public const string View                     = "Community.View";
            public const string Post                     = "Community.Post";
            public const string EditOwn                  = "Community.Edit.Own";
            public const string DeleteOwn                = "Community.Delete.Own";
            public const string DeleteCompany            = "Community.Delete.Company";
            public const string Reply                    = "Community.Reply";
            public const string DeleteReplyOwn           = "Community.DeleteReply.Own";
            public const string DeleteReplyCompany       = "Community.DeleteReply.Company";
            public const string Pin                      = "Community.Pin";
            public const string Unpin                    = "Community.Unpin";
            public const string Announce                 = "Community.Announce";
            public const string Mute                     = "Community.Mute";
        }

        // ── Reports ───────────────────────────────────────────────────────
        public static class Reports
        {
            public const string ViewOwn                  = "Reports.View.Own";
            public const string ViewTeam                 = "Reports.View.Team";
            public const string ViewCompany              = "Reports.View.Company";
            public const string Export                   = "Reports.Export";
            public const string ViewFinancial            = "Reports.ViewFinancial";
            public const string ViewHappinessTrend       = "Reports.ViewHappinessTrend";
        }

        // ── Users & Permission Management ─────────────────────────────────
        public static class Users
        {
            public const string View                     = "Users.View";
            public const string Create                   = "Users.Create";
            public const string Edit                     = "Users.Edit";
            public const string Deactivate               = "Users.Deactivate";
            public const string Delete                   = "Users.Delete";
            public const string AssignPermissions        = "Users.AssignPermissions";
            public const string ViewSessions             = "Users.ViewSessions";
            public const string RevokeSession            = "Users.RevokeSession";
        }

        // ── Company Settings ──────────────────────────────────────────────
        public static class Company
        {
            public const string View                     = "Company.View";
            public const string EditBasic                = "Company.EditBasic";
            public const string EditDetails              = "Company.EditDetails";
            public const string ViewBilling              = "Company.ViewBilling";
        }

        // ── Archive ───────────────────────────────────────────────────────
        public static class Archive
        {
            public const string ViewOwn                  = "Archive.View.Own";
            public const string ViewCompany              = "Archive.View.Company";
            public const string RestoreOwn               = "Archive.Restore.Own";
            public const string RestoreCompany           = "Archive.Restore.Company";
            public const string PermanentDelete          = "Archive.PermanentDelete";
        }

        // ── Terminal (System-level only) ──────────────────────────────────
        public static class Terminal
        {
            public const string Access                   = "Terminal.Access";
            public const string RunMigrations            = "Terminal.RunMigrations";
            public const string ViewLogs                 = "Terminal.ViewLogs";
        }
    }
}
