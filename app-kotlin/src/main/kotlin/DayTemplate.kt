package ahedley

object DayTemplate {
    fun partOne(input: List<String>) {
    }

    fun partTwo(input: List<String>) {
    }

    private val input = FileIO.readInput("day##input.txt") { s -> s }

    @JvmStatic
    fun main(args: Array<String>) {
        val partOneSolution = partOne(input)
        println(partOneSolution)

        val partTwoSolution = partTwo(input)
        println(partTwoSolution)
    }
}