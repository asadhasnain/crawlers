import requests
from bs4 import BeautifulSoup

def crawl_website(url):
    # Send a GET request to the URL and retrieve the HTML content
    response = requests.get(url)
    
    # Create a BeautifulSoup object to parse the HTML
    soup = BeautifulSoup(response.content, 'html.parser')
    
    # Find all the study cards on the page
    study_cards = soup.find_all('div', class_='project-detail')
    
    # Iterate over each study card and extract the desired information
    for card in study_cards:
    
        # Extract the study title
        title = card.find('h3').text.strip()
        description = card.find('p').text.strip()
        dollar = card.find_all('i', class_='fa fa-dollar')
        print("Study Title:", title)
        print("Description:", description)
        print("Dollars:", len(dollar))
        
        projectSpecs = card.find_all('li')
        
        
        for spec in projectSpecs:
            if spec.text.strip() != 'Photo ID Required':
                print(spec.text.strip())
        
        linkTitle = card.find('a').text.strip()
        url = 'https://participant.facilitymanagerplus.com/'
        print("linkTitle: ", url + card.find('a').get('href'))
     
        print("")
        
        

# URL of the initial page to crawl
url = "https://participant.facilitymanagerplus.com/AvailableStudies.aspx"
crawl_website(url)
