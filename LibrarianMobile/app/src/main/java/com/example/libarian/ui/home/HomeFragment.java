package com.example.libarian.ui.home;

import android.content.Intent;
import android.os.Bundle;
import android.util.Log;
import android.view.LayoutInflater;
import android.view.Menu;
import android.view.MenuInflater;
import android.view.MenuItem;
import android.view.View;
import android.view.ViewGroup;
import android.view.inputmethod.EditorInfo;
import android.widget.SearchView;
import android.widget.Toast;

import androidx.annotation.NonNull;
import androidx.annotation.Nullable;
import androidx.fragment.app.Fragment;
import androidx.recyclerview.widget.GridLayoutManager;
import androidx.recyclerview.widget.RecyclerView;

import com.example.libarian.MyIndex;
import com.example.libarian.R;
import com.example.libarian.ui.home.Adapter.BookAdapter;
import com.example.libarian.ui.home.Interface.APIBook;
import com.example.libarian.ui.home.Model.Book;

import java.time.LocalDate;
import java.time.format.DateTimeFormatter;
import java.util.ArrayList;
import java.util.Collections;
import java.util.Comparator;

import retrofit2.Call;
import retrofit2.Callback;
import retrofit2.Response;

public class HomeFragment extends Fragment implements BookAdapter.clickItem{

    RecyclerView rvBook;
    ArrayList<Book> books;
    BookAdapter bookAdapter;

    @Override
    public void onCreate(@Nullable Bundle savedInstanceState) {
        setHasOptionsMenu(true);
        super.onCreate(savedInstanceState);
    }

    public View onCreateView(@NonNull LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState) {
        return inflater.inflate(R.layout.fragment_home,container,false);

    }

    @Override
    public void onViewCreated(@NonNull View view, @Nullable Bundle savedInstanceState) {
        super.onViewCreated(view, savedInstanceState);
        books = new ArrayList<>();
        rvBook = view.findViewById(R.id.rvBook);

        bookAdapter = new BookAdapter(books, this);
        rvBook.setAdapter(bookAdapter);
        rvBook.setLayoutManager(new GridLayoutManager(getContext(),3));
        //rvBook.setLayoutManager(new LinearLayoutManager(MainActivity.this,LinearLayoutManager.VERTICAL,false));
        // rvBook.addItemDecoration(new DividerItemDecoration(MainActivity.this, LinearLayoutManager.VERTICAL));
        getListBook();
    }
    public void getListBook(){
        APIBook.api.getBook().enqueue(new Callback<ArrayList<Book>>() {
            @Override
            public void onResponse(Call<ArrayList<Book>> call, Response<ArrayList<Book>> response) {
                if(response.isSuccessful() && response.body()!=null){
                    books.addAll(response.body());
                    bookAdapter.notifyDataSetChanged();
                }
            }
            @Override
            public void onFailure(Call<ArrayList<Book>> call, Throwable t) {
                Toast.makeText(getContext(), "Lỗi", Toast.LENGTH_SHORT).show();
                Log.d("Lỗi","Lỗi");
            }
        });
    }


    @Override
    public void onCreateOptionsMenu(@NonNull Menu menu, @NonNull MenuInflater inflater) {
        inflater.inflate(R.menu.main,menu);
        SearchView searchView =(SearchView) menu.findItem(R.id.searchList).getActionView();
        searchView.setImeOptions(EditorInfo.IME_ACTION_DONE);
        searchView.setOnQueryTextListener(new SearchView.OnQueryTextListener() {
            @Override
            public boolean onQueryTextSubmit(String s) {
                bookAdapter.getFilter().filter(s);
                return false;
            }
            @Override
            public boolean onQueryTextChange(String s) {
                bookAdapter.getFilter().filter(s);
                return false;
            }
        });
        super.onCreateOptionsMenu(menu, inflater);
    }

    @Override
    public boolean onOptionsItemSelected(@NonNull MenuItem item) {

        DateTimeFormatter DATE_FORMATTER = DateTimeFormatter.ofPattern("dd/MM/yyyy");
        if(item.getItemId() == R.id.sort_title){
            Collections.sort(bookAdapter.booksFilter, new Comparator<Book>() {
                @Override
                public int compare(Book left, Book right) {
                    return left.getTitle().compareTo(right.getTitle());
                }
            });
            bookAdapter.notifyDataSetChanged();
        } else if (item.getItemId()==R.id.sort_year) {
            Collections.sort(bookAdapter.booksFilter, new Comparator<Book>() {
                @Override
                public int compare(Book left, Book right) {
                    LocalDate date1 = LocalDate.parse(left.getPublishingYear(),DATE_FORMATTER);
                    LocalDate date2 = LocalDate.parse(right.getPublishingYear(),DATE_FORMATTER);
                    return date2.compareTo(date1);
                }
            });
            bookAdapter.notifyDataSetChanged();
        }
        return super.onOptionsItemSelected(item);
    }

    @Override
    public void onItemClick(int pos, Book book) {
        MyIndex.current = pos;
        Intent intent = new Intent(getContext(),BookDetails.class);
        intent.putExtra("bookDetail", bookAdapter.booksFilter);
        startActivity(intent);
    }
}