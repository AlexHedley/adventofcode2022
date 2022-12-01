public static class Utils {
    public static string[] GetLines(string fileName) {
        return System.IO.File.ReadAllLines(fileName);
    }
}