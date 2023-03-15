import requests

api_url = 'http://localhost:5054/api/docker/containers'
headers = {'Content-Type': 'application/json', 'Authorization': 'Bearer <access_token>'}

response = requests.get(api_url, headers=headers)
print(response)

if response.status_code == 200:
    # Request successful
    data = response.json()
    print(data)
else:
    # Request failed
    print(f'Error: {response.status_code} - {response.text}')
