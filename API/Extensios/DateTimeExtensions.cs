namespace API;

public static class DateTimeExtensions{

    /**
    * This method is used to calculate the age of a user
    * @param dob: The date of birth of the user
    * @return The age of the user
    */
    public static int calculateAge(this DateOnly dob){
        var today = DateOnly.FromDateTime(DateTime.Now);

        var age = today.Year - dob.Year;

        if(dob > today.AddYears(-age)) age--;

        return age;
    }
}