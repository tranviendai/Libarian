package com.example.libarian.ui.home;

import android.content.Intent;
import android.os.Bundle;
import android.widget.ImageView;
import android.widget.TextView;

import androidx.annotation.Nullable;
import androidx.appcompat.app.AppCompatActivity;

import com.example.libarian.MyIndex;
import com.example.libarian.R;
import com.example.libarian.ui.home.Model.Book;
import com.squareup.picasso.Picasso;

import java.util.ArrayList;

public class BookDetails extends AppCompatActivity {
    TextView tvID, tvTitle, tvAuthorPublisher,tvCategory,tvNote;
    ImageView imgMain;
    Book book;
    ArrayList<Book> books = new ArrayList<>();

    @Override
    protected void onCreate(@Nullable Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.book_details);
        getSupportActionBar().setDisplayHomeAsUpEnabled(true);

        tvID = findViewById(R.id.tvID);
        tvTitle = findViewById(R.id.tvTitle);
        tvAuthorPublisher = findViewById(R.id.tvAuthorPublisher);
        tvCategory = findViewById(R.id.tvCategory);
        tvNote = findViewById(R.id.tvNote);
        imgMain = findViewById(R.id.imgMain);

        books = (ArrayList<Book>) getIntent().getSerializableExtra("bookDetail");
        book = books.get(MyIndex.current);

        Picasso.get().load(book.getImage()).into(imgMain);
        tvID.setText("#"+book.getId());
        tvTitle.setText(book.getTitle());
        tvAuthorPublisher.setText(book.getAuthor() + " - " + book.getPublisher());
        tvCategory.setText(book.getCategory());
    }

    @Override
    public void onBackPressed() {
        super.onBackPressed();
    }

    @Override
    public boolean onSupportNavigateUp() {
        onBackPressed();
        return super.onSupportNavigateUp();
    }
}
