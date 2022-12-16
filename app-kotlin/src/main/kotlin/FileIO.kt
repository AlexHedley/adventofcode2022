package ahedley

object FileIO {
    fun <T> readInput(filename: String, lineMapper: (String) -> T): List<T> {
        return this::class.java.getResourceAsStream(filename).bufferedReader().readLines().map(lineMapper)
    }
}