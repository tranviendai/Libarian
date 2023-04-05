package com.example.libarian.ui.home.Interface;

import com.example.libarian.ui.home.Model.Book;
import com.google.gson.Gson;
import com.google.gson.GsonBuilder;

import java.util.ArrayList;

import retrofit2.Call;
import retrofit2.Retrofit;
import retrofit2.converter.gson.GsonConverterFactory;
import retrofit2.http.GET;

public interface APIBook {
    Gson gson = new GsonBuilder().setDateFormat("dd-MM-yyy").create();

    APIBook api = new Retrofit.Builder().
            baseUrl("http://10.0.2.2:5137/api/")
            .addConverterFactory(GsonConverterFactory.create(gson))
            .build().create(APIBook.class);

    @GET("Books")
    Call<ArrayList<Book>> getBook();
}
