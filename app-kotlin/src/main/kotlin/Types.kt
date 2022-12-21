package ahedley

import kotlin.math.abs
import kotlin.math.sign

data class Vector2d(val x: Int, val y: Int) {
    fun abs(other: Vector2d) = if ( this.x == other.x ) abs(this.y - other.y) else abs(this.x - other.x)

    operator fun plus(other: Vector2d) = this.copy(x = x + other.x, y = y + other.y)
    operator fun minus(other: Vector2d) = this.copy(x = x - other.x, y = y - other.y)
    companion object {
        val origin = Vector2d(0, 0)

        fun fromFile(coord: String): Vector2d {
            val csv = coord.split(',').map { it.toInt() }
            return Vector2d(csv[0], csv[1])
        }
    }
}

val Vector2d.sign: Vector2d
    get() {
        return Vector2d(this.x.sign, this.y.sign)
    }