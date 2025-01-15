// Job
using System;
using System.Collections.Generic;
public class Job
{
    
    private string _company;
    private string _jobTitle;
    private int _startYear;
    private int _endYear;

    // Constructor
    public Job(string company, string jobTitle, int startYear, int endYear)
    {
        _company = company;
        _jobTitle = jobTitle;
        _startYear = startYear;
        _endYear = endYear;
    }

    // Display method to show the job information
    public void DisplayJobDetails()
    {
        Console.WriteLine($"{_jobTitle} ({_company}) {_startYear}-{_endYear}");
    }
}
// Resume


public class Resume
{
    
    private string _name;
    private List<Job> _jobs;

    // Constructor
    public Resume(string name)
    {
        _name = name;
        _jobs = new List<Job>();
    }

    // Method to add a job to the resume
    public void AddJob(Job job)
    {
        _jobs.Add(job);
    }

    // Method to display resume details (name + jobs)
    public void DisplayResumeDetails()
    {
        Console.WriteLine($"Name: {_name}");
        foreach (Job job in _jobs)
        {
            job.DisplayJobDetails(); // Call DisplayJobDetails for each job in the list
        }
    }
}
// Program


class Program
{
    static void Main()
    {
        // Create job instances
        Job job1 = new Job("Microsoft", "Software Engineer", 2019, 2022);
        Job job2 = new Job("Google", "Product Manager", 2022, 2025);

        // Create a resume instance
        Resume myResume = new Resume("Zacarías Flores del Campo");

        // Add jobs to the resume
        myResume.AddJob(job1);
        myResume.AddJob(job2);

        // Display the resume
        myResume.DisplayResumeDetails();
    }
}
