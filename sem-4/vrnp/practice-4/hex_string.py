from symb_string import SymbString

class HexString(SymbString):
    
    def __init__(self, obj_id, state):
        # Call the constructor of the parent class
        super().__init__(obj_id, state)

    def show_value(self):
        print(f"id: {self.id}, type: hexstring, value: {self.state}")

    def __sub__(self, other):
        try:
            # Convert both states from hex strings to integers
            val1 = int(self.state, 16)
            val2 = int(other.state, 16)
            
            # Subtract numeric values
            result_val = val1 - val2
            
            # Convert back to hex string (python's hex() adds '0x' prefix)
            new_state = hex(result_val)
            return HexString("result", new_state)
            
        except ValueError:
            print("error: one of the objects has an invalid hex value.")
            return HexString("result", self.state)