package com.example.libarian.ui.home.Model;

import com.google.gson.annotations.Expose;
import com.google.gson.annotations.SerializedName;

import java.io.Serializable;

public class Book  implements Serializable {
        @SerializedName("bookID")
        @Expose
        private String bookID;
        @SerializedName("id")
        @Expose
        private String id;
        @SerializedName("title")
        @Expose
        private String title;
        @SerializedName("category")
        @Expose
        private String category;
        @SerializedName("author")
        @Expose
        private String author;
        @SerializedName("image")
        @Expose
        private String image;
        @SerializedName("publisher")
        @Expose
        private String publisher;
        @SerializedName("publishingYear")
        @Expose
        private String publishingYear;

        public String getBookID() {
            return bookID;
        }

        public void setBookID(String bookID) {
            this.bookID = bookID;
        }

        public String getId() {
            return id;
        }

        public void setId(String id) {
            this.id = id;
        }

        public String getTitle() {
            return title;
        }

        public void setTitle(String title) {
            this.title = title;
        }

        public String getCategory() {
            return category;
        }

        public void setCategory(String category) {
            this.category = category;
        }

        public String getAuthor() {
            return author;
        }

        public void setAuthor(String author) {
            this.author = author;
        }

        public String getImage() {
            return image;
        }

        public void setImage(String image) {
            this.image = image;
        }

        public String getPublisher() {
            return publisher;
        }

        public void setPublisher(String publisher) {
            this.publisher = publisher;
        }

        public String getPublishingYear() {
            return publishingYear;
        }

        public void setPublishingYear(String publishingYear) {
            this.publishingYear = publishingYear;
        }
}
