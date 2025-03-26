public class PatientVitalsService
{
    public void RecordVitals(
        PatientDetails patientDetails, GeneralVitals generalVitals)
    {
        generalVitals.ValidateTemperature();
        generalVitals.ValidateBloodPressure();
        generalVitals.ValidateHeartRate(patientDetails.age);
        generalVitals.ValidateBloodType(patientDetails.bloodType);
        generalVitals.TriggerAlert();
    }
}

public class PatientDetails 
{
    public int patientId { get; set; }
    public int age { get; set; }
    public BloodType bloodType{ get; set; }
}

public class GeneralVitals
{
    public DateTime lastMealTime { get; set; }
    public double temperature { get; set; }
    public int heartRate { get; set; }
    public BloodPressure bloodPressure { get; set; }

    public void ValidateTemperature(double temperature)
    {
        if (temperature < 34 || temperature > 42)
            throw new ArgumentException("Invalid temperature");
    }

    public void ValidateHeartRate(int age) 
    {
        int maxHeartRate = 220 - age;
        if (heartRate < 40 || heartRate > maxHeartRate * 1.2)
            throw new ArgumentException($"Heart rate invalid for age {age}");
    }
    public void TriggerAlert() 
    {
            if ((DateTime.Now - lastMealTime).TotalHours < 2 && bloodPressure.diastolic > 90)
            TriggerAlert("Elevated postprandial blood pressure");
    }
    public void ValidateBloodPressure() {
        if (bloodPressure.systolic < 70 || bloodPressure.systolic > 200)
            throw new ArgumentException("Invalid systolic BP");
        if (bloodPressure.diastolic < 40 || bloodPressure.diastolic > 120)
            throw new ArgumentException("Invalid diastolic BP");
        if (bloodPressure.diastolic > bloodPressure.systolic)
            throw new ArgumentException("Diastolic cannot exceed systolic");
    }

    public void ValidateBloodType(BloodType bloodType) {
        if (!new[] {"A+", "A-", "B+", "B-", "AB+", "AB-", "O+", "O-"}.Contains(bloodType))
            throw new ArgumentException("Invalid blood type");
    }
}
    

public class BloodPressure
{
    public int systolic { get; set; }
    public int diastolic { get; set; }

}

public class BloodType 
{
    
}