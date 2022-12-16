package day16

object Day16 {
    class Valve(val identifier: String, val flowRate: Int, val connectedValves: List<String>) {
        companion object {
            private val lineRegex = """Valve ([A-Z]{2}) has flow rate=(\d+); tunnels? leads? to valves? (.*)""".toRegex()
            fun fromString(input: String): Valve {
                val (id, flowRate, connectedValves) = lineRegex.find(input)!!.destructured
                return Valve(id, flowRate.toInt(), connectedValves.split(", "))
            }
        }
    }
}