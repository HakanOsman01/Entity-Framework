namespace Demo
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string[] words = new string[] { "the", "quick", "brown", "fox" };
            Predicate<string> pred = (string w) => w.Length == 3;
            IEnumerable<string> filterWords = words.Where(w => pred(w));
            foreach (string word in filterWords)
            {
                Console.WriteLine(word);
            }
            IEnumerable<string> quiry = from word in words
                                        where word.Length == 3
                                        select word;
            foreach (string word in quiry)
            {

                Console.WriteLine(word.Normalize());


            }
        }
    }
}