library(tidyverse)
library(readr)
library(dplyr)
library("rjson")
library(jsonlite)
library(reshape2)
library(tidytext)
library(LDAvis)
library(topicmodels)


args <- commandArgs()
books <- fromJSON(txt="boooks.json")
a = rownames(books)
books = books %>% mutate(id = as.numeric(a))
users <- fromJSON(txt="Users.json")
users = users %>% select(Login, PastBooks)
metadata_content_based = books %>% select(id, BookName, Author, Genre, PublishedYear, Rating, NumPages)

#возраст 
metadata_content_based$book_age = 2022 - metadata_content_based$PublishedYear
metadata_content_based = metadata_content_based %>% select(-PublishedYear)

#длина названия 
metadata_content_based = metadata_content_based %>% mutate(tlLength = str_length(metadata_content_based$BookName))
metadata_content_based$tlLength = ifelse(is.na(metadata_content_based$tlLength), 0, metadata_content_based$tlLength)



word_counts <- metadata_content_based %>%
  filter(!is.na(Genre)) %>%
  count(BookName, Genre, sort = TRUE) %>%
  ungroup()

review_dtm <- word_counts %>%
  cast_dtm(BookName, Genre, n)

review_lda <- LDA(review_dtm, k = 5, control = list(seed = 12345))


review_topics <- tidy(review_lda, matrix = "beta")

review_top_terms <- review_topics %>%
  group_by(topic) %>%
  top_n(10, beta) %>%
  ungroup() %>%
  arrange(topic, -beta)




review_tags <- review_topics %>% rename(Genre = term)

#Каждому жанру выбирается только одна группа с наибольшим бета
tags_best_beta <- review_tags %>% 
  group_by(Genre) %>% 
  summarise(max_beta = max(beta))

review_tags <- review_tags %>% 
  filter(beta %in% tags_best_beta$max_beta)

metadata_tags = books %>% select(BookName, Genre, Rating)
metadata_tags_topics <- metadata_tags %>% 
  left_join(review_tags) 

metadata_tags_topics$topic = replace_na(metadata_tags_topics$topic, 0)

metadata_tags_topics2 = metadata_tags_topics %>% group_by(BookName, topic) %>% summarise(n = n())
metadata_tags_topics2 = metadata_tags_topics2 %>% group_by(BookName) %>% mutate(maxn = max(n))
metadata_tags_topics2 = metadata_tags_topics2 %>% filter(maxn == n)
metadata_tags_topics2 = metadata_tags_topics2 %>% select(BookName, topic)

metadata_tags_topics2 = mutate(metadata_tags_topics2, g1 = 1)
metadata_tags_topics2 = metadata_tags_topics2 %>% pivot_wider(names_from = topic, values_from = g1, values_fill = 0)
metadata_content_based = right_join(metadata_content_based, metadata_tags_topics2)







metadata_content_based = metadata_content_based %>% select(-Genre, -Author)
content_based2 = metadata_content_based
content_based2 = select(content_based2, -BookName)

rownames = content_based2$id
content_based2 = content_based2 %>% dplyr::select(-id)
rownames(content_based2) = rownames
sim = lsa::cosine(t(as.matrix(content_based2)))


content_based <- function(name_book, number)
{
  
 
  
  film = books %>% filter(BookName == name_book)
  
  
  if (count(film) == 0) {
    books = books %>%  arrange(-Rating) %>%  select(BookName)
    recommend = head(books, n = number)
    
    
  } else {
    
    diag(sim) = 0
    
    simCut = sim[,as.character(film$id)]
    mostSimilar = head(sort(simCut, decreasing = T), n = number)
    a = which(simCut %in% mostSimilar, arr.ind = TRUE, useNames = T)
    index = arrayInd(a, .dim = dim(sim))
    result = rownames(sim)[index[,1]]
    
    mostSimilar = data.frame(id = as.numeric(result), similar = simCut[index])
    recommend = mostSimilar %>% left_join(books) %>% select(BookName, similar) %>% arrange(-similar)
    recommend = select(recommend, BookName)
    recommend = head(recommend, n = number)
  }
  #reccomend = reccomend
  as.list(recommend[1])
  #head(recommend, n = number)
}

a = content_based(args[1],as.numeric(args[2]))$BookName


s = NULL
for (variable in a) {
  s = paste(s,variable, sep = ",")
}
s = str_remove(s, ",")
s
