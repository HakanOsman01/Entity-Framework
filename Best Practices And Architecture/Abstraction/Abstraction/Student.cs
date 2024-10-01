namespace Abstraction
{
    public class Student
    {
        public Student(string name,int grade)
        {
            this.Name = name;
            this.Grade = grade;
        }
        public Student() 
        {
            this.Name = "empty";
            this.Grade = 0;
        }
      

        public string Name { get; set; }
        public double Grade { get; set; }
       
       

    }
}
