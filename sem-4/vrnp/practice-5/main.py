#!/usr/bin/env python3
"""Doc."""

from marsh import Marsh

MAX_ROUTES: int = 8


def print_menu() -> None:
    """Print the interactive menu options to the console."""
    print(
        "\n"
        + "-- menu --\n"
        + "1 add route manually\n"
        + "2 add predefined routes (105, 42, 88)\n"
        + "3 find route by its number\n"
        + "4 print routes array data\n"
        + "0 exit\n"
    )


def add_route_manually(routes: list[Marsh]) -> None:
    """Doc."""

    if len(routes) >= MAX_ROUTES:
        print("array is full so cannot add route.")
        return

    try:
        route_num: int = int(input("enter route number: "))
        start_p: str = input("enter start point: ")
        end_p: str = input("enter end point: ")

        new_route = Marsh(
            start_point=start_p, end_point=end_p, route_number=route_num
        )
        routes.append(new_route)
        routes.sort()

        print("route added.")

    except ValueError:
        print("route number must be an integer.")


def load_hardcoded_routes(routes: list[Marsh]) -> None:
    """Doc."""

    hardcoded_data = [
        Marsh(start_point="Moscow", end_point="Kazan", route_number=105),
        Marsh(start_point="Novosibirsk", end_point="Omsk", route_number=42),
        Marsh(start_point="SPb", end_point="Murmansk", route_number=88),
    ]

    available_space: int = MAX_ROUTES - len(routes)

    if available_space <= 0:
        print("cannot load routes, array is full.")
        return

    routes_to_add = hardcoded_data[:available_space]
    routes.extend(routes_to_add)
    routes.sort()


def find_and_print_route(routes: list[Marsh]) -> None:
    """Doc."""

    try:
        target_num: int = int(input("enter route number to find: "))
        found: bool = False

        for route in routes:
            if route.route_number == target_num:
                print(route)
                found = True
                break

        if not found:
            print("no such route found.")

    except ValueError:
        print("route number must be an integer.")


def print_all_routes(routes: list[Marsh]) -> None:
    """Doc."""
    if not routes:
        print("array is empty.")
        return

    for route in routes:
        print(route)


def main() -> None:
    """Doc."""
    routes: list[Marsh] = []

    while True:
        print_menu()
        choice: str = input("select option: ").strip()

        if choice == "1":
            add_route_manually(routes)
        elif choice == "2":
            load_hardcoded_routes(routes)
        elif choice == "3":
            find_and_print_route(routes)
        elif choice == "4":
            print_all_routes(routes)
        elif choice == "0":
            break
        else:
            print("invalid input.")


if __name__ == "__main__":
    main()
