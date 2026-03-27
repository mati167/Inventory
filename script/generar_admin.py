import bcrypt

print("=== GENERADOR DE USUARIO ADMIN (PYTHON) ===")
username = input("Username: ")
password = input("Password: ")

# 1. Convertir la contraseña a bytes (necesario en Python)
password_bytes = password.encode('utf-8')

# 2. Generar el salt y el hash
# gensalt() por defecto usa un costo de 12, similar al de C#
salt = bcrypt.gensalt()
hashed_password = bcrypt.hashpw(password_bytes, salt)

# 3. Convertir el hash de bytes a string para el SQL
hash_str = hashed_password.decode('utf-8')

print("\n--- COPIA Y PEGA ESTO EN TU DB ---")
print(f"INSERT INTO administrator (username, password) VALUES ('{username}', '{hash_str}');")
print("----------------------------------\n")