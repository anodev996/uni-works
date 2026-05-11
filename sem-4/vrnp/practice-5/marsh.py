"""Doc."""


class Marsh:
    """Doc."""

    def __init__(
        self, start_point: str, end_point: str, route_number: int
    ) -> None:
        """Doc."""

        self._start_point = start_point
        self._end_point = end_point
        self._route_number = route_number

    @property
    def start_point(self) -> str:
        """Doc."""
        return self._start_point

    @property
    def end_point(self) -> str:
        """Doc."""
        return self._end_point

    @property
    def route_number(self) -> int:
        """Doc."""
        return self._route_number

    def __str__(self) -> str:
        """Doc."""
        return f"route {self._route_number} goes from {self._start_point} to {self._end_point}."

    def __lt__(self, other: "Marsh") -> bool:
        """
        Overloaded less-than operator for sorting.
        Allows sorting a list of Marsh objects by route_number.
        """
        return self._route_number < other.route_number
