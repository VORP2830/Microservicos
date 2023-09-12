import requests
import time

api_url = "http://localhost:5245/api/Queue/enviarmensagem"

for numero in range(1, 11):
    try:
        response = requests.post(api_url, json=f'O numero enviado foi: {numero}')
        if response.status_code == 200:
            print(f"Mensagem enviada com sucesso: {numero}")
        else:
            print(f"Falha ao enviar mensagem: {numero}. Status code: {response.status_code}")
    except Exception as e:
        print(f"Erro ao enviar mensagem: {numero}. Erro: {e}")

    time.sleep(1) 
