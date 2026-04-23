from symb_string import SymbString
from hex_string import HexString

class Factory:
    
    def __init__(self):
        self.objects = {} # Dictionary to store objects by their ID

    def create_object(self, obj_type, obj_id, state):
        if obj_id in self.objects:
            print("error: object with this id already exists.")
            return

        if obj_type == "1":
            new_obj = SymbString(obj_id, state)
            self.objects[obj_id] = new_obj
            print("object created successfully.")
        elif obj_type == "2":
            new_obj = HexString(obj_id, state)
            self.objects[obj_id] = new_obj
            print("object created successfully.")
        else:
            print("error: unknown type.")

    def delete_object(self, obj_id):
        if obj_id in self.objects:
            del self.objects[obj_id]
            print("object deleted successfully.")
        else:
            print("error: object not found.")

    def get_object(self, obj_id):
        if obj_id in self.objects:
            return self.objects[obj_id]
        return None

    def show_all(self):
        if len(self.objects) == 0:
            print("no objects in memory.")
        else:
            for obj in self.objects.values():
                obj.show_value()