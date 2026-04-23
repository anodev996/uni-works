#!/usr/bin/env python3
from factory import Factory

def main():
    factory = Factory()

    print("example test data:\n" +
          "1 - s1 - hello world\n" +
          "1 - s2 - world\n" +
          "2 - h1 - 1A\n" +
          "2 - h2 - B")

    while True:
        print("\n--- menu ---")
        print("1 create object")
        print("2 delete object")
        print("3 show specific object")
        print("4 show all objects")
        print("5 subtract objects (s1 - s2)")
        print("0 exit")
        
        choice = input("\nyour choise: ")
        print()

        if choice == "1":
            print("types: 1 - symbstring, 2 - hexstring")
            obj_type = input("enter type (1 or 2): ")
            obj_id = input("enter new id: ")
            state = input("enter state (value): ")
            factory.create_object(obj_type, obj_id, state)

        elif choice == "2":
            obj_id = input("enter id to delete: ")
            factory.delete_object(obj_id)

        elif choice == "3":
            obj_id = input("enter id to show: ")
            obj = factory.get_object(obj_id)
            if obj:
                obj.show_value()
            else:
                print("error: object not found.")

        elif choice == "4":
            factory.show_all()

        elif choice == "5":
            id1 = input("enter id of first object (s1): ")
            id2 = input("enter id of second object (s2): ")
            
            obj1 = factory.get_object(id1)
            obj2 = factory.get_object(id2)
            
            if obj1 and obj2:
                # This triggers the __sub__ method (operator -)
                result_obj = obj1 - obj2
                print("result of subtraction:")
                result_obj.show_value()
            else:
                print("error: one or both objects not found.")

        elif choice == "0":
            print("exiting program...")
            break

        else:
            print("invalid choice. try again.")

if __name__ == "__main__":
    main()
