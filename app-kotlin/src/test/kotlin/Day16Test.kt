package day16

import org.junit.jupiter.api.Assertions.assertEquals
import org.junit.jupiter.api.Test

import day16.Day16.Valve

internal class Day16Test {
    val testInput = listOf(
        "Valve AA has flow rate=0; tunnels lead to valves DD, II, BB",
        "Valve BB has flow rate=13; tunnels lead to valves CC, AA",
        "Valve CC has flow rate=2; tunnels lead to valves DD, BB",
        "Valve DD has flow rate=20; tunnels lead to valves CC, AA, EE",
        "Valve EE has flow rate=3; tunnels lead to valves FF, DD",
        "Valve FF has flow rate=0; tunnels lead to valves EE, GG",
        "Valve GG has flow rate=0; tunnels lead to valves FF, HH",
        "Valve HH has flow rate=22; tunnel leads to valve GG",
        "Valve II has flow rate=0; tunnels lead to valves AA, JJ",
        "Valve JJ has flow rate=21; tunnel leads to valve II",
    )

    @Test
    fun `valve identifier is parsed`() {
        assertEquals("AA", Valve.fromString(testInput[0]).identifier)
        assertEquals("BB", Valve.fromString(testInput[1]).identifier)
        assertEquals("CC", Valve.fromString(testInput[2]).identifier)
        assertEquals("DD", Valve.fromString(testInput[3]).identifier)
        assertEquals("EE", Valve.fromString(testInput[4]).identifier)
        assertEquals("FF", Valve.fromString(testInput[5]).identifier)
        assertEquals("GG", Valve.fromString(testInput[6]).identifier)
        assertEquals("HH", Valve.fromString(testInput[7]).identifier)
        assertEquals("II", Valve.fromString(testInput[8]).identifier)
        assertEquals("JJ", Valve.fromString(testInput[9]).identifier)
    }

    @Test
    fun `valve flow rate is parsed`() {
        assertEquals(0, Valve.fromString(testInput[0]).flowRate)
        assertEquals(13, Valve.fromString(testInput[1]).flowRate)
        assertEquals(2, Valve.fromString(testInput[2]).flowRate)
        assertEquals(20, Valve.fromString(testInput[3]).flowRate)
        assertEquals(3, Valve.fromString(testInput[4]).flowRate)
        assertEquals(0, Valve.fromString(testInput[5]).flowRate)
        assertEquals(0, Valve.fromString(testInput[6]).flowRate)
        assertEquals(22, Valve.fromString(testInput[7]).flowRate)
        assertEquals(0, Valve.fromString(testInput[8]).flowRate)
        assertEquals(21, Valve.fromString(testInput[9]).flowRate)
    }

    @Test
    fun `valve connected valves are parsed`() {
        assertEquals(listOf("DD", "II", "BB"), Valve.fromString(testInput[0]).connectedValves)
    }
}