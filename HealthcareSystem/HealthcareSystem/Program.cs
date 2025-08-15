using System;
using System.Collections.Generic;

// Step 1: Generic Repository
public class Repository<T>
{
    private List<T> items = new();

    public void Add(T item) => items.Add(item);

    public IEnumerable<T> GetAll() => items;
}

// Step 2: Patient Class
public class Patient
{
    public int Id { get; set; }
    public string Name { get; set; }

    public Patient(int id, string name)
    {
        Id = id;
        Name = name;
    }
}

// Step 3: Prescription Class
public class Prescription
{
    public int Id { get; set; }
    public int PatientId { get; set; }
    public string Medication { get; set; }

    public Prescription(int id, int patientId, string medication)
    {
        Id = id;
        PatientId = patientId;
        Medication = medication;
    }
}

// Step 4: Healthcare App
public class HealthSystemApp
{
    public void Run()
    {
        // Create repositories
        var patientRepo = new Repository<Patient>();
        var prescriptionRepo = new Repository<Prescription>();

        // Seed patients
        patientRepo.Add(new Patient(1, "John Doe"));
        patientRepo.Add(new Patient(2, "Jane Smith"));
        patientRepo.Add(new Patient(3, "Alice Johnson"));

        // Seed prescriptions
        prescriptionRepo.Add(new Prescription(1, 1, "Amoxicillin"));
        prescriptionRepo.Add(new Prescription(2, 1, "Ibuprofen"));
        prescriptionRepo.Add(new Prescription(3, 2, "Paracetamol"));

        // Step 5: Group prescriptions by patient ID
        Dictionary<int, List<Prescription>> prescriptionMap = new();

        foreach (var prescription in prescriptionRepo.GetAll())
        {
            if (!prescriptionMap.ContainsKey(prescription.PatientId))
            {
                prescriptionMap[prescription.PatientId] = new List<Prescription>();
            }
            prescriptionMap[prescription.PatientId].Add(prescription);
        }

        // Step 6: Display data
        Console.WriteLine("\n--- Healthcare System ---");
        foreach (var patient in patientRepo.GetAll())
        {
            Console.WriteLine($"\nPatient: {patient.Name} (ID: {patient.Id})");

            if (prescriptionMap.ContainsKey(patient.Id))
            {
                Console.WriteLine("Prescriptions:");
                foreach (var p in prescriptionMap[patient.Id])
                {
                    Console.WriteLine($" - {p.Medication}");
                }
            }
            else
            {
                Console.WriteLine("No prescriptions found.");
            }
        }
    }
}

// Step 7: Main Entry (temporary for testing Q2)
class Program
{
    static void Main()
    {
        var app = new HealthSystemApp();
        app.Run();
    }
}
