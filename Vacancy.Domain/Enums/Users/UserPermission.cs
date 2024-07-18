namespace Vacancy.Domain.Enums.Users;

public enum UserPermission
{
    #region auth
    ViewPermissions = 1001,
    ViewPermission = 1002,
    UpdatePermission = 1003,
    ViewRoles = 1010,
    ViewRole = 1011,
    DeleteRole = 1012,
    AddRole = 1013,
    UpdateRole = 1014,
    AddingPermissionsIntoRole = 1015,
    RemovingPermissionFromRole = 1016,
    #endregion

    #region User
    ViewUsers = 1200,
    ViewUser = 1201,
    UpdateUser = 1203,
    UpdateUserRole = 1204,
    DeleteUser = 1205,
    ApproveUser = 1206,
    #endregion

    #region Vacancy
    ViewVacancies = 1300,
    ViewVacancy = 1301,
    UpdateVacancy = 1302,
    DeleteVacancy = 1303,
    CreateVacancy = 1304,
    #endregion

    #region Application
    ViewApplications = 1400,
    ViewApplication = 1401,
    UpdateApplication = 1402,
    DeleteApplication = 1403,
    ChangeApplicationStatus = 1404,
    CreateApplication = 1405,
    DeleteApplications = 1406
    #endregion
}
