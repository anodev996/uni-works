class SymbString:
    
    def __init__(self, obj_id, state):
        self.id = obj_id
        self.state = str(state)

    def show_value(self):
        print(f"id: {self.id}, type: symbstring, value: {self.state}")

    def __sub__(self, other):
        # If s2 (other) is a substring of s1 (self), remove it
        if other.state in self.state:
            new_state = self.state.replace(other.state, "")
            return SymbString("result", new_state)
        else:
            # Otherwise return s1
            return SymbString("result", self.state)